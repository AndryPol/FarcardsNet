using FarcardContract.Farcard6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestFarcardNet
{
    internal static class Extensions
    {
        internal static void AssertMemorySize<T>(this T mobject, int size)
        {
            var memSize = Marshal.SizeOf(typeof(T));
            if(memSize!=size)
                Assert.Fail($"Размер {typeof(T).Name} = {memSize} ожидалось {size}");

        }

    }
}
