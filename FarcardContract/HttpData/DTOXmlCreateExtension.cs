using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FarcardContract.HttpData
{
    public static class DTOXmlCreateExtension
    {
        internal static bool AppendAttributesFromObject<T>(this XmlElement node, T objectAttributes, bool onlySerializationAttr = false, XmlDocument doc = null)
        {
            bool res = false;
            try
            {
                if (doc == null)
                    doc = node.OwnerDocument;
                if (node != null && objectAttributes != null && doc != null)
                {
                    var props = objectAttributes.GetType().GetProperties().Where(x => x.CanRead && !x.Name.Equals("Item"));
                    if (onlySerializationAttr)
                    {
                        props = props.Where(x => Attribute.IsDefined(x, typeof(XmlAnyAttributeAttribute)));
                    }

                    var attDic = props.ToDictionary(x => x, x => x.GetCustomAttributes(typeof(XmlAttributeAttribute), true)
                        .OfType<XmlAttributeAttribute>().Where(a => string.IsNullOrWhiteSpace(a.AttributeName)));

                    foreach (var prop in props)
                    {
                        object val;
                        if (prop.PropertyType.BaseType == typeof(Enum))
                        {
                            val = Convert.ChangeType(prop.GetValue(objectAttributes, null), typeof(long));
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            val = ((DateTime)prop.GetValue(objectAttributes, null)).ToString("yyyy-MM-ddTHH:mm:ss");
                        }
                        else if (prop.PropertyType == typeof(Decimal))
                        {
                            val = (Convert.ToInt64((decimal)prop.GetValue(objectAttributes, null) * 100));
                        }
                        else
                        {
                            val = prop.GetValue(objectAttributes, null);
                        }
                        var xAtt = attDic[prop].FirstOrDefault();
                        var name = xAtt != null ? xAtt.AttributeName : prop.Name;
                        node.AppendAttribute(name, val);
                    }

                    res = true;
                }
            }
            catch { }
            return res;
        }

        internal static XmlElement AppendXmlBuffer(this XmlNode node, string name, byte[] xmlBuff, XmlDocument doc = null)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (xmlBuff == null || xmlBuff.Length == 0)
                throw new ArgumentException("xml buffer is null or empty");

            if (doc == null)
            {
                if (node.NodeType == XmlNodeType.Document)
                    doc = node as XmlDocument;
                else
                    doc = node.OwnerDocument;
            }

            if (doc == null)
                throw new ArgumentException("owner document is null");
            using (var ms = new MemoryStream(xmlBuff))
            {
                var inpDoc = new XmlDocument();
                inpDoc.Load(ms);
                var xmlDec = inpDoc.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
                if (xmlDec != null)
                    inpDoc.RemoveChild(xmlDec);

                var inpBuf = node.AppendElement(name);
                var importNode = doc.ImportNode(inpDoc.FirstChild, true);
                inpBuf.AppendChild(importNode);
                return inpBuf;
            }
        }

        internal static void AppendDeclaration(this XmlDocument document, Encoding encoding = null, string version = "1.0")
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            var dec = document.CreateXmlDeclaration(version, encoding?.WebName, null);
            document.AppendChild(dec);
        }

        internal static XmlElement AppendElement(this XmlNode node, string name, XmlDocument doc = null)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (doc == null)
            {
                if (node.NodeType == XmlNodeType.Document)
                    doc = node as XmlDocument;
                else
                    doc = node.OwnerDocument;
            }

            if (doc == null)
                throw new ArgumentException("owner document is null");

            var newNode = doc.CreateElement(name);
            node.AppendChild(newNode);

            return newNode;
        }

        internal static void AppendAttribute(this XmlElement node, string name, object value, XmlDocument doc = null)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (doc == null)
                doc = node.OwnerDocument;

            if (doc == null)
                throw new ArgumentException("Owner document is null");

            var attr = doc.CreateAttribute(name);
            if (value != null)
                attr.Value = value.ToString();

            node.Attributes.Append(attr);

        }
    }
}
