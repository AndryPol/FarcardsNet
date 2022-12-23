using FarcardContract;
using FarcardContract.Farcard6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TunnelFarcard6;

namespace TestFarcardNet
{
    [TestClass]
    public class TestFarcardFabric
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestFarcardAllFabric()
        {
            try
            {
                IFarcards allFarcard = new FarcardsAllFabric().GetProcessor();
                Assert.IsNotNull(allFarcard);
                TestIFarcard6(allFarcard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestFarcard6Tunnel()
        { 
            var fileinfo = new FileInfo("TunnelFarcard6.dll");
            IFarcards6 tunneFarcards6 = new Farcards6Fabric().GetProcessor(fileinfo);
            TestIFarcard6(tunneFarcards6);

        }

        
        private void TestIFarcard6(IFarcards6 farcards6)
        {
            try
            {
                farcards6.Init_dll();
                var info = new CardInfoEx();
                var outbuf = string.Empty;
                ushort outkind = 0;
                var res = farcards6.GetCardInfoEx_dll(1, 1, 1, ref info, new byte[0], 0, out outbuf, out outkind);
                Assert.AreEqual(res, 0);
                res = farcards6.GetCardInfoEx_dll(10, 1, 1, ref info, new byte[0], 0, out outbuf, out outkind);
                Assert.AreEqual(res, 1);
                farcards6.Done_dll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
