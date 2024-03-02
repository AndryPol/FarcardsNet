using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    [XmlRoot("CHECK")]
    public class Check
    {
        /// <summary>
        /// Код станции, на которой сгенерирован документ
        /// </summary>
        [XmlAttribute("stationcode")]
        public string StationCode { get; set; }

        /// <summary>
        /// Код ресторана, в котором сгенерирован документ
        /// </summary>
        [XmlAttribute("restaurantcode")]
        public string RestaurantCode { get; set; }

        /// <summary>
        /// Имя (сетевое) кассового сервера, на котором сгенерирован документ
        /// </summary>
        [XmlAttribute("cashservername")]
        public string CashServerName { get; set; }

        /// <summary>
        /// Время генерации документа(чека)
        /// </summary>
        [XmlAttribute("generateddatetime")]
        public DateTime GeneratedDateTime { get; set; }

        /// <summary>
        /// Режим чека(<see cref="CheckModes.cs"/>) , с версии 1.4 типы 0-10, до версии 1.4 поддерживались типы 0-4
        /// </summary>
        [XmlAttribute("chmode")]
        public CheckModes CheckMode { get; set; }

        /// <summary>
        /// Windows locale клиента, с версии 2.4, см.(<see href="https://msdn.microsoft.com/en-us/goglobal/bb896001.aspx"/>) , число из первой колонки надо перевести в десятичное
        /// </summary>
        [XmlAttribute("locale")]
        public int Locale { get; set; }

        /// <summary>
        /// с версии 2.6, дата смены
        /// </summary>
        [XmlAttribute("shiftdate")]
        public DateTime ShiftDate { get; set; }

        /// <summary>
        /// с версии 2.6, номер смены
        /// </summary>
        [XmlAttribute("shiftnum")]
        public int ShiftNum { get; set; }

        /// <summary>
        /// с версии 27, версия протокола, поддерживаемая клиентом FarCards - генератором XML
        /// </summary>
        [XmlAttribute("protocolversion")]
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// с версии 27, Имя приложения-клиента FarCards, генератора XML (RK6, RK7, Shelter, …)
        /// </summary>
        [XmlAttribute("clientapp")]
        public string ClientApp { get; set; }

        /// <summary>
        /// с версии 27, Версия приложения-клиента FarCards, генератора XML (RK6, RK7, Shelter, …)
        /// </summary>
        [XmlAttribute("clientversion")]
        public string ClientVersion { get; set; }

        /// <summary>
        /// Содержимое чека
        /// </summary>
        [XmlElement("CHECKDATA")]
        public CheckData CheckData { get; set; }

    }
}
