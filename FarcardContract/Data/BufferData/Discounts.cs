using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class Discounts
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return DiscountParts?.Count ?? 0; }
            set { }
        }

        [XmlElement("DISCOUNTPART")]
        public List<DiscountPart> DiscountParts { get; set; }
    }
}