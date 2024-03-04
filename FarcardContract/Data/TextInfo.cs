using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract.Data
{
    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi, Pack = 1,Size = 258)]
    public class TextInfo
    {
        private readonly UInt16 size = 258;

        public UInt16 Sieze { get { return 0;} }
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 256)]
        private string text;
        public string Text { get { return text;  }
            set
            {
                text = (value == null)? string.Empty:  
                (value.Length<255)? value:
                value.Substring(0,255);
            }
        }

        public TextInfo()
        {
            text = string.Empty;
        }

        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TextInfo {");
            sb.AppendLine($"Text->{Text}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
