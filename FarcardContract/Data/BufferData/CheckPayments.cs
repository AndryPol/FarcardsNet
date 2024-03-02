using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckPayments
    {
        /// <summary>
        /// Количество записей
        /// </summary>
        [XmlAttribute("count")]
        public int Count
        {
            get { return Payments?.Count ?? 0; }
            set { }
        }

        /// <summary>
        /// Информация об оплатах
        /// </summary>
        [XmlElement("PAYMENT")]
        public List<Payment> Payments { get; set; }
    }
}