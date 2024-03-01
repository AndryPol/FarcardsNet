using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract.Data.Farcard6
{
    /// <summary>
    /// Информация о транзакции для применения на сервере
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 122)]
    public class TransactionInfoEx
    {
        private readonly UInt16 _size = 122;
        public UInt16 Sieze
        {
            get { return 0; } 
            set { }
        }
        /// <summary>
        /// Карта
        /// </summary>
        [Description("Карта")]
        public long Card
        {
            get { return _lCard; }
            set { _lCard = value; }
        }

        private long _lCard;

        /// <summary>
        /// Идентификатор владельца карты
        /// </summary>
        [Description("Идентификатор владельца карты")]
        public long PersonID
        {
            get { return _lPersonId; }
            set { _lPersonId = value; }
        }

        private long _lPersonId;

        /// <summary>
        /// Номер счета
        /// </summary>
        [Description("Номер счета")]
        public UInt32 Account
        {
            get { return _dwAccount; }
            set { _dwAccount = value; }
        }

        private UInt32 _dwAccount;

        /// <summary>
        /// Тип транзакции 0 - платеж(снятие денег со счета) 1 - скидка 2 - бонус(начисление денег на счет)3 - потраты гостя(сколько заплатил своих денег)
        /// </summary>
        [Description("Тип транзакции")]
        public TransactType Kind
        {
            get { return _bKind; }
            set { _bKind = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private TransactType _bKind;

        
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
            get { return _lSumma / 100.00m; }
            set { _lSumma = Convert.ToInt64(value * 100); }
        }

        private Int64 _lSumma;

        /// <summary>
        /// код ресторана
        /// </summary>
        [Description("код ресторана")]
        public UInt16 Restaurant
        {
            get { return _wRestaurant; }
            set { _wRestaurant = value; }
        }

        private UInt16 _wRestaurant;

        /// <summary>
        /// кассовая дата(0 -> 30 / 12 / 1899)
        /// </summary>
        [Description("кассовая дата")]
        public DateTime RKDate
        {
            get { return new DateTime(1899, 12, 30).AddDays(_dwRKDate); }
            set { _dwRKDate = (uint)(value - new DateTime(1899, 12, 30)).Days; }
        }

        private UInt32 _dwRKDate;

        /// <summary>
        ///  номер кассы
        /// </summary>
        [Description("номер кассы")]
        public Byte RKUnit
        {
            get { return _bRkUnit; }
            set { _bRkUnit = value; }
        }

        private byte _bRkUnit;

        /// <summary>
        ///  номер чека
        /// </summary>
        [Description("номер чека")]
        public UInt32 RKCheck
        {
            get { return _dwRkCheck; }
            set { _dwRkCheck = value; }
        }

        private UInt32 _dwRkCheck;

        /// <summary>
        /// сумма с налогом A
        /// </summary>
        [Description("сумма с налогом A")]
        public Decimal VatSumA
        {
            get { return _lVatSumA / 100.00m; }
            set { _lVatSumA = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumA;

        /// <summary>
        /// размер налога A в процентах  15.00%)
        /// </summary>
        [Description("размер налога A")]
        public Decimal VatPrcA
        {
            get { return _wVatPrcA / 100.00m; }
            set { _wVatPrcA = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcA;

        /// <summary>
        /// сумма с налогом B
        /// </summary>
        [Description("сумма с налогом B")]
        public Decimal VatSumB
        {
            get { return _lVatSumB / 100.00m; }
            set { _lVatSumB = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumB;

        /// <summary>
        ///  размер налога B в процентах * 100
        /// </summary>
        [Description("размер налога B")]
        public Decimal VatPrcB
        {
            get { return _wVatPrcB / 100.00m; }
            set { _wVatPrcB = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcB;

        /// <summary>
        ///  сумма с налогом C
        /// </summary>
        [Description("сумма с налогом C")]
        public Decimal VatSumC
        {
            get { return _lVatSumC / 100.00m; }
            set { _lVatSumC = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumC;

        /// <summary>
        /// размер налога C в процентах * 100
        /// </summary>
        [Description("размер налога C")]
        public Decimal VatPrcC
        {
            get { return _wVatPrcC / 100.00m; }
            set { _wVatPrcC = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcC;

        /// <summary>
        ///  сумма с налогом D
        /// </summary>
        [Description("сумма с налогом D")]
        public Decimal VatSumD
        {
            get { return _lVatSumD / 100.00m; }
            set { _lVatSumD = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumD;

        /// <summary>
        ///  размер налога D в процентах * 100
        /// </summary>
        [Description("размер налога D")]
        public Decimal VatPrcD
        {
            get { return _wVatPrcD / 100.00m; }
            set { _wVatPrcD = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcD;

        /// <summary>
        ///  сумма с налогом E
        /// </summary>
        [Description("сумма с налогом E")]
        public Decimal VatSumE
        {
            get { return _lVatSumE / 100.00m; }
            set { _lVatSumE = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumE;

        /// <summary>
        ///  размер налога E в процентах * 100
        /// </summary>
        [Description("размер налога E")]
        public Decimal VatPrcE
        {
            get { return _wVatPrcE / 100.00m; }
            set { _wVatPrcE = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcE;

        /// <summary>
        /// сумма с налогом F
        /// </summary>

        [Description("сумма с налогом F")]
        public Decimal VatSumF
        {
            get { return _lVatSumF / 100.00m; }
            set { _lVatSumF = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumF;

        /// <summary>
        ///  размер налога F в процентах * 100
        /// </summary>
        [Description("размер налога F")]
        public Decimal VatPrcF
        {
            get { return _wVatPrcF / 100.00m; }
            set { _wVatPrcF = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcF;

        /// <summary>
        ///  сумма с налогом G
        /// </summary>
        [Description("сумма с налогом G")]
        public Decimal VatSumG
        {
            get { return _lVatSumG / 100.00m; }
            set { _lVatSumG = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumG;

        /// <summary>
        /// размер налога G в процентах * 100
        /// </summary>
        [Description("размер налога G")]
        public Decimal VatPrcG
        {
            get { return _wVatPrcG / 100.00m; }
            set { _wVatPrcG = Convert.ToUInt16(value * 100); }
            }

        private UInt16 _wVatPrcG;

        /// <summary>
        /// сумма с налогом H
        /// </summary>
        [Description("сумма с налогом H")]
        public Decimal VatSumH
        {
            get { return _lVatSumH / 100.00m; }
            set { _lVatSumH = Convert.ToInt64(value * 100); }
            }

        private Int64 _lVatSumH;

        /// <summary>
        /// размер налога H в процентах * 100
        /// </summary>
        [Description("размер налога H")]
        public Decimal VatPrcH
        {
            get { return _wVatPrcH / 100.00m; }
            set { _wVatPrcH = Convert.ToUInt16(value * 100); }
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
            _bRkUnit = 0;
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
    };

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
