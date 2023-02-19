using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using System;
using System.Collections.Generic;

namespace FarcardContract.Farcard6
{
    public interface IFarcards6
    {
        void Init();

        void Done();

        int GetCardInfoEx(Int64 card,
              UInt32 restaurant,
              UInt32 unitNo,
              ref CardInfoEx cardInfo,
              Byte[] inpBuf,
              BuffKind inpKind,
              out byte[] outBuf,
              out BuffKind outKind);

        int TransactionsEx(List<TransactionInfoEx> transactionInfo,
            Byte[] inpBuf,
            BuffKind inpKind,
            out byte[] outBuf,
            out BuffKind outKind);

        int FindEmail(string email, ref HolderInfo holderInfo);

        void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr);

        void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr);

        void AnyInfo(byte[] inpBuf, out byte[] outBuf);

        int GetDiscLevelInfoL(UInt32 account, ref DiscLevelInfo info);

        int GetCardImageEx(long card, ref TextInfo info);


    }
}
