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
    public class Bonus
    {
        /// <summary>
        /// ID бонуса
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// код бонуса
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Сумма рассчитанного бонуса
        /// </summary>
        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        /// <summary>
        /// Название бонуса
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Код карты
        /// </summary>
        [XmlAttribute("cardcode")]
        public string CardCode { get; set; }

        /// <summary>
        /// Интерфейс (в RK7 код логического интерфейса)
        /// </summary>
        [XmlAttribute("interface")]
        public int Interface { get; set; }

        /// <summary>
        /// Идентификатор линии (скидки)
        /// </summary>
        [XmlAttribute("uni")]
        public string Uni { get; set; }
    }
}
