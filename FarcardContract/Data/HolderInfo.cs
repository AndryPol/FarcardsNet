﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 54)]
    public class HolderInfo
    {   
        //Word размер структуры = 54 байт(это поле заполнено при вызове)
        private readonly UInt16 size = 54;
        public UInt16 Sieze { get { return 0; } set { } }
        //DWORD Номер счета
        /// <summary>
        /// Номер счета
        /// </summary>
        [Description("Номер счета")]
        public UInt32 ClientId { get { return dwClientId; } set { dwClientId = value; } }
        
        private UInt32 dwClientId;

        //Int64       
        /// <summary>
        /// Номер карты
        /// </summary>
        [Description("Номер Карты")]
        public long Card { get { return lCard; } set { lCard = value; } }
        
        private long lCard;


        /// <summary>
        ///40 байт Asciiz строка - имя владельца карты
        /// </summary>
        [Category("CardInfoEx")]
        [DisplayName("Имя владельца карты")]
        [Description("Имя владельца карты")]
        public String Owner
        {
            get
            {
                return _owner;
            } 
            set
            {
                _owner = (value == null) ? string.Empty :
                    (value.Length < 39) ? value :
                    value.Substring(0, 39);
            }
        }
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string _owner;
        
        public HolderInfo()
        {
            dwClientId = 0;
            lCard = 0;
            _owner = string.Empty;
        }
      
        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("HolderInfo {");
            sb.AppendLine($"Holder->{Owner}");
            sb.AppendLine($"Account->{ClientId}");
            sb.AppendLine($"Card->{Card}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
