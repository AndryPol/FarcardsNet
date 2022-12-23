using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract
{
    [Export(typeof(IFarcards))]
    internal class FarcardAllDemo : IFarcards
    {

        public void Init_dll()
        {

        }

        public int GetCardInfoEx_dll(long Card, uint Restaurant, uint UnitNo, ref CardInfoEx CardInfo, Byte[] InpBuf, ushort InpKind,
           out string pOutBuf, out ushort OutKind)
        {
            pOutBuf = String.Empty;
            OutKind = 0;
            if (Card == 1)
            {
                CardInfo.Demo();
                var results = CardInfo.ToStringLog();

                return 0;
            }

            return 1;

        }

        public int TransactionsEx_dll(List<TransactionInfoEx> TransactionInfo, Byte[] InpBuf, ushort InpKind, out string OutBuf, out ushort OutKind)
        {




            OutBuf = string.Empty;
            OutKind = 0;
            return 0;
        }

        public int FindEmail_dll(string Email, ref HolderInfo OutInfo)
        {
            var size = Marshal.SizeOf(typeof(HolderInfo));
            OutInfo.Demo();
            return 0;
        }

        public void FindCardsL(string FindText, CBFind CbFind, IntPtr Back)
        {
            CbFind?.Invoke(Back, 1234, 1, "Тестовый Клиент");
        }

        public void Done_dll()
        {

        }

    }
}
