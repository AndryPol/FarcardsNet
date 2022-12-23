using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace FarcardContract.Farcard5
{
    [Export(typeof(IFarcards5))]
    public class Farcard5Demo : IFarcards5
    {
        public void Init_dll()
        {

        }

        public void Done_dll()
        {

        }
    }
}
