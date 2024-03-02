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
    public class Discount
    {
        /// <summary>
        /// ID скидки
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Код скидки
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Сумма скидки фактически примененной
        /// </summary>
        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        /// <summary>
        /// Название скидки
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
        /// ver 1.4 идентификатор линии в заказе
        /// </summary>
        [XmlAttribute("uni")]
        public string Uni { get; set; }

        /// <summary>
        /// ver 1.4 идентификатор линии объекта (линии блюда), может быть пустым, если скидка на весь чек (в том числе на категорию блюд)
        /// </summary>
        [XmlAttribute("objectuni")]
        public string ObjectUni { get; set; }
    }
}
