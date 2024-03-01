using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FarcardContract.Data.Farcard5
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 258)]
    public class MessageInfo
    {
        private UInt16 size = 259;

        public UInt16 Sieze { get { return 0; } }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string _message;

        private readonly byte _endByte = 0;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = (value == null) ? string.Empty :
                    (value.Length < 255) ? value :
                    value.Substring(0, 255);
            }
        }

        public MessageInfo()
        {
            _message = string.Empty;
        }

        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MessageInfo {");
            sb.AppendLine($"Message->{Message}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
