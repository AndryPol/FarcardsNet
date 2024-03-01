using FarcardContract;

namespace TunnelFarcard6
{
    public class TunnelFarcard6Settings:XmlSettings<TunnelFarcard6Settings>
    {
        public int LogLevel { get; set; } = 5;

        public string PathDll { get; set; } = "ExtDllHTTP.dll";
    }
}
