using FarcardContract.Data.Farcard6;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FarcardContract.HttpData
{

    public static class DTOXmlParserExt
    {
        private static readonly Logger<XmlParser> _logger = new Logger<XmlParser>(2);



        internal static T ParseAttributeToObject<T>(this XmlNode node, bool onlySerializationAttr = false)
        {
            try
            {
                if (node != null && node.Attributes != null)
                {
                    T res = Activator.CreateInstance<T>();
                    var props = typeof(T).GetProperties().Where(x => x.CanWrite && !x.Name.Equals("Item")).ToList();
                    if (onlySerializationAttr)
                        props = props.Where(x => System.Attribute.IsDefined(x, typeof(XmlAnyAttributeAttribute))).ToList();
                    var attDic = props.ToDictionary(x => x, x => x.GetCustomAttributes(typeof(XmlAttributeAttribute), true)
                        .OfType<XmlAttributeAttribute>().Where(a => string.IsNullOrWhiteSpace(a.AttributeName)));
                    if (onlySerializationAttr)
                    {
                        foreach (var prop in attDic)
                        {
                            try
                            {
                                var xAttr = prop.Value.FirstOrDefault();
                                var name = xAttr != null ? xAttr.AttributeName : prop.Key.Name;
                                var val = node.Attribute(name, prop.Key.PropertyType, false);
                                prop.Key.SetValue(res, val, null);
                            }
                            catch (Exception e)
                            {
                                _logger.Error(e);
                            }
                        }

                        return res;
                    }

                    for (int i = 0; i < node.Attributes.Count; i++)
                    {
                        try
                        {
                            var attr = node.Attributes[i];

                            var prop = props.FirstOrDefault(x =>
                                x.Name.Equals(attr.Name, StringComparison.CurrentCultureIgnoreCase));

                            if (prop != null)
                            {
                                Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                object safeValue;
                                if (t == typeof(DateTime))
                                    safeValue = XmlConvert.ToDateTime(attr.Value, new[] { "yyyy-MM-ddTHH-mm-ss", "yyyy-MM-ddTHH:mm:ss" });
                                else if (t.IsEnum)
                                    safeValue = Enum.Parse(t, attr.Value);
                                else if (t == typeof(Decimal))
                                {
                                    int val = 0;
                                    int.TryParse(attr.Value, out val);
                                    safeValue = val / 100.00m;
                                }
                                else
                                    safeValue = (attr.Value == null) ? null : Convert.ChangeType(attr.Value, t);
                                prop.SetValue(res, safeValue, null);
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e);
                        }
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return default;
        }

        internal static void GetBufferXmlFromNode(this XmlDocument document, string nameNodeBuffer, string nameAttrKind,
            out byte[] buff, out UInt16 kind)
        {
            buff = null;
            kind = 0;
            try
            {
                if (document != null && nameNodeBuffer != null)
                {
                    var encoding = document.GetEncoding();
                    var nodeBuffer = document.Node(nameNodeBuffer);
                    var doc = new XmlDocument();
                    doc.AppendDeclaration(encoding);
                    var importNode = doc.ImportNode(nodeBuffer.FirstChild, true);
                    doc.AppendChild(importNode);
                    buff = encoding.GetBytes(doc.InnerXml);
                    if (nameAttrKind != null)
                    {
                        kind = nodeBuffer.Attribute<UInt16>(nameAttrKind, false);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

        }



        internal static T GetAttributeFromNode<T>(XmlDocument document, string xnode, string attribute, bool ignoreCase = true)
        {
            var node = document.DocumentElement.Node(xnode, ignoreCase: ignoreCase);
            if (node == null)
                return default;

            return node.Attribute<T>(attribute, ignoreCase: ignoreCase);
        }

        internal static XmlNode Node(this XmlNode node, string name, bool throwException = true, bool ignoreCase = true)
        {
            var xpath = ignoreCase ? GetNodeXpathCaseInsensitive(name) : name;

            var foundNode = node.SelectSingleNode(xpath);
            if (foundNode == null && throwException)
            {
                var msg = $"node [{name}] is null or does not exist";
                _logger.Warn(msg);
                throw new InvalidOperationException(msg);
            }
            return foundNode;
        }

        internal static XmlNodeList Nodes(this XmlNode node, string name, bool throwException = true, bool ignoreCase = true)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            var xpath = ignoreCase ? GetNodeXpathCaseInsensitive(name) : name;
            var foundNodes = node.SelectNodes($"{xpath}");
            if (foundNodes == null && throwException)
            {
                var msg = $"nodes [{name}] is null or does not exist";
                _logger.Warn(msg);
                throw new InvalidOperationException(msg);
            }
            return foundNodes;
        }

        internal static T Attribute<T>(this XmlNode node, string name, bool required = true, bool ignoreCase = true)
        {
            string attrValue = "<null>";
            try
            {

                attrValue = Attribute(node, name, required, ignoreCase);
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                var res = (T)Convert.ChangeType(attrValue, t, CultureInfo.InvariantCulture);
                return res;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (required)
                    throw new InvalidCastException($"value [{attrValue}] of attribute [{name}] is not belong to type {typeof(T).Name}");
            }

            return default;
        }

        internal static object Attribute(this XmlNode node, string name, Type type, bool required = true,
            bool ignoreCase = true)
        {
            string attrValue = string.Empty;
            try
            {
                attrValue = Attribute(node, name, required, ignoreCase);
                Type t = Nullable.GetUnderlyingType(type) ?? type;
                var res = Convert.ChangeType(attrValue, t, CultureInfo.InvariantCulture);
                return res;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (required)
                    throw new InvalidCastException($"value [{attrValue}] of attribute [{name}] is not belong to type {type.Name}");
            }
            return default;
        }

        internal static string Attribute(this XmlNode node, string name, bool required = true, bool ignoreCase = true)
        {
            if (node == null)
            {
                var msg = "node is NULL";
                _logger.Warn(msg);
                throw new InvalidOperationException(msg);
            }

            if (node.Attributes == null)
            {
                var msg = $"node [{node.Name}] have no attributes";
                _logger.Warn(msg);
                throw new InvalidOperationException(msg);
            }

            if (ignoreCase)
            {
                for (int i = 0; i < node.Attributes.Count; i++)
                {
                    var atr = node.Attributes[i];
                    if (atr.LocalName.ToLower() == name.ToLower())
                        return atr.Value;
                }
            }

            var attr = node.Attributes[name];

            if (attr == null)
            {
                if (!required)
                    return null;
                var msg = $"attribute [{name}] of node [{node.Name}] is NULL or does not exist";
                _logger.Warn(msg);
                throw new InvalidOperationException(msg);
            }

            return attr.Value;
        }

        internal static Encoding GetEncoding(this XmlDocument document)
        {
            Encoding encoding = Encoding.UTF8;
            var dec = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            if (dec != null)
            {
                try
                {
                    encoding = Encoding.GetEncoding(dec.Encoding);
                }
                catch (ArgumentException ex)
                {
                    _logger.Error(ex);
                }
            }
            return encoding;
        }


        private static string GetNodeXpathCaseInsensitive(this string value)
        {
            string xpath = String.Format("//*[translate(name(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz') = '{0}']", value.ToLower());
            return xpath;
        }

        class XmlParser
        {

        }

    }

}
