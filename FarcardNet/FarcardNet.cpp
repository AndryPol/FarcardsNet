#include "pch.h"

#include "FarcardNet.h"
#include <msclr/marshal_cppstd.h>

#include "FarcardNetSettings.h"

namespace FarcardNet {


	using namespace ComponentModel;
	using namespace Composition;
	using namespace FarcardContract;
	using namespace FarcardContract::Farcard5;
	using namespace FarcardContract::Farcard6;
	using namespace FarcardContract::Data;
	using namespace FarcardContract::Data::Farcard5;
	using namespace FarcardContract::Data::Farcard6;
	using namespace Collections::Generic;
	using namespace IO;

	ref class FarcardNet {};
	enum FarcardType
	{
		Farcards5,
		Farcards6,
		FarcardsAll
	};

	gcroot<Logger<FarcardNet^>^> _logger = nullptr;
	gcroot<IFarcards6^> _farcard6 = nullptr;
	gcroot<IFarcards5^> _farcard5 = nullptr;
	gcroot<IFarcards^> _farcards = nullptr;

	FarcardType InitFactories(FarcardNetSettings^ settings)
	{
		FileInfo^ file_info = nullptr;
		_logger->Info("Library: " + settings->Library);
		if (!String::IsNullOrWhiteSpace(settings->Library))
		{
			file_info = gcnew FileInfo(settings->Library);
			_logger->Info("File path: " + file_info->FullName);
		}
		else
		{
			_logger->Info("Loads Demo Plugins");
		}
		_logger->Info("Load Plugin");

		try
		{
			_logger->Info("Load Plugin Farcars5");
			IFarcards5^ farcards5 = Farcards5Factory().GetProcessor(file_info);
			_logger->Info("Load Plugin Farcards5 Complete");
			_farcard5 = gcroot<IFarcards5^>(_farcard5);
			//_farcard5 = farcards5;
			return Farcards5;
		}
		catch (CompositionException^ ex)
		{
			_logger->Error(ex);
		}

		try
		{
			_logger->Info("Load Plugin Farcars6");
			IFarcards6^ farcards6 = Farcards6Factory().GetProcessor(file_info);
			_logger->Info("Load Plugin Farcards6 Complete");
			_farcard6 = gcroot<IFarcards6^>(farcards6);
			//_farcard6 = farcards6;
			return Farcards6;
		}
		catch (CompositionException^ ex)
		{
			_logger->Error(ex);
		}

		try
		{
			_logger->Info("Load Plugin FarcarsAll");
			IFarcards^ farcards = FarcardsAllFactory().GetProcessor(file_info);
			_logger->Info("Load Plugin FarcardsAll Complete");
			_farcards = gcroot<IFarcards^>(farcards);
		//	_farcards = farcards;
			return FarcardsAll;
		}
		catch (CompositionException^ ex)
		{
			_logger->Error(ex);
		}

		throw gcnew Exception("Farcards Plugin not Initialize");

	};

	void Init()
	{
		try {
			FarcardNetSettings^ settings = FarcardNetSettings::GetSettings(nullptr);
			_logger = gcroot<Logger<FarcardNet^>^>(gcnew  Logger<FarcardNet^>(settings->LogLevel, false));
			

			_logger->Info("Load Plugin");

			FarcardType initType = InitFactories(settings);

			switch (initType)
			{
			case Farcards5:
			{
				if (!Object::ReferenceEquals(_farcard5, nullptr))
				{
					_logger->Info("Plugin farcard5: " + _farcard5->GetType()->FullName + " loaded");

					_logger->Info("Plugin farcard5 Init Begin");
					_farcard5->Init();
					_logger->Info("Plugin farcard5 Init Complete");
				}
				break;
			}
			case Farcards6:
			{
				if (!Object::ReferenceEquals(_farcard6, nullptr))
				{
					_logger->Info("Plugin farcard6: " + _farcard6->GetType()->FullName + " loaded");

					_logger->Info("Plugin farcard6 Init Begin");
					_farcard6->Init();
					_logger->Info("Plugin farcard6 Init Complete");
				}
				break;
			}
			default:
			{
				if (!Object::ReferenceEquals(_farcards, nullptr))
				{
					_logger->Info("Plugin farcardsAll: " + _farcards->GetType()->FullName + " loaded");

					_logger->Info("Plugin farcardsAll Init Begin");
					safe_cast<IFarcards5^>(_farcards)->Init();
					_logger->Info("Plugin farcardsAll Init Complete");
				}
			}
			}

			if (Object::ReferenceEquals(_farcard5, nullptr) &&
				Object::ReferenceEquals(_farcard6, nullptr) &&
				Object::ReferenceEquals(_farcards, nullptr))
			{
				throw gcnew Exception("Plugin not initialize");
			}
			_logger->Info("Plugin type: "+ ((int)(initType)).ToString() + " Loaded Complete");
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
		if (!Object::ReferenceEquals(_farcard6, nullptr))
		{
			try
			{
				_logger->Info("Done farcards6 Begin");
				if (!Object::ReferenceEquals(_farcard6, nullptr))
				{
					_logger->Info("Plugin farcards6 Done Begin");
					_farcard6->Done();
					_logger->Info("Plugin farcards6 Done Complete");
				}
				_logger->Info("Done farcards6 Complete");
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

			try
			{
				_logger->Info("Dispose farcards6 Begin");
				if (!Object::ReferenceEquals(_farcard6, nullptr))
				{
					_logger->Info("Plugin farcards6 Dispose Begin");
					delete _farcard6;
					_farcard6 = nullptr;
					_logger->Info("Plugin farcards6 Dispose Complete");
				}
				_logger->Info("Dispose farcards6 Complete");
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
		if (!Object::ReferenceEquals(_farcard5, nullptr))
		{
			try
			{
				_logger->Info("Done farcards5 Begin");
				if (!Object::ReferenceEquals(_farcard5, nullptr))
				{
					_logger->Info("Plugin farcards5 Done Begin");
					_farcard5->Done();
					_logger->Info("Plugin farcards5 Done Complete");
				}
				_logger->Info("Done farcards5 Complete");
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
			try
			{
				_logger->Info("Dispose farcards5 Begin");
				if (!Object::ReferenceEquals(_farcard5, nullptr))
				{
					_logger->Info("Plugin farcards5 Dispose Begin");
					delete _farcard5;
					_farcard5 = nullptr;
					_logger->Info("Plugin farcards5 Dispose Complete");
				}
				_logger->Info("Dispose farcards5 Complete");
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
		if (!Object::ReferenceEquals(_farcards, nullptr))
		{
			try
			{
				_logger->Info("Done farcardsAll Begin");
				if (!Object::ReferenceEquals(_farcards, nullptr))
				{
					_logger->Info("Plugin farcardsAll Done Begin");
					safe_cast<IFarcards5^>(_farcards)->Done();
					_logger->Info("Plugin farcardsAll Done Complete");
				}
				_logger->Info("Done farcardsAll Complete");
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
			try
			{
				_logger->Info("Dispose farcardsAll Begin");
				if (!Object::ReferenceEquals(_farcards, nullptr))
				{
					_logger->Info("Plugin farcardsAll Dispose Begin");
					delete _farcards;
					_farcards = nullptr;
					_logger->Info("Plugin farcardsAll Dispose Complete");
				}
				_logger->Info("Dispose farcardsAll Complete");
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
	}

	int GetCardInfoEx(Int64 card, UInt32 restaurant, UInt32 unitNo, IntPtr info, IntPtr pInpBuf, UInt32 inpLen,
		BuffKind inpKind, array<Byte>^% outBuf, UInt32% outLen, BuffKind% outKind)
	{
		int res = 1;
		try {
			_logger->Info("GetCardInfoEx Begin card: " + card + " rest: " + restaurant + " unit: " + unitNo);

			CardInfoEx^ cardInfo = gcnew CardInfoEx();

			array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);

			Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
			_logger->Info("CardInfoEx Before Invoke: " + cardInfo->ToStringLog());
			if (inpBuf->Length > 0)
			{
				_logger->Info("InpBuf: " + Text::Encoding::UTF8->GetString(inpBuf));
			}

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin farcard6 GetCardInfoEx Invoke");
				res = _farcard6->GetCardInfoEx(card, restaurant, unitNo, cardInfo, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin farcard6 GetCardInfoEx Invoke Complete result: " + res);

			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardInfoEx Invoke");
				res = _farcards->GetCardInfoEx(card, restaurant, unitNo, cardInfo, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin farcardAll GetCardInfoEx Invoke Complete result: " + res);
			}
			else 
			{
				_logger->Error("No Plugin farcard GetCardInfoEx Invoke");
			}

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

	int GetCardInfoL(Int64 card, UInt32 restaurant, UInt32 unitNo, IntPtr info)
	{
		int res = 1;
		try {
			_logger->Info("GetCardInfoL Begin card: " + card + " rest: " + restaurant + " unit: " + unitNo);

			CardInfoL^ cardInfo = gcnew CardInfoL();

			_logger->Info("CardInfoL Before Invoke: " + cardInfo->ToStringLog());


			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 GetCardInfoL Invoke");
				res = _farcard5->GetCardInfoL(card, restaurant, unitNo, cardInfo);
				_logger->Info("Plugin farcard5 GetCardInfoL Invoke Complete result: " + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardInfoL Invoke");
				res = _farcards->GetCardInfoL(card, restaurant, unitNo, cardInfo);
				_logger->Info("Plugin farcardAll GetCardInfoL Invoke Complete result: " + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetCardInfoL Invoke");
			}

			_logger->Info("CardInfoL After Invoke: " + cardInfo->ToStringLog());

			if (res == 0)
			{
				_logger->Info("Save CardInfoL to native address:" + info.ToInt32());
				Marshal::StructureToPtr(cardInfo, info, false);
			}

			_logger->Info("GetCardInfoL Complete Result: " + res);

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

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin farcard6 TransactionEx Begin");
				res = _farcard6->TransactionsEx(% listTr, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin farcard6 TransactionEx Complete Result: " + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll TransactionEx Begin");
				res = _farcards->TransactionsEx(% listTr, inpBuf, inpKind, outBuf, outKind);
				_logger->Info("Plugin farcardAll TransactionEx Complete Result: " + res);
			}
			else
			{
				_logger->Error("No Plugin farcard TransactionEx Invoke");
			}

			if (!Object::ReferenceEquals(outBuf, nullptr) && outBuf->Length > 0)
			{
				_logger->Info("OutBuf: " + Text::Encoding::UTF8->GetString(outBuf));
			}

			outLen = !Object::ReferenceEquals(outBuf, nullptr) ? outBuf->Length : 0;
			_logger->Info("TransactionEx Complete Result: " + res);

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

	int TransactionPacketL(UInt32 count, IntPtr pList)
	{
		int res = 1;
		try
		{
			List<TransactionPacketInfoL^>% listTr = List<TransactionPacketInfoL^>();
			_logger->Info("Copy native: " + pList.ToInt32() + "TransactionPacketLList to object, count: " + count);
			for (UInt32 i = 0; i < count; i++)
			{
				int size = IntPtr::Size;
				IntPtr address = safe_cast<IntPtr>(
					Marshal::ReadInt32(pList, i * IntPtr::Size));

				_logger->Info("Native address: " + address.ToInt32() + " of item transaction:" + i);

				TransactionPacketInfoL^ info =
					safe_cast<TransactionPacketInfoL^>(
						Marshal::PtrToStructure(address
							,
							TransactionPacketInfoL::typeid)
						);

				listTr.Add(info);
				_logger->Info("TransactionPacketL Item: " + i + " " + info->ToStringLog());
			}
			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 TransactionPacketL begin");
				res = _farcard5->TransactionPacketL(% listTr);
				_logger->Info("Plugin farcard5 TransactionPacketL Complete result:" + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll TransactionPacketL begin");
				res = safe_cast<IFarcards5^>(_farcards)->TransactionPacketL(% listTr);
				_logger->Info("Plugin farcardAll TransactionPacketL Complete result:" + res);
			}
			else
			{
				_logger->Error("No Plugin farcard TransactionPacketL Invoke");
			}

			_logger->Info("TransactionPacket Complete result:" + res);

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

	int TransactionL(UInt32 account, IntPtr info)
	{
		int res = 1;
		try
		{
			_logger->Info("TransactionEx begin");
			_logger->Info("Copy native: " + info.ToInt32() + " TransactionL to object");

			TransactionInfoL^ trInfo =
				safe_cast<TransactionInfoL^>(
					Marshal::PtrToStructure(info
						,
						TransactionInfoL::typeid)
					);

			_logger->Info("TransactionEx Item: " + trInfo->ToStringLog());

			if (!Object::ReferenceEquals(_farcard5, nullptr)) {

				_logger->Info("Plugin Farcard5 TransactionEx Begin");
				res = _farcard5->TransactionL(account, trInfo);
				_logger->Info("Plugin Farcard5 TransactionEx Complete Result: " + res);

			}
			else if (!Object::ReferenceEquals(_farcards, nullptr)) {

				_logger->Info("Plugin FarcardAll TransactionEx Begin");
				res = _farcards->TransactionL(account, trInfo);
				_logger->Info("Plugin FarcardAll TransactionEx Complete Result: " + res);
			}
			else
			{
				_logger->Error("No Plugin farcard TransactionEx Invoke");
			}

			_logger->Info("TransactionEx Complete Result: " + res);
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
			_logger->Info("GetCardImageEx card:" + card);


			TextInfo^ info = gcnew TextInfo();
			_logger->Info("Info before invoke" + info->ToStringLog());


			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin farcard6 GetCardImageEx Before Card: " + card);
				res = _farcard6->GetCardImageEx(card, info);
				_logger->Info("Plugin farcard6 GetCardImageEx Complete Result: " + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardImageEx Before Card: " + card);
				res = _farcards->GetCardImageEx(card, info);
				_logger->Info("Plugin farcardAll GetCardImageEx Complete Result: " + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetCardImageEx Invoke");
			}

			_logger->Info("Info after invoke" + info->ToStringLog());

			if (res == 0)
			{
				_logger->Info("Copy Info to native address: " + pInfo.ToInt32());
				Marshal::StructureToPtr(info, pInfo, false);
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

	int GetCardImageL(Int64 card, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			_logger->Info("GetCardImageL card:" + card);

			TextInfo^ info = gcnew TextInfo();
			_logger->Info("Info before invoke" + info->ToStringLog());

			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard6 GetCardImageL Before Card: " + card);
				res = _farcard5->GetCardImageL(card, info);
				_logger->Info("Plugin farcard6 GetCardImageL Complete Result: " + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardImageL Before Card: " + card);
				res = _farcards->GetCardImageL(card, info);
				_logger->Info("Plugin farcardAll GetCardImageL Complete Result: " + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetCardImageL Invoke");
			}

			_logger->Info("Info after invoke" + info->ToStringLog());

			if (res == 0)
			{
				_logger->Info("Copy Info to native address: " + pInfo.ToInt32());
				Marshal::StructureToPtr(info, pInfo, false);
			}

			_logger->Info("GetCardImageL Complete");
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
			HolderInfo^ info = gcnew  HolderInfo();

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				res = _farcard6->FindEmail(email, info);
			}
			else if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				res = _farcard5->FindEmail(email, info);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				res = safe_cast<IFarcards5^>(_farcards)->FindEmail(email, info);
			}
			else
			{
				_logger->Error("No Plugin farcard FindEmail Invoke");
			}

			if (res == 0)
			{
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
			else if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_farcard5->FindCardsL(findText, cbFind, backPtr);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				safe_cast<IFarcards5^>(_farcards)->FindCardsL(findText, cbFind, backPtr);
			}
			else
			{
				_logger->Error("No Plugin farcard FindCardsL Invoke");
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
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_farcards->FindAccountsByKind(kind, findText, cbFind, backPtr);
			}
			else
			{
				_logger->Error("No Plugin farcard FindAccountsByKind Invoke");
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

			array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
			Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
			if (inpBuf->Length > 0)
			{
				_logger->Info("InpBuf: " + Text::Encoding::UTF8->GetString(inpBuf));
			}

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin farcard6 AnyInfo begin");
				_farcard6->AnyInfo(inpBuf, outBuf);
				_logger->Info("Plugin farcard6 AnyInfo Complete");
			}
			else if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 AnyInfo begin");
				_farcard5->AnyInfo(inpBuf, outBuf);
				_logger->Info("Plugin farcard5 AnyInfo Complete");
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll AnyInfo begin");
				safe_cast<IFarcards5^>(_farcards)->AnyInfo(inpBuf, outBuf);
				_logger->Info("Plugin farcardAll AnyInfo Complete");
			}
			else
			{
				_logger->Error("No Plugin farcard AnyInfo Invoke");
			}

			if (!Object::ReferenceEquals(outBuf, nullptr) && outBuf->Length > 0)
			{
				_logger->Info("OutBuf: " + Text::Encoding::UTF8->GetString(outBuf));
			}

			outLen = !Object::ReferenceEquals(outBuf, nullptr) ? outBuf->Length : 0;
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

			DiscLevelInfo^ info = gcnew  DiscLevelInfo();
			_logger->Info("DiscLevelInfo Before Invoke: " + info->ToStringLog());

			if (!Object::ReferenceEquals(_farcard6, nullptr))
			{
				_logger->Info("Plugin farcard6 GetDiscLevelInfo begin");
				res = _farcard6->GetDiscLevelInfoL(account, info);
				_logger->Info("Plugin farcard6 GetDiscLevelInfo Complete result:" + res);
			}
			else if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 GetDiscLevelInfo begin");
				res = _farcard5->GetDiscLevelInfoL(account, info);
				_logger->Info("Plugin farcard5 GetDiscLevelInfo Complete result:" + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetDiscLevelInfo begin");
				res = safe_cast<IFarcards5^>(_farcards)->GetDiscLevelInfoL(account, info);
				_logger->Info("Plugin farcardAll GetDiscLevelInfo Complete result:" + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetDiscLevelInfo Invoke");
			}

			_logger->Info("DiscLevelInfo After Invoke : " + info->ToStringLog());

			if (res == 0)
			{

				Marshal::StructureToPtr(info, pInfo, false);
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

	int GetCardMessageL(UInt32 account, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			TextInfoEx^ info = gcnew TextInfoEx();
			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 GetCardMessageL begin");
				res = _farcard5->GetCardMessageL(account, info);
				_logger->Info("Plugin farcard5 GetCardMessageL Complete result:" + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardMessageL begin");
				res = safe_cast<IFarcards5^>(_farcards)->GetCardMessageL(account, info);
				_logger->Info("Plugin farcardAll GetCardMessageL Complete result:" + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetCardMessageL Invoke");
			}

			if (res == 0)
			{

				Marshal::StructureToPtr(info, pInfo, false);
			}

			_logger->Info("GetCardMessageL Complete result:" + res);

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

	int GetCardMessage2L(UInt32 account, IntPtr pInfo)
	{
		int res = 1;
		try
		{
			TextInfoEx^ info = gcnew TextInfoEx();
			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 GetCardMessage2L begin");
				res = _farcard5->GetCardMessage2L(account, info);
				_logger->Info("Plugin farcard5 GetCardMessage2L Complete result:" + res);
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll GetCardMessage2L begin");
				res = safe_cast<IFarcards5^>(_farcards)->GetCardMessage2L(account, info);
				_logger->Info("Plugin farcardAll GetCardMessage2L Complete result:" + res);
			}
			else
			{
				_logger->Error("No Plugin farcard GetCardMessage2L Invoke");
			}

			if (res == 0)
			{

				Marshal::StructureToPtr(info, pInfo, false);
			}

			_logger->Info("GetCardMessage2L Complete result:" + res);

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


	void CheckInfoL(UInt32 account, IntPtr pInpBuf, UInt32 inpLen)
	{
		try
		{
			array<Byte>^ inpBuf = gcnew  array<Byte>(inpLen);
			Marshal::Copy(pInpBuf, inpBuf, 0, inpLen);
			if (inpBuf->Length > 0)
			{
				_logger->Info("InpBuf: " + Text::Encoding::UTF8->GetString(inpBuf));
			}

			if (!Object::ReferenceEquals(_farcard5, nullptr))
			{
				_logger->Info("Plugin farcard5 CheckInfoL begin");
				_farcard5->CheckInfoL(account, inpBuf);
				_logger->Info("Plugin farcard5 CheckInfoL Complete");
			}
			else if (!Object::ReferenceEquals(_farcards, nullptr))
			{
				_logger->Info("Plugin farcardAll CheckInfoL begin");
				_farcards->CheckInfoL(account, inpBuf);
				_logger->Info("Plugin farcardAll CheckInfoL Complete");
			}
			else
			{
				_logger->Error("No Plugin farcard CheckInfoL Invoke");
			}

			_logger->Info("CheckInfoL Complete");

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
}
