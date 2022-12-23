using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarcardContract;

namespace TunnelFarcard6
{
    public class TunnelFarcardSettings:XmlSettings<TunnelFarcardSettings>
    {
        public string PathDll { get; set; } = "ExtDllHTTP.dll";
    }
}
