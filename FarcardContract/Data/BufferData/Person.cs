using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    public class Person
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("role")]
        public string Role { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}
