using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public enum ItemType
    {
        /// <summary>
        /// произвольный элемент
        /// </summary>
        [Description("произвольный элемент")]
        [XmlEnum("undefined")]
        Undefined,

        /// <summary>
        /// строка блюда
        /// </summary>
        [Description("строка блюда")]
        [XmlEnum("dish")]
        Dish,

        /// <summary>
        /// комбо блюдо
        /// </summary>
        [Description("комбо блюдо")]
        [XmlEnum("combo")]
        Combo,

        /// <summary>
        /// модификатор
        /// </summary>
        [Description("модификатор")]
        [XmlEnum("modify")]
        Modify,

        /// <summary>
        /// билет
        /// </summary>
        [XmlEnum("ticket")]
        Ticket
    }
}
