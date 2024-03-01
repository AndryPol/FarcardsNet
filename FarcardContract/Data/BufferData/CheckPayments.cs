using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckPayments
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return Payments?.Count ?? 0; }
            set { }
        }

        [XmlElement("PAYMENT")]
        public List<Payment> Payments { get; set; }
    }
}