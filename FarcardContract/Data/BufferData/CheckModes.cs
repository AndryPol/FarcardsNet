using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    public enum CheckModes
    {
        /// <summary>
        ///    не чек (вызов вне чека или технический вызов для получения, например, только имени клиента)	в GetCardInfo - короткий XML и в Transaction, если вызвано через SimpleTransaction (из скрипта)
        /// </summary>
        [XmlEnum("0")]
        NotCheck,

        /// <summary>
        /// оплата (касса проводит операцию оплаты заказа)	в GetCardInfo перед транзакцией и в Transaction
        /// </summary>
        [XmlEnum("1")]
        Sale,

        /// <summary>
        /// откат (касса не смогла завершить все этапы оплаты корректно, но ранее интерфейс подтвердил успешную транзакцию)	в GetCardInfo перед транзакцией и в Transaction
        /// </summary>
        [XmlEnum("2")]
        SaleRollback,

        /// <summary>
        /// возврат (возврат чека после успешного оформления)	в GetCardInfo перед транзакцией и в Transaction
        /// </summary>
        [XmlEnum("3")]
        Return,

        /// <summary>
        ///  откат возврата (касса не смогла завершить все этапы возврата корректно, но ранее интерфейс подтвердил успешную транзакцию возврата)	в GetCardInfo перед транзакцией и в Transaction
        /// </summary>
        [XmlEnum("4")]
        ReturnRollback,

        /// <summary>
        /// режим заказа (заказ не завершен)	в GetCardInfo (заказ может быть незавершен)
        /// </summary>
        [XmlEnum("5")]
        OrderEditing,

        /// <summary>
        ///   печать пречека (заказ не завершен, но готов к завершению)	в GetCardInfo при макетировании и в Transaction (в режиме «все чеки/пречеки в farcards»)
        /// </summary>
        [XmlEnum("6")]
        OrderBill,

        /// <summary>
        ///  режим расчета (заказ не завершен, но готов к завершению)	в GetCardInfo
        /// </summary>
        [XmlEnum("7")]
        OrderCalc,

        /// <summary>
        ///  печать инфо о карте, возможно, не из заказа - в GetCardInfo	
        /// </summary>
        [XmlEnum("8")]
        PrintCardInfo,

        /// <summary>
        ///  создание заказа в «карте на входе»	в GetCardInfo (перед заказом, для запроса информации для создания заказа) - короткий XML
        /// </summary>
        [XmlEnum("9")]
        InitEntranceCardOrder,

        /// <summary>
        /// после транзакций для печати слипов  в GetCardInfo короткий XML
        /// </summary>
        [XmlEnum("10")]
        PrintAfterTransaction,

        /// <summary>
        /// сервис-печать (заказ не завершен)	(иногда там могут быть скидки с персональной информацией), начиная с версии 1.5
        /// </summary>
        [XmlEnum("11")]
        ServPrint,

        /// <summary>
        /// отмена пречека (заказ не завершен)	начиная с версии 2.4
        /// </summary>
        [XmlEnum("12")]
        CancelBill,

        /// <summary>
        /// перед транзакциями  начиная с версии 28
        /// </summary>
        [XmlEnum("13")]
        BeforeReceipt
    }
}