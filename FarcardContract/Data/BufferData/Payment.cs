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
    public class Payment
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Название элемента в строке
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Сумма внесенная в этой валюте
        /// </summary>
        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        /// <summary>
        /// Сумма внесенная в базовой валюте, должна совпадать с суммой sum по LINE
        /// </summary>
        [XmlAttribute("bsum")]
        public decimal BSumm { get; set; }

        /// <summary>
        /// Интерфейс (в RK7 код логического интерфейса)
        /// </summary>
        [XmlAttribute("interface")]
        public int Interface { get; set; }

        /// <summary>
        /// Код карты
        /// </summary>
        [XmlAttribute("cardcode")]
        public string CardCode { get; set; }

        /// <summary>
        /// Авторизованное имя гостя
        /// </summary>
        [XmlAttribute("ownerinfo")]
        public string OwnerInfo { get; set; }

        /// <summary>
        /// ver 1.4 идентификатор линии
        /// </summary>
        [XmlAttribute("uni")]
        public string Uni { get; set; }

        /// <summary>
        /// ver 1.4 внешняя информация о транзакции, например, информация от терминала авторизации
        /// </summary>
        [XmlAttribute("exttransactioninfo")]
        public string ExtraTransactionInfo { get; set; }

        /// <summary>
        /// Признак типа оплаты (1 - наличные 2 - банковские карты 3 - прочее)
        /// </summary>
        [XmlAttribute("paytype")]
        public PayTypes PayType { get; set; }
    }
}
