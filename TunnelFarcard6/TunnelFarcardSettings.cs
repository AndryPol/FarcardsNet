using FarcardContract;

namespace TunnelFarcard6
{
    public class TunnelFarcardSettings:XmlSettings<TunnelFarcardSettings>
    {
        public int LogLevel { get; set; } = 5;

        public string PathDll { get; set; } = "ExtDllHTTP.dll";
    }
}
