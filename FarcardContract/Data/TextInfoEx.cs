using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FarcardContract.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 258)]
    public class TextInfoEx
    {
        private readonly UInt16 size = 259;

        public UInt16 Sieze { get { return 0; } }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = (value == null) ? string.Empty :
                    (value.Length < 255) ? value :
                    value.Substring(0, 255);
            }
        }

        private readonly byte endChar = 0;

        public TextInfoEx()
        {
            text = string.Empty;
        }

        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TextInfoEx {");
            sb.AppendLine($"Text->{Text}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
