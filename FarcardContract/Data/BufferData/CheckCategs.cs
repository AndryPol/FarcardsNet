using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckCategs
    {
        /// <summary>
        /// Количество записей
        /// </summary>
        [XmlAttribute("count")]
        public int Count
        {
            get { return Categs?.Count ?? 0; }
            set { }
        }

        /// <summary>
        /// Информация о категории
        /// </summary>
        [XmlElement("CATEG")]
        public List<Categ> Categs { get; set; }
    }
}