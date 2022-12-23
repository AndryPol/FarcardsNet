using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract.Farcard6
{
    /// <summary>
    /// Информация о транзакции для применения на сервере
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 122)]
    public class TransactionInfoEx : ITransactionInfoEx
    {
        private readonly UInt16 _size = 122;
        public UInt16 Sieze
        {
            get => 0;
            set { }
        }
        /// <summary>
        /// Карта
        /// </summary>
        [Description("Карта")]
        public long Card
        {
            get => _lCard;
            set => _lCard = value;
        }

        private long _lCard;

        /// <summary>
        /// Идентификатор владельца карты
        /// </summary>
        [Description("Идентификатор владельца карты")]
        public long PersonID
        {
            get => _lPersonId;
            set => _lPersonId = value;
        }

        private long _lPersonId;

        /// <summary>
        /// Номер счета
        /// </summary>
        [Description("Номер счета")]
        public UInt32 Account
        {
            get => _dwAccount;
            set => _dwAccount = value;
        }

        private UInt32 _dwAccount;

        /// <summary>
        /// Тип транзакции 0 - платеж(снятие денег со счета) 1 - скидка 2 - бонус(начисление денег на счет)3 - потраты гостя(сколько заплатил своих денег)
        /// </summary>
        [Description("Тип транзакции")]
        public TransactType Kind
        {
            get => _bKind;
            set => _bKind = value;
        }

        private TransactType _bKind;
        // [FieldOffset(21)]
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
        [Description("сумма")]
        public Decimal Summa
        {
            get => _lSumma / 100.00m;
            set => _lSumma = Convert.ToInt64(value * 100);
        }

        private Int64 _lSumma;

        /// <summary>
        /// код ресторана
        /// </summary>
        [Description("код ресторана")]
        public UInt16 Restaurant
        {
            get => _wRestaurant;
            set => _wRestaurant = value;
        }

        private UInt16 _wRestaurant;

        /// <summary>
        /// кассовая дата(0 -> 30 / 12 / 1899)
        /// </summary>
        [Description("кассовая дата")]
        public DateTime RKDate
        {
            get => new DateTime(1899, 12, 30).AddDays(_dwRKDate);
            set => _dwRKDate = (uint)(value - new DateTime(1899, 12, 30)).Days;
        }

        private UInt32 _dwRKDate;

        /// <summary>
        ///  номер кассы
        /// </summary>
        [Description("номер кассы")]
        public Byte RKUni
        {
            get => _bRkUni;
            set => _bRkUni = value;
        }

        private byte _bRkUni;

        /// <summary>
        ///  номер чека
        /// </summary>
        [Description("номер чека")]
        public UInt32 RKCheck
        {
            get => _dwRkCheck;
            set => _dwRkCheck = value;
        }

        private UInt32 _dwRkCheck;

        /// <summary>
        /// сумма с налогом A
        /// </summary>
        [Description("сумма с налогом A")]
        public Decimal VatSumA
        {
            get => _lVatSumA / 100.00m;
            set => _lVatSumA = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumA;

        /// <summary>
        /// размер налога A в процентах  15.00%)
        /// </summary>
        [Description("размер налога A")]
        public Decimal VatPrcA
        {
            get => _wVatPrcA / 100.00m;
            set => _wVatPrcA = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcA;

        /// <summary>
        /// сумма с налогом B
        /// </summary>
        [Description("сумма с налогом B")]
        public Decimal VatSumB
        {
            get => _lVatSumB / 100.00m;
            set => _lVatSumB = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumB;

        /// <summary>
        ///  размер налога B в процентах * 100
        /// </summary>
        [Description("размер налога B")]
        public Decimal VatPrcB
        {
            get => _wVatPrcB / 100.00m;
            set => _wVatPrcB = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcB;

        /// <summary>
        ///  сумма с налогом C
        /// </summary>
        [Description("сумма с налогом C")]
        public Decimal VatSumC
        {
            get => _lVatSumC / 100.00m;
            set => _lVatSumC = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumC;

        /// <summary>
        /// размер налога C в процентах * 100
        /// </summary>
        [Description("размер налога C")]
        public Decimal VatPrcC
        {
            get => _wVatPrcC / 100.00m;
            set => _wVatPrcC = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcC;

        /// <summary>
        ///  сумма с налогом D
        /// </summary>
        [Description("сумма с налогом D")]
        public Decimal VatSumD
        {
            get => _lVatSumD / 100.00m;
            set => _lVatSumD = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumD;

        /// <summary>
        ///  размер налога D в процентах * 100
        /// </summary>
        [Description("размер налога D")]
        public Decimal VatPrcD
        {
            get => _wVatPrcD / 100.00m;
            set => _wVatPrcD = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcD;

        /// <summary>
        ///  сумма с налогом E
        /// </summary>
        [Description("сумма с налогом E")]
        public Decimal VatSumE
        {
            get => _lVatSumE / 100.00m;
            set => _lVatSumE = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumE;

        /// <summary>
        ///  размер налога E в процентах * 100
        /// </summary>
        [Description("размер налога E")]
        public Decimal VatPrcE
        {
            get => _wVatPrcE / 100.00m;
            set => _wVatPrcE = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcE;

        /// <summary>
        /// сумма с налогом F
        /// </summary>

        [Description("сумма с налогом F")]
        public Decimal VatSumF
        {
            get => _lVatSumF / 100.00m;
            set => _lVatSumF = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumF;

        /// <summary>
        ///  размер налога F в процентах * 100
        /// </summary>
        [Description("размер налога F")]
        public Decimal VatPrcF
        {
            get => _wVatPrcF / 100.00m;
            set => _wVatPrcF = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcF;

        /// <summary>
        ///  сумма с налогом G
        /// </summary>
        [Description("сумма с налогом G")]
        public Decimal VatSumG
        {
            get => _lVatSumG / 100.00m;
            set => _lVatSumG = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumG;

        /// <summary>
        /// размер налога G в процентах * 100
        /// </summary>
        [Description("размер налога G")]
        public Decimal VatPrcG
        {
            get => _wVatPrcG / 100.00m;
            set => _wVatPrcG = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcG;

        /// <summary>
        /// сумма с налогом H
        /// </summary>
        [Description("сумма с налогом H")]
        public Decimal VatSumH
        {
            get => _lVatSumH / 100.00m;
            set => _lVatSumH = Convert.ToInt64(value * 100);
        }

        private Int64 _lVatSumH;

        /// <summary>
        /// размер налога H в процентах * 100
        /// </summary>
        [Description("размер налога H")]
        public Decimal VatPrcH
        {
            get => _wVatPrcH / 100.00m;
            set => _wVatPrcH = Convert.ToUInt16(value * 100);
        }

        private UInt16 _wVatPrcH;

        public TransactionInfoEx()
        {
            _lCard = 0;
            _lPersonId = 0;
            _dwAccount = 0;
            _bKind = 0;
            _lSumma = 0;
            _wRestaurant = 0;
            _dwRKDate = 0;
            _bRkUni = 0;
            _dwRkCheck = 0;
            _lVatSumA = 0;
            _wVatPrcA = 0;
            _lVatSumB = 0;
            _wVatPrcB = 0;
            _lVatSumC = 0;
            _wVatPrcC = 0;
            _lVatSumD = 0;
            _wVatPrcD = 0;
            _lVatSumE = 0;
            _wVatPrcE = 0;
            _lVatSumF = 0;
            _wVatPrcF = 0;
            _lVatSumG = 0;
            _wVatPrcG = 0;
            _lVatSumH = 0;
            _wVatPrcH = 0;
        }




        public string ToStringLog()
        {
            var sb = new StringBuilder();

            var pr = this.GetType().GetProperties();
            for (int i = 0; i < pr.Length; i++)
            {
                var n = pr[i].Name;
                DescriptionAttribute d = (DescriptionAttribute)pr[i].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                if (d != null)
                {
                    n = d.Description;
                }
                if (i + 1 == pr.Length)
                {
                    sb.Append(n + ":-> " + pr[i].GetValue(this, null));
                }
                else sb.AppendLine(n + " :-> " + pr[i].GetValue(this, null));

            }
            return sb.ToString();
        }

        public void Demo()
        {
            Card = 1;
            PersonID = 1234;
            Account = 1234;
            Kind = (TransactType)10;
            Summa = -1.00m;
            Restaurant = 1;
            RKDate = DateTime.Now;
            RKUni = 1;
            RKCheck = 321;
            VatSumA = 10;
            VatPrcA = 1;
            VatSumB = 20;
            VatPrcB = 2;
            VatSumC = 30;
            VatPrcC = 3;
            VatSumD = 40;
            VatPrcD = 4;
            VatSumE = 50;
            VatPrcE = 5;
            VatSumF = 60;
            VatPrcF = 6;
            VatSumG = 70;
            VatPrcG = 7;
            VatSumH = 80;
            VatPrcH = 8;

        }
    };
}
