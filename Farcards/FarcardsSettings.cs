using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarcardContract;

namespace Farcards
{
    public class FarcardsSettings:XmlSettings<FarcardsSettings>
    {
        public string ExtDll { get; set; } = "TunnelFarcard6.dll";

    }
}
