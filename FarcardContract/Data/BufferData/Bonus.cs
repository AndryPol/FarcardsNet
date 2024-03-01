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
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("cardcode")]
        public string CardCode { get; set; }

        [XmlAttribute("interface")]
        public int Interface { get; set; }

        [XmlAttribute("uni")]
        public string Uni { get; set; }
    }
}
