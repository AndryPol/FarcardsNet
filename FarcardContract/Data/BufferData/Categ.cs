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
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        [XmlAttribute("discsum")]
        public decimal DiscSumm { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
