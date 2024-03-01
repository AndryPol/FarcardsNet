using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    public class Line
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("uni")]
        public string Uni { get; set; }

        [XmlAttribute("parent")]
        public string Parent { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public ItemType Type { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("quantity")]
        public decimal Quantity { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        [XmlAttribute("servprint")]
        public string ServPrint { get; set; }

        [XmlAttribute("servprint_id")]
        public string ServPrintId { get; set; }

        [XmlAttribute("categ")]
        public string Categ { get; set; }

        [XmlAttribute("categ_id")]
        public string CategId { get; set;}

    }
}
