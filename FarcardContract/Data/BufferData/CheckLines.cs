using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckLines
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return Lines?.Count ?? 0; }
            set { }
        }

        [XmlElement("LINE")]
        public List<Line> Lines { get; set; }
    }
}