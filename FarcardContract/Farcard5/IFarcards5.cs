using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarcardContract.Data.Farcard5;

namespace FarcardContract.Farcard5
{
    public interface IFarcards5
    {
        void Init();
        void Done();

        int GetCardInfoL(UInt64 card,
            UInt32 restaurant,
            UInt32 unitNo,
            ref CardInfoL cardInfo);

        int TransactionL(UInt32 account,
            TransactionInfoL transactionInfo);

    }
}
