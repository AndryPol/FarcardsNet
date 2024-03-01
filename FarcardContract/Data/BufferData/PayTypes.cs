using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public enum PayTypes
    {
        /// <summary>
        /// Наличные
        /// </summary>
        [XmlEnum("1")]
        Cash = 1,

        /// <summary>
        /// Банковская карта
        /// </summary>
        [XmlEnum("2")]
        BankCard = 2,

        /// <summary>
        /// Прочее
        /// </summary>
        [XmlEnum("3")]
        Other = 3
    }
}