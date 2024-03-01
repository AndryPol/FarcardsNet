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
        [XmlAttribute("ordernum")]
        public string OrderNum { get; set; }

        [XmlAttribute("orderguid")]
        public string OrderGuid { get; set; }

        [XmlAttribute("quests")]
        public int Guests { get; set; }

        [XmlAttribute("checknum")]
        public string CheckNum { get; set; }

        [XmlAttribute("tablename")]
        public string TableName { get; set; }

        [XmlAttribute("order_cat")]
        public string OrderCat { get; set; }

        [XmlAttribute("order_type")]
        public string OrderType { get; set; }

        [XmlAttribute("startservice")]
        public DateTime StartService { get; set; }

        [XmlAttribute("closedatetime")]
        public DateTime ClosedDateTime { get; set; }

        [XmlAttribute("checkguid")]
        public string CheckGuid { get; set; }

        [XmlAttribute("printnum")]
        public string PrintNum { get; set; }

        [XmlAttribute("extfiscid")]
        public string ExtFiscId { get; set; }

        [XmlAttribute("delprintnum")]
        public string DelPrintNum { get; set; }

        [XmlAttribute("fiscdocnum")]
        public string FiscDocNum { get; set; }

        [XmlAttribute("delfiscdocnum")]
        public string DelFiscDocNum { get; set; }

        [XmlAttribute("persistentcomment")]
        public string PersistentComment { get; set; }

        [XmlElement("CHECKPERSONS")]
        public CheckPersons CheckPersons { get; set; }

        [XmlElement("CHECKLINES", IsNullable = false), DefaultValue(null)]
        public CheckLines CheckLines { get; set; }

        [XmlElement("CHECKCATEGS", IsNullable = false), DefaultValue(null)]
        public CheckCategs CheckCategs { get; set; }

        [XmlElement("CHECKDISCOUNTS", IsNullable = false), DefaultValue(null)]
        public CheckDiscounts CheckDiscounts { get; set; }

        [XmlElement("BONUSES", IsNullable = false), DefaultValue(null)]
        public Bonuses Bonuses { get; set; }

        [XmlElement("DISCOUNTS", IsNullable = false), DefaultValue(null)]
        public Discounts Discounts { get; set; }

        [XmlElement("CHECKPAYMENTS", IsNullable = false), DefaultValue(null)]
        public CheckPayments CheckPayments { get; set; }

    }
}
