using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace FarcardContract.Data.Farcard6
{


    [Category("CardInfoEx")]
    // [DisplayName("Инфо по карте")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 1164)]
    public class CardInfoEx
    {

        private readonly UInt16 size = 1164;

        public UInt16 Sieze
        {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Карта существовала, но была удалена 0 - нет, и сейчас есть 1 - да, удалена
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Карта удалена")]
        [Description("Описывает была ли удалена существующая карта")]
        public YesNo Deleted
        {
            get { return bDeleted; }
            set { bDeleted = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo bDeleted;

        /// <summary>
        /// Карту надо изъять //	0 - нет //	1 - да
        /// </summary>
        [DisplayName("Карту надо изъять")]
        [Category("CardInfoEx")]
        [Description("Описывает требуется забрать ли карту у клиента")]
        public YesNo Grab
        {
            get { return _bGrab; }
            set { _bGrab = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo _bGrab;

        /// <summary>
        /// Истек срок действия //	0 - нет	//	1 - да
        /// </summary>
        [DisplayName("Срок действия истек?")]
        [Category("CardInfoEx")]
        [Description("Описывает истек ли срок действия карты")]
        public YesNo StopDate
        {
            get { return bStopDate; }
            set { bStopDate = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo bStopDate;

        /// <summary>
        /// Сейчас карта не действует 0 - нет, действует 1 - да, не действует
        /// </summary>
        [DisplayName("Карта не активна")]
        [Description("Описывает действует ли карта")]
        [Category("CardInfoEx")]

        public YesNo Holy
        {
            get { return bHoly; }
            set { bHoly = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo bHoly;

        /// <summary>
        /// Нужно ли подтверждение менеджера//	0 - не нужно//	1 - нужно
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Подтверждение менеджера")]
        [Description("Описывает требуется ли подтверждение менеджера")]
        public YesNo Manager
        {
            get { return _bManager; }
            set { _bManager = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo _bManager;

        /// <summary>
        /// Карта заблокирована//	0 - нет//	1 - да
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Карта заблокирована")]
        [Description("Описывает заблокирована ли карта")]
        public YesNo Locked
        {
            get { return _bLocked; }
            set { _bLocked = value; }
        }

        [MarshalAs(UnmanagedType.U1)]
        private YesNo _bLocked;

        /// <summary>
        ///256 байт  причина блокировки карты - будет показана на кассе 
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Причина блокировки карты")]
        [Description("Описывает почему заблокирована ли карта")]
        public String WhyLock
        {
            get { return _whyLock; }
            set { _whyLock = value?.Length < 256 ? value : value?.Substring(0, 255); }
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string _whyLock;

        /// <summary>
        /// 40 байт имя владельца карты
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Имя владельца карты")]
        [Description("Имя владельца карты")]
        public String Holder
        {
            get { return _holder; }
            set { _holder = value?.Length < 40 ? value : value?.Substring(0, 39); }
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string _holder;

        /// <summary>
        /// Идентификатор владельца карты
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Идентификатор владельца карты")]
        [Description("Идентификатор владельца карты")]
        public long PersonID
        {
            get { return _lPersonId; }
            set { _lPersonId = value; }
        }

        private long _lPersonId;

        /// <summary>
        /// Номер счета/// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер счета")]
        [Description("Номер счета")]
        public UInt32 Account
        {
            get { return _dwAccount; }
            set { _dwAccount = value; }
        }

        private UInt32 _dwAccount;

        /// <summary>
        /// тип неплательщика
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Тип неплательщика")]
        [Description("Тип неплательщика")]
        public UInt32 Unpay
        {
            get { return _dwUnpay; }
            set { _dwUnpay = value; }
        }

        private UInt32 _dwUnpay;

        /// <summary>
        /// номер бонуса
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер бонуса")]
        [Description("Номер бонуса")]
        public UInt16 Bonus
        {
            get { return _wBonus; }
            set { _wBonus = value; }
        }

        private UInt16 _wBonus;

        /// <summary>
        /// номер скидки
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер скидки")]
        [Description("Номер скидки")]
        public UInt16 Discount
        {
            get { return _wDiscount; }
            set { _wDiscount = value; }
        }

        private UInt16 _wDiscount;


        /// <summary>
        /// предельная сумма скидки, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Предельная сумма скидки")]
        [Description("Предельная сумма скидки")]
        public decimal DiscLimit
        {
            get { return _lDiscLimit / 100.00m; }
            set { _lDiscLimit = Convert.ToInt64(value * 100); }
        }

        private long _lDiscLimit;

        /// <summary>
        /// сумма, доступная для оплаты счета, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 1")]
        [Description("Сумма на карточном счете N 1")]
        public decimal Summa
        {
            get { return _lSumma / 100.00m; }
            set { _lSumma = Convert.ToInt64(value * 100); }
        }

        private long _lSumma;

        //  / <summary>
        // / сумма на карточном счете N 2, в копейках
        // / </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 2")]
        [Description("Сумма на карточном счете N 2")]
        public decimal Sum2
        {
            get { return _lSum2 / 100.00m; }
            set { _lSum2 = Convert.ToInt64(value * 100); }
        }

        private long _lSum2;

        /// <summary>
        /// сумма на карточном счете N 3, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 3")]
        [Description("Сумма на карточном счете N 3")]
        public decimal Sum3
        {
            get { return _lSum3 / 100.00m; }
            set { _lSum3 = Convert.ToInt64(value * 100); }
        }

        private long _lSum3;

        /// <summary>
        /// сумма на карточном счете N 4, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 4")]
        [Description("Сумма на карточном счете N 4")]
        public decimal Sum4
        {
            get { return _lSum4 / 100.00m; }
            set { _lSum4 = Convert.ToInt64(value * 100); }
        }

        private long _lSum4;

        /// <summary>
        /// сумма на карточном счете N 5, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 5")]
        [Description("Сумма на карточном счете N 5")]
        public decimal Sum5
        {
            get { return _lSum5 / 100.00m; }
            set { _lSum5 = Convert.ToInt64(value * 100); }
        }

        private long _lSum5;

        /// <summary>
        /// сумма на карточном счете N 6, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 6")]
        [Description("Сумма на карточном счете N 6")]
        public decimal Sum6
        {
            get { return _lSum6 / 100.00m; }
            set { _lSum6 = Convert.ToInt64(value * 100); }
        }

        private long _lSum6;

        /// <summary>
        /// сумма на карточном счете N 7, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 7")]
        [Description("Сумма на карточном счете N 7")]
        public decimal Sum7
        {
            get { return _lSum7 / 100.00m; }
            set { _lSum7 = Convert.ToInt64(value * 100); }
        }

        private long _lSum7;

        /// <summary>
        /// сумма на карточном счете N 8, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 8")]
        [Description("Сумма на карточном счете N 8")]
        public decimal Sum8
        {
            get { return _lSum8 / 100.00m; }
            set { _lSum8 = Convert.ToInt64(value * 100); }
        }

        private long _lSum8;

        public decimal this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Summa;
                    case 1: return Sum2;
                    case 2: return Sum3;
                    case 3: return Sum4;
                    case 4: return Sum5;
                    case 5: return Sum6;
                    case 6: return Sum7;
                    case 7: return Sum8;
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Summa = value; break;
                    case 1: Sum2 = value; break;
                    case 2: Sum3 = value; break;
                    case 3: Sum4 = value; break;
                    case 4: Sum5 = value; break;
                    case 5: Sum6 = value; break;
                    case 6: Sum7 = value; break;
                    case 7: Sum8 = value; break;
                }
            }
        }

        /// <summary>
        /// 256 байт произвольная информация о карте
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация о карте")]
        [Description("Произвольная информация о карте")]
        public String DopInfo { get { return _dopInfo; } set { _dopInfo = value?.Length < 256 ? value : value?.Substring(0, 255); } }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string _dopInfo;

        /// <summary>
        /// 256 байт информация для вывода на экран кассы
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация для вывода на экран кассы")]
        [Description("Произвольная информация для вывода на экран кассы")]
        public String ScrMessage { get { return _scrMessage; } set { _scrMessage = value?.Length < 256 ? value : value?.Substring(0, 255); } }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string _scrMessage;

        /// <summary>
        ///  256 байт информация для распечатки на принтере
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация для распечатки на принтере")]
        [Description("Произвольная информация для распечатки на принтере")]
        public string PrnMessage { get { return _prnMessage; } set { _prnMessage = value?.Length < 256 ? value : value?.Substring(0, 255); } }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string _prnMessage;

        public CardInfoEx()
        {
            Deleted = 0;
            Grab = 0;
            StopDate = 0;
            Holy = YesNo.No;
            Manager = 0;
            Locked = 0;
            WhyLock = "";
            Holder = "";
            PersonID = 0;
            Account = 0;
            Unpay = 0;
            Bonus = 0;
            Discount = 0;
            DiscLimit = 0;
            Summa = 0;
            Sum2 = 0;
            Sum3 = 0;
            Sum4 = 0;
            Sum5 = 0;
            Sum6 = 0;
            Sum7 = 0;
            Sum8 = 0;
            DopInfo = "";
            PrnMessage = "";
            ScrMessage = "";
        }


        /// <summary>
        /// вывод класса Cardinfo в строку 
        /// </summary>
        /// <returns>строка описывающая значения.</returns>
        public string ToStringLog()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Информация по карте: {");
            builder.AppendLine("Карта-> " + (Deleted == YesNo.Yes ? "была удалена" : "не удалена"));
            builder.AppendLine("Карту-> : " + (Grab == YesNo.Yes ? "надо изъять" : "не нужно изымать"));
            builder.AppendLine("Cрок действия карты-> " + (StopDate == YesNo.Yes ? "истек" : "не истек"));
            builder.AppendLine("Карта-> " + (Holy == YesNo.Yes ? "не действует" : "действует"));
            builder.AppendLine("Подтверждение менеджера-> " + (Manager == YesNo.Yes ? "нужно" : "не нужно"));
            builder.AppendLine("Карта-> " + (Locked == YesNo.Yes ? "заблокирована" : "не заблокирована"));
            builder.AppendLine("Причина блокировки карты:-> " + WhyLock);
            builder.AppendLine("Фио:-> " + (Holder));
            builder.AppendLine("Идентификатор владельца:-> " + PersonID);
            builder.AppendLine("Номер счета:-> " + Account);
            builder.AppendLine("Тип неплательщика:-> " + Unpay);
            builder.AppendLine("Номер бонуса:-> " + Bonus);
            builder.AppendLine("Номер скидки:-> " + Discount);
            builder.AppendLine("Максимальная сумма скидки:-> " + (DiscLimit.ToString("N")));
            for (int i = 0; i < 8; i++)
            {
                builder.AppendLine($"Доступная сумма по субсчету {i + 1}:-> " + this[i].ToString("N"));
            }
            builder.AppendLine("Информация по карте:-> " + DopInfo);
            builder.AppendLine("Информация для вывода на экран:-> " + ScrMessage);
            builder.AppendLine("Информация для печати на чеке:-> " + PrnMessage);
            builder.Append("}");
            return builder.ToString();
        }
    };

    public enum YesNo : byte
    {
        [XmlEnum("0")]
        No = 0,
        [XmlEnum("1")]
        Yes = 1
    }
}
