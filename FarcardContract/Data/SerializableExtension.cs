using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace FarcardContract.Data
{
    public class Serializer
    {
        private static Logger<Serializer> _logger = new Logger<Serializer>(2);
        public static string SerializeObject<T>(T o, bool throwException = false)
        {
            string s = "";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                    {   
                        xmlTextWriter.Formatting = Formatting.Indented;
                        XmlSerializerNamespaces xmn = new XmlSerializerNamespaces();
                        xmn.Add("", "");
                        xmlSerializer.Serialize(xmlTextWriter, o, xmn);
                        using (var newMemoryStream = (MemoryStream)xmlTextWriter.BaseStream)
                        {
                            s = Encoding.UTF8.GetString(newMemoryStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);


                if (throwException)
                    throw ex;
            }
            return s;
        }


        public static T DeSerializeObject<T>(string xml, bool throwException = false) where T : new()
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                    using (StreamReader xmlStreamReader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        return (T)xmlSerializer.Deserialize(xmlStreamReader);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (throwException)
                    throw ex;

            }
            return new T();
        }

    }
}
