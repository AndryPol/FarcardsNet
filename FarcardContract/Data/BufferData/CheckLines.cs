using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckLines
    {
        /// <summary>
        /// Количество записей
        /// </summary>
        [XmlAttribute("count")]
        public int Count
        {
            get { return Lines?.Count ?? 0; }
            set { }
        }

        /// <summary>
        /// Линии чека
        /// </summary>
        [XmlElement("LINE")]
        public List<Line> Lines { get; set; }
    }
}