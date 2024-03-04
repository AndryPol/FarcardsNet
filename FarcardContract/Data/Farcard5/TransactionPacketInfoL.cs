using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FarcardContract.Data.Farcard5
{
    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi,Pack = 1)]
    public class TransactionPacketInfoL
    {
        private UInt16 _dwAccount;

        
        private TransactionInfoL _info;

        /// <summary>
        /// Номер счета
        /// </summary>
        public UInt16 Account
        {
            get { return _dwAccount; }
            set { _dwAccount = value; }
        }

        /// <summary>
        /// Транзакция 
        /// </summary>
        public TransactionInfoL Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public TransactionPacketInfoL()
        {
            _dwAccount = 0;
            _info = new TransactionInfoL();
        }

        public string ToStringLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TransactionPacketInfoL {");
            sb.AppendLine($"Account->{Account}");
            sb.Append($"Info->{ (Info != null ?Info.ToStringLog():"Empty")}");
            sb.Append("}");
            return sb.ToString();
        }

    }
}
