using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    public class CheckData
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        [XmlAttribute("ordernum")]
        public string OrderNum { get; set; }

        /// <summary>
        /// GUID заказа
        /// </summary>
        [XmlAttribute("orderguid")]
        public string OrderGuid { get; set; }

        /// <summary>
        /// Количество гостей
        /// </summary>
        [XmlAttribute("quests")]
        public int Guests { get; set; }

        /// <summary>
        /// Номер чека, на момент передачи может отсутствовать, появляется только если чек распечатан
        /// </summary>
        [XmlAttribute("checknum")]
        public string CheckNum { get; set; }

        /// <summary>
        /// Номер стола, может отсутствовать
        /// </summary>
        [XmlAttribute("tablename")]
        public string TableName { get; set; }

        /// <summary>
        /// ver 1.5 категория заказа (код), может отсутствовать
        /// </summary>
        [XmlAttribute("order_cat")]
        public string OrderCat { get; set; }

        /// <summary>
        /// ver 1.5 тип заказа (код), может отсутствовать
        /// </summary>
        [XmlAttribute("order_type")]
        public string OrderType { get; set; }

        /// <summary>
        /// Время начала обслуживания
        /// </summary>
        [XmlAttribute("startservice")]
        public DateTime StartService { get; set; }

        /// <summary>
        /// Время окончания обслуживания, передается только если обслуживание завершено
        /// </summary>
        [XmlAttribute("closedatetime")]
        public DateTime ClosedDateTime { get; set; }

        /// <summary>
        /// GUID в формате {XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}
        /// </summary>
        [XmlAttribute("checkguid")]
        public string CheckGuid { get; set; }

        /// <summary>
        /// Печатный номер чека, с версии 30
        /// </summary>
        [XmlAttribute("printnum")]
        public string PrintNum { get; set; }

        /// <summary>
        /// Серийный номер ФР, с версии 30
        /// </summary>
        [XmlAttribute("extfiscid")]
        public string ExtFiscId { get; set; }

        /// <summary>
        /// Печатный номер документа удаления чека, с версии 31
        /// </summary>
        [XmlAttribute("delprintnum")]
        public string DelPrintNum { get; set; }

        /// <summary>
        /// Фискальный номер документа для печати чека, с версии 32
        /// </summary>
        [XmlAttribute("fiscdocnum")]
        public string FiscDocNum { get; set; }

        /// <summary>
        /// Фискальный номер документа удаления чека, с версии 32
        /// </summary>
        [XmlAttribute("delfiscdocnum")]
        public string DelFiscDocNum { get; set; }

        /// <summary>
        /// Сохраняемый комментарий к визиту, с версии 34
        /// </summary>
        [XmlAttribute("persistentcomment")]
        public string PersistentComment { get; set; }

        /// <summary>
        /// Персонал
        /// </summary>
        [XmlElement("CHECKPERSONS")]
        public CheckPersons CheckPersons { get; set; }

        /// <summary>
        /// Линии чека
        /// </summary>
        [XmlElement("CHECKLINES", IsNullable = false), DefaultValue(null)]
        public CheckLines CheckLines { get; set; }

        /// <summary>
        /// Категория в чеке
        /// </summary>
        [XmlElement("CHECKCATEGS", IsNullable = false), DefaultValue(null)]
        public CheckCategs CheckCategs { get; set; }

        /// <summary>
        /// Скидки в чеке
        /// </summary>
        [XmlElement("CHECKDISCOUNTS", IsNullable = false), DefaultValue(null)]
        public CheckDiscounts CheckDiscounts { get; set; }

        /// <summary>
        /// Информация о бонусах
        /// </summary>
        [XmlElement("BONUSES", IsNullable = false), DefaultValue(null)]
        public Bonuses Bonuses { get; set; }

        /// <summary>
        /// Информация о скидках
        /// </summary>
        [XmlElement("DISCOUNTS", IsNullable = false), DefaultValue(null)]
        public Discounts Discounts { get; set; }

        /// <summary>
        /// Оплаты в чеке
        /// </summary>
        [XmlElement("CHECKPAYMENTS", IsNullable = false), DefaultValue(null)]
        public CheckPayments CheckPayments { get; set; }

    }
}
