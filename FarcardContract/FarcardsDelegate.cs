using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using System;
using System.Runtime.InteropServices;

namespace FarcardContract
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void CBFind(
        IntPtr backPtr,
        UInt32 account,
        Int64 card,
        [MarshalAs(UnmanagedType.LPStr)]
        String owner
        );

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate int GetCardInfoEx(
        [In] Int64 card,
        [In] UInt32 restaurant,
        [In] UInt32 unitNo,
        [In, Out]
        CardInfoEx cardInfo,
        [In,MarshalAs(UnmanagedType.LPArray,
             ArraySubType = UnmanagedType.I1,
             SizeParamIndex = 5)]
        Byte[] inpBuf,
        [In] UInt32 inpLen,
        [In] BuffKind inpKind,
        [Out] out IntPtr outBuf,
        [Out] out UInt32 outLen,
        [Out] out BuffKind outKind
        );

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate int TransactionsEx(
         UInt32 count,
        IntPtr info,
        [In,MarshalAs(UnmanagedType.LPArray,
             ArraySubType = UnmanagedType.I1,
             SizeParamIndex = 3)]
        Byte[] inpBuf,
         UInt32 inpLen,
         BuffKind inpKind,
         [Out] out IntPtr outBuf,
         [Out] out UInt32 outLen,
         [Out] out BuffKind outKind
        );

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate int FindEmail(
        [MarshalAs(UnmanagedType.LPStr)]
        String email,
        [In,Out]
        HolderInfo outInfo);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void FindCardsL(
        [MarshalAs(UnmanagedType.LPStr)]
        String findText,
        [MarshalAs(UnmanagedType.FunctionPtr)]
        CBFind cbFind,
        IntPtr backPtr);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void FindAccountsByKind(
        FindKind kind,
        [MarshalAs(UnmanagedType.LPStr)]
        String findText,
        [MarshalAs(UnmanagedType.FunctionPtr)]
        CBFind cbFind,
        IntPtr backPtr);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate int GetCardImageEx(
        Int64 card,
        [In, Out]
        TextInfo info);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void AnyInfo(
        [In,MarshalAs(UnmanagedType.LPArray,
             ArraySubType = UnmanagedType.I1,
             SizeParamIndex = 2)]
        Byte[] inpBuf,
        UInt32 inpLen,
        out IntPtr outBuf,
        out UInt32 outLen);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate int GetDiscLevelInfoL(
        UInt32 account,
        [In, Out]
        DiscLevelInfo info);

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void Init();

    [UnmanagedFunctionPointer(CallingConvention.StdCall,
        CharSet = CharSet.Ansi,
        SetLastError = true)]
    public delegate void Done();

}
