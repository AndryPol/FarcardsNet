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
        /// <summary>
        /// ID объекта справочника
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор линии
        /// </summary>
        [XmlAttribute("uni")]
        public string Uni { get; set; }

        /// <summary>
        /// Ссылка на родительский идентификатор линии
        /// </summary>
        [XmlAttribute("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Текст - то что в чеке
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Тип строки <see cref="ItemType.cs"/>
        /// </summary>
        [XmlAttribute("type")]
        public ItemType Type { get; set; }

        /// <summary>
        /// Код объекта справочника
        /// </summary>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Количество элементов в строке чека
        /// </summary>
        [XmlAttribute("quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Цена по действующему прейскуранту (до скидок, наценок)
        /// </summary>
        [XmlAttribute("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Сумма после скидок/наценок
        /// </summary>
        [XmlAttribute("sum")]
        public decimal Summ { get; set; }

        /// <summary>
        /// Название категории классификации сервис печати
        /// </summary>
        [XmlAttribute("servprint")]
        public string ServPrint { get; set; }

        /// <summary>
        /// ID категории классификации сервис печати
        /// </summary>
        [XmlAttribute("servprint_id")]
        public string ServPrintId { get; set; }

        /// <summary>
        /// Название категории для отчетов
        /// </summary>
        [XmlAttribute("categ")]
        public string Categ { get; set; }

        /// <summary>
        /// ver 2.2 ID категории. Заполняется из классификации, которая указана в параметре Классификация для КДС и VDU. Вместо него можно использовать:
        /// servprint_id — указан в параметре Классификация для ОбщСменОтч
        /// egais_categ_id — указан в параметре Классификация для ЕГАИС, доступном с версии r_keeper 7.7.0.223.
        /// </summary>
        [XmlAttribute("categ_id")]
        public string CategId { get; set;}

    }
}
