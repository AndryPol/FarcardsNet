#include "pch.h"

#include "FarcardNet.h"
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
			_logger->Info("Library: " + settings->Library);
			if (!String::IsNullOrWhiteSpace(settings->Library))
			{
				file_info = gcnew FileInfo(settings->Library);
				_logger->Info("File path: " + file_info->FullName);
			}
			_logger->Info("Load Plugin");
			_farcard6 = gcroot<IFarcards6^>();
			_farcard6 = Farcards6Factory().GetProcessor(file_info);

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin: " + _farcard6->GetType()->FullName + " loaded");

				_logger->Info("Plugin Init Begin");
				_farcard6->Init();
				_logger->Info("Plugin Init Complete");
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
			_logger->Info("Done Begin");
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin Done Begin");
				_farcard6->Done();
				_logger->Info("Plugin Done Complete");
			}
			_logger->Info("Done Complete");
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
			_logger->Info("GetCardInfoEx Begin card: " + card + " rest: " + restaurant + " unit: " + unitNo);
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				CardInfoEx^ cardInfo = gcnew CardInfoEx();

				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);

				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
				_logger->Info("CardInfoEx Before Invoke: " + cardInfo->ToStringLog());
				if (inpBuf->Length > 0)
				{
					_logger->Info("InpBuf: " + Text::Encoding::UTF8->GetString(inpBuf));
				}
				_logger->Info("Plugin GetCardInfoEx Invoke");
				res = _farcard6->GetCardInfoEx(card, restaurant, unitNo, cardInfo, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin GetCardInfoEx Invoke Complete result: " + res);

				_logger->Info("CardInfoEx After Invoke: " + cardInfo->ToStringLog());

				if (res == 0)
				{
					_logger->Info("Save CardInfoEx to native address:" + info.ToInt32());
					Marshal::StructureToPtr(cardInfo, info, false);
				}

				if (!Object::ReferenceEquals(outBuf, nullptr) && outBuf->Length > 0)
				{
					_logger->Info("OutBuf: " + Text::Encoding::UTF8->GetString(outBuf));
				}

				outLen = !Object::ReferenceEquals(outBuf, nullptr) ? outBuf->Length : 0;
				_logger->Info("GetCardInfoEx Complete Result: " + res);
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
			_logger->Info("TransactionEx begin");
			if (!Object::ReferenceEquals(_farcard6, nullptr)) {

				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);

				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);

				_logger->Info("InpBuf" + Text::Encoding::UTF8->GetString(inpBuf));

				List<TransactionInfoEx^>% listTr = List<TransactionInfoEx^>();
				_logger->Info("Copy native: " + pList.ToInt32() + " TransactionExList to object, count: " + count);
				for (UInt32 i = 0; i < count; i++)
				{
					int size = IntPtr::Size;
					IntPtr address = safe_cast<IntPtr>(
						Marshal::ReadInt32(pList, i * IntPtr::Size));

					_logger->Info("Native address: " + address.ToInt32() + " of item transaction:" + i);

					TransactionInfoEx^ info =
						safe_cast<TransactionInfoEx^>(
							Marshal::PtrToStructure(address
								,
								TransactionInfoEx::typeid)
							);

					listTr.Add(info);
					_logger->Info("TransactionEx Item: " + i + " " + info->ToStringLog());
				}
				_logger->Info("Plugin TransactionEx Begin");
				res = _farcard6->TransactionsEx(% listTr, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin TransactionEx Complete Result: " + res);

				if (!Object::ReferenceEquals(outBuf, nullptr) && outBuf->Length > 0)
				{
					_logger->Info("OutBuf: " + Text::Encoding::UTF8->GetString(outBuf));
				}

				outLen = !Object::ReferenceEquals(outBuf, nullptr) ? outBuf->Length : 0;
				_logger->Info("TransactionEx Complete Result: " + res);
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
			_logger->Info("GetCardImage card:" + card);
			if (!Object::ReferenceEquals(_farcard6, nullptr)) {

				TextInfo^ info = gcnew TextInfo();
				_logger->Info("Plugin GetCardImageEx Before Card: " + card);
				_logger->Info("Info before" + info->ToStringLog());
				res = _farcard6->GetCardImageEx(card, info);
				_logger->Info("Plugin GetCardImageEx Complete Result: " + res);
				_logger->Info("Info before" + info->ToStringLog());

				if (res == 0)
				{
					_logger->Info("Copy Info to native address: " + pInfo.ToInt32());
					Marshal::StructureToPtr(info, pInfo, false);
				}
			}
			_logger->Info("GetCardImageEx Complete");
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
			_logger->Info("AnyInfo Begin");
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
				Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
				if (inpBuf->Length > 0)
				{
					_logger->Info("InpBuf: " + Text::Encoding::UTF8->GetString(inpBuf));
				}
				_logger->Info("Plugin AnyInfo begin");
				_farcard6->AnyInfo(inpBuf, outBuf);
				_logger->Info("Plugin AnyInfo Complete");

				if (!Object::ReferenceEquals(outBuf, nullptr) && outBuf->Length > 0)
				{
					_logger->Info("OutBuf: " + Text::Encoding::UTF8->GetString(outBuf));
				}

				outLen = !Object::ReferenceEquals(outBuf, nullptr) ? outBuf->Length : 0;
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
			_logger->Info("GetDiscLevelInfo begin account: " + account);
			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				DiscLevelInfo^ info = gcnew  DiscLevelInfo();
				_logger->Info("DiscLevelInfo Before Invoke: " + info->ToStringLog());

				_logger->Info("Plugin GetDiscLevelInfo begin");
				res = _farcard6->GetDiscLevelInfoL(account, info);
				_logger->Info("Plugin GetDiscLevelInfo Complete result:" + res);

				_logger->Info("DiscLevelInfo After Invoke : " + info->ToStringLog());

				if (res == 0)
				{

					Marshal::StructureToPtr(info, pInfo, false);
				}
			}
			_logger->Info("GetDiscLevelInfo Complete result:" + res);
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
