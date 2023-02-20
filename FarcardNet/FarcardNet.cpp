#include "pch.h";

#include "FarcardNet.h";
#include <msclr/marshal_cppstd.h>

#include "FarcardNetSettings.h"

namespace FarcardNet {


	using namespace ComponentModel;
	using namespace FarcardContract::Farcard6;
	using namespace Collections::Generic;
	using namespace IO;

	ref class FarcardNet {};

	gcroot<Logger<FarcardNet^>^> _logger;
	gcroot<IFarcards6^> _farcard6;

	void Init()
	{
		try {
			FarcardNetSettings^ settings = FarcardNetSettings::GetSettings(nullptr);
			_logger = gcroot<Logger<FarcardNet^>^>();
			_logger = gcnew  Logger<FarcardNet^>(settings->LogLevel, false);

			FileInfo^ file_info = nullptr;
			if (!String::IsNullOrWhiteSpace(settings->Library))
				file_info = gcnew FileInfo(settings->Library);

			_farcard6 = gcroot<IFarcards6^>();
			_farcard6 = Farcards6Fabric().GetProcessor(file_info);
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_farcard6->Init();
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
			Done();

			std::string message = msclr::interop::marshal_as<std::string>(ex->ToString());
			throw std::exception(message.c_str());
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
			Done();

			std::string message = msclr::interop::marshal_as<std::string>(ex.ToString());
			throw std::exception(message.c_str(), code);
		}
	}

	void Done()
	{
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
				_farcard6->Done();
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
	}

	int GetCardInfoEx(Int64 card, UInt32 restaurant, UInt32 unitNo, IntPtr info, IntPtr pInpBuf, UInt32 inpLen,
		BuffKind inpKind, array<Byte>^% outBuf, UInt32% outLen, BuffKind% outKind)
	{
		int res = 1;
		try {
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				CardInfoEx^ cardInfo = gcnew CardInfoEx();
				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
				res = _farcard6->GetCardInfoEx(card, restaurant, unitNo, cardInfo, inpBuf, inpKind, outBuf, outKind);
				if (res == 0)
				{
					Marshal::StructureToPtr(cardInfo, info, false);
				}

				outLen = outBuf != nullptr ? outBuf->Length : 0;
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
		return  res;
	}



	int TransactionsEx(UInt32 count, IntPtr pList, IntPtr pInpBuf, UInt32 inpLen, BuffKind inpKind,
		array<Byte>^% outBuf, UInt32& outLen, BuffKind& outKind)
	{
		int res = 1;
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr)) {
				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
				List<TransactionInfoEx^>% listTr = List<TransactionInfoEx^>();

				for (int i = 0; i < count; i++)
				{
					int size = IntPtr::Size;
					TransactionInfoEx^ info =
						(TransactionInfoEx^)Marshal::PtrToStructure((IntPtr)Marshal::ReadInt32(pList, i * IntPtr::Size),
							TransactionInfoEx::typeid);
					listTr.Add(info);
				}

				res = _farcard6->TransactionsEx(% listTr, inpBuf, (BuffKind)0, outBuf, outKind);
				outLen = outBuf != nullptr ? outBuf->Length : 0;
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
		return res;
	}

	int GetCardImageEx(Int64 card, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr)) {
				TextInfo^ info = gcnew TextInfo();
				res = _farcard6->GetCardImageEx(card, info);
				if (res == 0)
				{
					Marshal::StructureToPtr(info, pInfo, false);
				}
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
		return res;
	}

	int FindEmail([MarshalAs(UnmanagedType::LPStr)]String^ email, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				HolderInfo^ info = gcnew  HolderInfo();
				res = _farcard6->FindEmail(email, info);
				if (res == 0)
					Marshal::StructureToPtr(info, pInfo, false);
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
		return res;
	}

	void FindCardsL(
		[MarshalAs(UnmanagedType::LPStr)]
	String^ findText,
		[MarshalAs(UnmanagedType::FunctionPtr)]
	CBFind^ cbFind,
		IntPtr backPtr)
	{
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_farcard6->FindCardsL(findText, cbFind, backPtr);
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
	}

	void FindAccountsByKind(
		FindKind kind,
		[MarshalAs(UnmanagedType::LPStr)]
	String^ findText,
		[MarshalAs(UnmanagedType::FunctionPtr)]
	CBFind^ cbFind,
		IntPtr backPtr)
	{
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_farcard6->FindAccountsByKind(kind, findText, cbFind, backPtr);
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
	}

	void AnyInfo(IntPtr pInpBuf, UInt32 inpLen, array<Byte>^% outBuf, UInt32% outLen)
	{
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);

				_farcard6->AnyInfo(inpBuf, outBuf);

				outLen = outBuf != nullptr ? outBuf->Length : 0;
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
	}

	int GetDiscLevelInfoL(UInt32 account, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				DiscLevelInfo^ info = gcnew  DiscLevelInfo();
				res = _farcard6->GetDiscLevelInfoL(account, info);
				if (res == 0)
					Marshal::StructureToPtr(info, pInfo, false);
			}
		}
		catch (Exception^ ex)
		{
			_logger->Error(ex);
		}
		catch (...)
		{
			int code = GetLastError();
			Exception% ex = Win32Exception(code);
			_logger->Error(% ex);
		}
		return res;
	}
}
