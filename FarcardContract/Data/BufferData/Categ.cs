using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    public class Categ
    {
        /// <summary>
        /// ID категории классификации сервис печати
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Код категории классификации сервис печати
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        /// <summary>
        /// Сумма фактических скидок по этой категории
        /// </summary>
        [XmlAttribute("discsum")]
        public decimal DiscSumm { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
