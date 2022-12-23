using FarcardContract.Farcard6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace TestFarcardNet
{
    [TestClass]
    public class TestFarcardContract
    {
        [TestMethod]
        public void TestCardInfoMemory()
        {
            var cInfo = new CardInfoEx().Demo();
            for (int i = 0; i < 9; i++)
            {
                cInfo[i] = (i + 1) * 50.11m;
            }
            cInfo.AssertMemorySize(1164);
            int size = Marshal.SizeOf(cInfo);
            var buffer = new byte[size];
            var ptr = Marshal.AllocHGlobal(buffer.Length);
            Marshal.StructureToPtr(cInfo, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);

            var type = typeof(CardInfoEx);
            var yesNoType = typeof(YesNo);
            var csize = BitConverter.ToUInt16(buffer, 0);
            Assert.AreEqual(csize, size);
            var deletedOffs = Marshal.OffsetOf(type, "bDeleted").ToInt32();
            var bDeleted = Enum.Parse(yesNoType, buffer[deletedOffs].ToString());
            Assert.AreEqual(cInfo.Deleted, bDeleted);

        }

        [TestMethod]
        public void TestTransInfoMemory()
        {
            var tInfo = new TransactionInfoEx();
            tInfo.AssertMemorySize(122);

        }
    }
}
