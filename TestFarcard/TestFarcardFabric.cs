using FarcardContract;
using FarcardContract.Farcard6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using HttpFarcardService;
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

        public void TestFarcard6Http()
        {
            using (var testenv = new TestHttpEnvironment())
            {
                try
                {
                    var fileinfo = new FileInfo("HttpFarcard6Client.dll");
                    IFarcards6 httpFarcards6 = new Farcards6Fabric().GetProcessor(fileinfo);
                    TestIFarcard6(httpFarcards6);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        private void TestIFarcard6(IFarcards6 farcards6)
        {
            try
            {
                farcards6.Init();
                var info = new CardInfoEx();
                var outbuf = new byte[] { };
                BuffKind outkind = BuffKind.None;

                string inputCheck = @"
<?xml version=""1.0"" encoding=""utf-8""?>
<CHECK stationcode=""1"" restaurantcode=""100010001"" cashservername=""SERV01"" generateddatetime=""2022-12-24T11:03:46"" chmode=""5"" locale=""1049"" shiftdate=""2022-12-24"" shiftnum=""1"">
				<EXTINFO reservation="""">
					<INTERFACES current=""1"">
						<INTERFACE cardcode=""1"" type=""PDS"" id=""1"" mode=""0"" interface=""1"">
							<HOLDERS>
								<ITEM cardcode=""1""/>
							</HOLDERS>
							<ALLCARDS/>
						</INTERFACE>
					</INTERFACES>
					<EXTCARDPROPERTIES>
						<PROPERTY name=""MAIN_PHONE_NUMBER""/>
					</EXTCARDPROPERTIES>
				</EXTINFO>
				<CHECKDATA checknum=""0"" printnum=""0"" fiscdocnum=""0"" delprintnum=""0"" delfiscdocnum=""0"" extfiscid="""" tablename=""0"" startservice=""2022-12-24T11:03:42"" ordernum=""1001/1"" guests=""1"" orderguid=""{398624F7-CA8C-4AB8-8940-7D116BAE73E1}"" checkguid=""{32FA0A30-877B-4DCE-A80C-D29064F1ED8D}"" order_cat=""1"" order_type=""9001"" persistentcomment="""">
					<CHECKPERSONS count=""1"">
						<PERSON id=""{D388DF80-FAFA-44EA-9D2C-348879E82F27}"" name=""Администратор"" code=""7"" role=""7""/>
					</CHECKPERSONS>
					<CHECKLINES count=""1"">
						<LINE id=""{BFDE9322-4B98-43D1-B27A-6C99FE9FE624}"" code=""4"" name=""Тест"" uni=""4"" type=""dish"" price=""1"" pr_list_sum=""1"" categ_id="""" servprint_id="""" egais_categ_id="""" quantity=""1"" sum=""1"">
							<LINETAXES count=""1"">
								<TAX id=""{34694A49-F862-4D07-9ACD-5EECF76E1B6E}"" sum=""0.15""/>
							</LINETAXES>
						</LINE>
					</CHECKLINES>
					<CHECKCATEGS count=""1"">
						<CATEG id=""0"" code=""0"" name="""" sum=""1"" discsum=""0""/>
					</CHECKCATEGS>
					<CHECKTAXES count=""1"">
						<TAX id=""{34694A49-F862-4D07-9ACD-5EECF76E1B6E}"" code=""1"" rate=""18"" sum=""0.15"" name=""НДС""/>
					</CHECKTAXES>
					<CURRENCIES count=""14"">
						<CURRENCY name=""Рубли"" id=""{16D72549-14D8-4F31-9E2A-0A833D4F5EED}"" amount=""100""/>
						<CURRENCY name=""Евро"" id=""{2A426EA7-CA4A-482C-B74C-45347F18759F}"" amount=""3""/>
						<CURRENCY name=""Доллар США"" id=""{D2DB2A5B-CB57-4626-AFB3-0851DCCA4724}"" amount=""3""/>
						<CURRENCY name=""VISA"" id=""{D7D5E50D-7246-4A71-939F-9B37AA300ACF}"" amount=""100""/>
						<CURRENCY name=""Master Card"" id=""{F95D8DCB-2060-4EEC-AE70-B4AC5A4E9D7F}"" amount=""100""/>
						<CURRENCY name=""American Express"" id=""{C47685DD-7635-4C78-A943-5E1F1CC1BC43}"" amount=""100""/>
						<CURRENCY name=""Diners Club"" id=""{268430C5-5A1F-4C0D-A2BA-11F38F2481B6}"" amount=""100""/>
						<CURRENCY name=""Euro Cirrus Maestro"" id=""{2C54289A-E02B-4CCE-AE14-99C7E6089335}"" amount=""100""/>
						<CURRENCY name=""JCB"" id=""{3F79BA35-8F64-460A-94AC-C4241DE07ED2}"" amount=""100""/>
						<CURRENCY name=""ПДС оплата"" id=""{9CA200F8-5961-4A6F-AE22-A095C65D1485}"" amount=""100""/>
						<CURRENCY name=""Безнал"" id=""{A77CEBBB-AB99-40CA-A998-4BA3DD630FDA}"" amount=""100""/>
						<CURRENCY name=""Карта отеля"" id=""{8FED6D5B-BF7E-4FC5-BFBA-7C7CC10A0C80}"" amount=""100""/>
						<CURRENCY name=""МИР"" id=""{8B3EB99E-FDA0-4E74-87DA-696C5EB4C2BC}"" amount=""100""/>
						<CURRENCY name=""Валюта СБП"" id=""{959B3882-CB13-49AF-9293-A745BDBE20E4}"" amount=""100""/>
					</CURRENCIES>
				</CHECKDATA>
			</CHECK>";

                var inpbuf = Encoding.Default.GetBytes(inputCheck);
                var res = farcards6.GetCardInfoEx(1, 1, 1, ref info, inpbuf, BuffKind.Xml, out outbuf, out outkind);
                Assert.AreEqual(res, 0);
                info = new CardInfoEx();
                res = farcards6.GetCardInfoEx(2, 1, 1, ref info, inpbuf, BuffKind.Xml, out outbuf, out outkind);
                Assert.AreEqual(res, 1);

                var tlistinfo = new List<TransactionInfoEx>();
                for (int i = 0; i < 3; i++)
                {
                    var tr = new TransactionInfoEx()
                    {
                        Account = (uint)(1000 + i),
                        Card = i,
                        PersonID = 1010 + i,
                        RKDate = DateTime.Now,
                        Summa = 100m,
                        Restaurant = 1,
                        RKUni = 1,
                        RKCheck = 1,
                        Kind = (TransactType)i
                    };
                    tlistinfo.Add(tr);
                }

                res = farcards6.TransactionsEx(tlistinfo, inpbuf, BuffKind.Xml, out outbuf, out outkind);
                Assert.AreEqual(res, 0);
                farcards6.Done();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }



    public class TestHttpEnvironment : IDisposable
    {
        private FarcardHttpService _httpService;

        public TestHttpEnvironment()
        {
            _httpService = new FarcardHttpService();
            new Thread(() =>
            {
                _httpService.Start();
            })
            { IsBackground = true }.Start();
        }
        public void Dispose()
        {
            _httpService.Stop();
        }
    }
}
