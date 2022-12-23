using System;

namespace FarcardContract.Farcard6
{
    public interface ITransactionInfoEx
    {
        UInt16 Sieze { get; set; }

        /// <summary>
        /// Карта
        /// </summary>
        long Card { get; set; }

        /// <summary>
        /// Идентификатор владельца карты
        /// </summary>
        long PersonID { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        UInt32 Account { get; set; }

        /// <summary>
        /// Тип транзакции 0 - платеж(снятие денег со счета) 1 - скидка 2 - бонус(начисление денег на счет)3 - потраты гостя(сколько заплатил своих денег)
        /// </summary>
        TransactType Kind { get; set; }

        /// <summary>
        ///<para/>сумма, в копейках, для типа 0 (платеж) :
        ///<para/> оплата снятием денег с карты - отрицательная сумма,
        ///<para/>отмена оплаты - положительная сумма;
        /// <para/>для типа 1 (скидка) :	скидка клиенту - отрицательная сумма,
        ///<para/> отмена скидки - положительная сумма;
        ///<para/> для типа 2 (бонус) :клиенту начисляется бонус - положительная сумма,
        /// <para/>отмена бонуса - отрицательная сумма; 
        /// <para/>для типа 3 (потраты) :	клиент заплатил - положительная сумма,
        ///<para/> отмена чека - отрицательная сумма;
        /// </summary>
        Decimal Summa { get; set; }

        /// <summary>
        /// код ресторана
        /// </summary>
        UInt16 Restaurant { get; set; }

        /// <summary>
        /// кассовая дата(0 -> 30 / 12 / 1899)
        /// </summary>
        DateTime RKDate { get; set; }

        /// <summary>
        ///  номер кассы
        /// </summary>
        Byte RKUni { get; set; }

        /// <summary>
        ///  номер чека
        /// </summary>
        UInt32 RKCheck { get; set; }

        /// <summary>
        /// сумма с налогом A
        /// </summary>
        Decimal VatSumA { get; set; }

        /// <summary>
        /// размер налога A в процентах  15.00%)
        /// </summary>
        Decimal VatPrcA { get; set; }

        /// <summary>
        /// сумма с налогом B
        /// </summary>
        Decimal VatSumB { get; set; }

        /// <summary>
        ///  размер налога B в процентах * 100
        /// </summary>
        Decimal VatPrcB { get; set; }

        /// <summary>
        ///  сумма с налогом C
        /// </summary>
        Decimal VatSumC { get; set; }

        /// <summary>
        /// размер налога C в процентах * 100
        /// </summary>
        Decimal VatPrcC { get; set; }

        /// <summary>
        ///  сумма с налогом D
        /// </summary>
        Decimal VatSumD { get; set; }

        /// <summary>
        ///  размер налога D в процентах * 100
        /// </summary>
        Decimal VatPrcD { get; set; }

        /// <summary>
        ///  сумма с налогом E
        /// </summary>
        Decimal VatSumE { get; set; }

        /// <summary>
        ///  размер налога E в процентах * 100
        /// </summary>
        Decimal VatPrcE { get; set; }

        /// <summary>
        /// сумма с налогом F
        /// </summary>

        Decimal VatSumF { get; set; }

        /// <summary>
        ///  размер налога F в процентах * 100
        /// </summary>
        Decimal VatPrcF { get; set; }

        /// <summary>
        ///  сумма с налогом G
        /// </summary>
        Decimal VatSumG { get; set; }

        /// <summary>
        /// размер налога G в процентах * 100
        /// </summary>
        Decimal VatPrcG { get; set; }

        /// <summary>
        /// сумма с налогом H
        /// </summary>
        Decimal VatSumH { get; set; }

        /// <summary>
        /// размер налога H в процентах * 100
        /// </summary>
        Decimal VatPrcH { get; set; }
    }

    /// <summary>
    ///Тип транзакции 0 - платеж(снятие денег со счета) 1 - скидка 2 - бонус(начисление денег на счет)3 - потраты гостя(сколько заплатил своих денег) 
    /// </summary>
    public enum TransactType : byte
    {

        payment = 0,
        discount = 1,
        bonus = 2,
        spending = 3
    }
}