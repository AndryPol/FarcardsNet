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
        /// <summary>
        /// ID объекта справочника
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Текст - имя кассира/официанта
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Код роли
        /// </summary>
        [XmlAttribute("role")]
        public string Role { get; set; }

        /// <summary>
        /// Код объекта справочника
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}
