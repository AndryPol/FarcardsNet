using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 90)]
    public class DiscLevelInfo
    {
        private readonly UInt16 size = 90;

        public UInt16 Sieze
        {
            get { return 0; }
            set { }
        }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string _currName;
        /// <summary>
        /// Название текущего дисконтного уровня
        /// </summary>
        [DisplayName("Название текущего дисконтного уровня")]
        [Description("Название текущего дисконтного уровня")]
        public string CurrentName
        {
            get { return _currName; }
            set
            {
                _currName = (value == null) ? string.Empty :
                    (value.Length < 39) ? value :
                    value.Substring(0, 39);
            }
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string _nextName;

        /// <summary>
        /// Название cледующего дисконтного уровня
        /// </summary>
        [DisplayName("Название cледующего дисконтного уровня")]
        [Description("Название cледующего дисконтного уровня")]
        public string NextName
        {
            get { return _nextName; }
            set
            {
                _nextName = (value == null) ? string.Empty :
                    (value.Length < 39) ? value :
                    value.Substring(0, 39);
            }
        }

        private Int64 _sumToNext;
        /// <summary>
        /// Сумма, необходимая для перехода на следующий уровень
        /// </summary>
        [DisplayName("Cумма, для перехода на следующий уровень")]
        [Description("Cумма, для перехода на следующий уровень")]
        public Decimal SumToNextLevel
        {
            get { return _sumToNext / 100.00m; }
            set { _sumToNext = Convert.ToInt64(value * 100); }
        }

        public DiscLevelInfo()
        {
            _currName = string.Empty;
            _nextName = string.Empty;
            _sumToNext = 0;
        }

        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DiscLevelInfo {");
            sb.AppendLine($"Текущий уровень: {CurrentName}");
            sb.AppendLine($"Следующий уровень {NextName}");
            sb.AppendLine($"Сумма для перехода {SumToNextLevel}");
            sb.AppendLine("}");

            return sb.ToString();
        }

    }
}
