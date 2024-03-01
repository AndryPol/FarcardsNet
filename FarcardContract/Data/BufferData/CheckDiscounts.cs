using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckDiscounts
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return Discounts?.Count ?? 0; }
            set { }
        }

        [XmlElement("DISCOUNT")]
        public List<Discount> Discounts { get; set; }
    }
}