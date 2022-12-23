using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static FarcardContract.Farcard6.CardInfoEx;

namespace FarcardContract.Farcard6
{
    public interface ICardInfoEx
    {
        UInt16 Sieze { get; set; }
        /// <summary>
        /// Карта существовала, но была удалена 0 - нет, и сейчас есть 1 - да, удалена
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Карта удалена")]
        [Description("Описывает была ли удалена существующая карта")] 
        YesNo Deleted { get; set; }
        
        /// <summary>
        /// Карту надо изъять //	0 - нет //	1 - да
        /// </summary>
        [DisplayName("Карту надо изъять")]
        [Category("CardInfoEx")]
        [Description("Описывает требуется забрать ли карту у клиента")]
        YesNo Grab { get; set; }
        
        /// <summary>
        /// Истек срок действия //	0 - нет	//	1 - да
        /// </summary>
        [DisplayName("Срок действия истек?")]
        [Category("CardInfoEx")]
        [Description("Описывает истек ли срок действия карты")] 
        YesNo StopDate { get; set; }
   
        /// <summary>
        /// Сейчас карта не действует 0 - нет, действует 1 - да, не действует
        /// </summary>
        [DisplayName("Карта не активна")]
        [Description("Описывает действует ли карта")]
        [Category("CardInfoEx")]
        bool Holy { get; set; }
      
        /// <summary>
        /// Нужно ли подтверждение менеджера//	0 - не нужно//	1 - нужно
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Подтверждение менеджера")]
        [Description("Описывает требуется ли подтверждение менеджера")]
        YesNo Manager { get; set; }
       
        /// <summary>
        /// Карта заблокирована//	0 - нет//	1 - да
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Карта заблокирована")]
        [Description("Описывает заблокирована ли карта")]
        YesNo Locked { get; set; }
        
        /// <summary>
        ///256 байт  причина блокировки карты - будет показана на кассе 
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Причина блокировки карты")]
        [Description("Описывает почему заблокирована ли карта")]
        String WhyLock { get; set; } 
        
        /// <summary>
        /// 40 байт имя владельца карты
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Имя владельца карты")]
        [Description("Имя владельца карты")]
        String Holder { get;set; }
       
        /// <summary>
        /// Идентификатор владельца карты
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Идентификатор владельца карты")]
        [Description("Идентификатор владельца карты")]
        long PersonID { get; set; }

        /// <summary>
        /// Номер счета/// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер счета")]
        [Description("Номер счета")]
        UInt32 Account { get; set; }
       
        /// <summary>
        /// тип неплательщика
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Тип неплательщика")]
        [Description("Тип неплательщика")] 
        UInt32 Unpay { get; set; }
     
        /// <summary>
        /// номер бонуса
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер бонуса")]
        [Description("Номер бонуса")]
        UInt16 Bonus { get; set; }
        
        /// <summary>
        /// номер скидки
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Номер скидки")]
        [Description("Номер скидки")]
        UInt16 Discount { get; set; }
     
        /// <summary>
        /// предельная сумма скидки, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Предельная сумма скидки")]
        [Description("Предельная сумма скидки")]
        decimal DiscLimit { get; set; }
       
        /// <summary>
        /// сумма, доступная для оплаты счета, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 1")]
        [Description("Сумма на карточном счете N 1")]
        decimal Summa { get; set; }
        
        //  / <summary>
        // / сумма на карточном счете N 2, в копейках
        // / </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 2")]
        [Description("Сумма на карточном счете N 2")]
        decimal Sum2 { get; set; }
    
        /// <summary>
        /// сумма на карточном счете N 3, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 3")]
        [Description("Сумма на карточном счете N 3")]
        decimal Sum3 { get; set; }

        /// <summary>
        /// сумма на карточном счете N 4, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 4")]
        [Description("Сумма на карточном счете N 4")]
        decimal Sum4 { get; set; }
       
        /// <summary>
        /// сумма на карточном счете N 5, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 5")]
        [Description("Сумма на карточном счете N 5")]
        decimal Sum5 { get; set; }

        /// <summary>
        /// сумма на карточном счете N 6, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 6")]
        [Description("Сумма на карточном счете N 6")]
        decimal Sum6 { get; set; }

        /// <summary>
        /// сумма на карточном счете N 7, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 7")]
        [Description("Сумма на карточном счете N 7")]
        decimal Sum7 { get; set; }

        /// <summary>
        /// сумма на карточном счете N 8, в копейках
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Сумма на карточном счете N 8")]
        [Description("Сумма на карточном счете N 8")]
        decimal Sum8 { get; set; }

        [Category("CardInfoEx")]
        [DisplayName("Суммы на счетах")]
        [Description("Суммы на счетах")]
        decimal this[int index] { get; set; }

        /// <summary>
        /// 256 байт произвольная информация о карте
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация о карте")]
        [Description("Произвольная информация о карте")]
        String DopInfo { get; set; }
        
        /// <summary>
        /// 256 байт информация для вывода на экран кассы
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация для вывода на экран кассы")]
        [Description("Произвольная информация для вывода на экран кассы")]
        String ShowInfo { get; set; }
       
        /// <summary>
        ///  256 байт информация для распечатки на принтере
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Произвольная информация для распечатки на принтере")]
        [Description("Произвольная информация для распечатки на принтере")]
        string ScrMessage { get; set; }


    }
    public enum YesNo : byte
    {
        No = 0, Yes = 1
    }
}
