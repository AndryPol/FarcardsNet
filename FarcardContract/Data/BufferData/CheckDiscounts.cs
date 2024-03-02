using System.Collections.Generic;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public class CheckDiscounts
    {
        /// <summary>
        /// Количество записей
        /// </summary>
        [XmlAttribute("count")]
        public int Count
        {
            get { return Discounts?.Count ?? 0; }
            set { }
        }

        /// <summary>
        /// Информация о скидках
        /// </summary>
        [XmlElement("DISCOUNT")]
        public List<Discount> Discounts { get; set; }
    }
}