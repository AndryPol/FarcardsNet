using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class Bonuses
    {
        /// <summary>
        /// Количество записей
        /// </summary>
        [XmlAttribute("count")]
        public int Count
        {
            get { return BonusList?.Count ?? 0; }
            set { }
        }

        /// <summary>
        /// Информация о бонусах
        /// </summary>
        [XmlElement("BONUS")]
        public List<Bonus> BonusList { get; set; }
    }
}