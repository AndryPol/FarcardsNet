#ifndef FARCARDNET
#define FARCARDNET


#include "Stdafx.H"
#include <msclr\all.h>
#pragma once
namespace FarcardNet {


	using namespace msclr;
	using namespace System;
	using namespace FarcardContract;
	using namespace FarcardContract::Data;
	using namespace FarcardContract::Data::Farcard6;
	using namespace System::Runtime::InteropServices;
	using System::Runtime::InteropServices::MarshalAsAttribute;
	using System::Runtime::InteropServices::InAttribute;


	void Init();

	void Done();

	int GetCardInfoEx(
		Int64 card, UInt32 restaurant,
		UInt32 unitNo, IntPtr info,
		IntPtr pInpBuf, UInt32 inpLen,
		BuffKind inpKind, [Out]array<Byte>^% outBuf,
		[Out]UInt32% outLen, [Out]BuffKind% outKind);

	int TransactionsEx(
		UInt32 count, IntPtr list,
		IntPtr pInpBuf, UInt32 inpLen,
		BuffKind inpKind, [Out]array<Byte>^% outBuf,
		[Out]UInt32% outLen, [Out]BuffKind% outKind);

	int GetCardImageEx(
		Int64 card,
		IntPtr pInfo);

	int FindEmail(
		[MarshalAs(UnmanagedType::LPStr)]
	String^ email,
		IntPtr pInfo);

	void FindCardsL(
		[MarshalAs(UnmanagedType::LPStr)]
	String^ findText,
		[MarshalAs(UnmanagedType::FunctionPtr)]
	CBFind^ cbFind,
		IntPtr backPtr);

	void FindAccountsByKind(
		FindKind kind,
		[MarshalAs(UnmanagedType::LPStr)]
	String^ findText,
		[MarshalAs(UnmanagedType::FunctionPtr)]
	CBFind^ cbFind,
		IntPtr backPtr);

	void AnyInfo(
		IntPtr pInpBuf,
		UInt32 inpLen,
		[Out]array<Byte>^% outBuf,
		[Out]UInt32% outLen);

	int GetDiscLevelInfoL(
		UInt32 account,
		IntPtr pInfo);
}
#endif
