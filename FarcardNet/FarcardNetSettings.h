#ifndef FARCARDNETSETTINGS
#define FARCARDNETSETTINGS

#pragma once
namespace  FarcardNet
{

	using namespace FarcardContract;
	[System::Serializable]
	public ref class FarcardNetSettings : public XmlSettings<FarcardNetSettings^>
	{
	public:

		property int LogLevel;
		property System::String^ Library;
		FarcardNetSettings();
	};
}
#endif