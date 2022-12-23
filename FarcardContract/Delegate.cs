using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FarcardContract.Farcard6;

namespace FarcardContract
{
    public delegate void CBFind(
        IntPtr Back,
        UInt32 Account,
        Int64 Card,
        [MarshalAs(UnmanagedType.LPStr)] string Holder
        );

    public delegate int GetCardInfoEx(
        Int64 Card,
        UInt32 Restaurant,
        UInt32 UnitNo,
        ref CardInfoEx Info,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 5)]
        byte[] InpBuf,
        UInt32 InpLen,
        UInt16 InpKind,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 8)]
        [Out]out byte[] OutBuf,
        [Out]out Int32 OutLen,
        [Out]out UInt16 OutKind
        );

public delegate int TransactionsEx(
    UInt32 Count,
    IntPtr info,
    IntPtr InpBuf,
    UInt32 InpLen,
    UInt16 InpKind,
    [MarshalAs(UnmanagedType.LPStr)] out String OutBuf,
    out Int32 OutLen,
    out UInt16 OutKind
    );


public delegate void Init();

public delegate void Done();
}
