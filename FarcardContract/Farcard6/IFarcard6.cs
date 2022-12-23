using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarcardContract.Farcard6
{
    public interface IFarcards6
    {
        void Init_dll();
        void Done_dll();

        int GetCardInfoEx_dll(Int64 Card,
              UInt32 Restaurant,
              UInt32 UnitNo,
              ref CardInfoEx CardInfo,
              Byte[] InpBuf,
              UInt16 InpKind,
              out String OutBuf,
              out UInt16 OutKind);
        int TransactionsEx_dll(List<TransactionInfoEx> TransactionInfo,
            Byte[] InpBuf,
            UInt16 InpKind,
            out String OutBuf,
            out UInt16 OutKind);

        int FindEmail_dll(string Email, ref HolderInfo OutInfo);

        void FindCardsL(string FindText, CBFind CbFind, IntPtr Back);




    }
}
