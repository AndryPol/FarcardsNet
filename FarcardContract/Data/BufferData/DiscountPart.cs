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
    public class DiscountPart
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("disclineuni")]
        public int DiscLineUni { get; set; }

        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

      
    }
}
