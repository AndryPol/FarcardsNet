using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class Bonuses
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return BonusList?.Count ?? 0; }
            set { }
        }

        [XmlElement("BONUS")]
        public List<Bonus> BonusList { get; set; }
    }
}