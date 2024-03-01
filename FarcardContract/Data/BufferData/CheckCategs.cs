using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckCategs
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return Categs?.Count ?? 0; }
            set { }
        }

        [XmlElement("CATEG")]
        public List<Categ> Categs { get; set; }
    }
}