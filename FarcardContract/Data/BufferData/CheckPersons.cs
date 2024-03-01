using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{

    public class CheckPersons
    {
        [XmlAttribute("count")]
        public int Count
        {
            get { return Persons?.Count ?? 0; }
            set { }
        }

        [XmlElement("PERSON")]
        public List<Person> Persons { get; set; }
    }
}
