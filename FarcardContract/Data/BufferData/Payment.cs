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
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        [XmlAttribute("bsum")]
        public decimal BSumm { get; set; }

        [XmlAttribute("interface")]
        public int Interface { get; set; }

        [XmlAttribute("cardcode")]
        public string CardCode { get; set; }

        [XmlAttribute("ownerinfo")]
        public string OwnerInfo { get; set; }

        [XmlAttribute("uni")]
        public string Uni { get; set; }

        [XmlAttribute("exttransactioninfo")]
        public string ExtraTransactionInfo { get; set; }

        [XmlAttribute("paytype")]
        public PayTypes PayType { get; set; }
    }
}
