using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using FarcardContract.Farcard6;

namespace FarcardContract.Demo.Farcard6
{
    [Export(typeof(IFarcards6))]
    public class Farcard6Demo : IFarcards6
    {
        readonly Logger<Farcard6Demo> _logger = new Logger<Farcard6Demo>();
        bool UI = false;
        public void Init()
        {
            _logger.Trace("Begin init");
            try
            {
                var app = Process.GetCurrentProcess();

                _logger.Trace($"start app {app.Id} : {app.ProcessName}");
                foreach (ProcessModule module in app.Modules)
                {
                    _logger.Trace($"Loaded modules: {module.ModuleName} {module.EntryPointAddress}");
                }
                _logger.Trace("Load Demo Processor");
                UI = Environment.UserInteractive;
                if (UI)
                {
                //    var t = new Thread(startform);
                //    t.IsBackground = false;
                //    t.SetApartmentState(ApartmentState.STA);
                //    t.Start();
                //    Thread.Sleep(100);
                }
            }
            catch { }
            _logger.Trace("End init");
        }

        //void startform()
        //{
        //    //var m = new Test();
        //    //m.FormClosed += FormClosed;

        //    //Application.Run(m);


        //}

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            var process = Process.GetCurrentProcess();
            // var m = process.MainModule;

            // process.CloseMainWindow();
            // process.WaitForExit();
            process.Kill();

        }

        public void Done()
        {
            _logger.Trace("Call done");
        }

        public int GetCardInfoEx(long card, uint restaurant, uint unitNo, ref CardInfoEx cardInfo, Byte[] inpBuf, BuffKind inpKind,
            out byte[] outBuf, out BuffKind outKind)
        {


            outBuf = null;
            outKind = 0;
            _logger.Trace("GetCardInfoExDemo");

            var inputXml = Encoding.UTF8.GetString(inpBuf);
            _logger.Trace($"InpBuf {inputXml}");

            if (card == 1)
            {
                cardInfo.Demo();
                var results = cardInfo.ToStringLog();

                _logger.Trace(results);
                _logger.Trace("result: 0");
                return 0;
            }
            _logger.Trace(cardInfo.ToStringLog());
            _logger.Trace("result: 1");
            return 1;

        }

        public int TransactionsEx(List<TransactionInfoEx> transactionInfo, Byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {
            outBuf = null;
            outKind = 0;
            _logger.Trace("TransactionExDemo");
            var inputXml = Encoding.UTF8.GetString(inpBuf);
            _logger.Trace($"InpBuf {inputXml}");

            foreach (var transaction in transactionInfo)
            {
                _logger.Trace(transaction.ToStringLog());
            }

            var text = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <TRRESPONSE error_code=""0"" err_text="""">
                <TRANSACTION ext_id=""1111111"" num=""222222"" cardcode=""777777"" slip=""Текст для печати""/>
                </TRRESPONSE>";
            outBuf = Encoding.UTF8.GetBytes(text);
            outKind = BuffKind.Xml;
            _logger.Trace("result 0");
            return 0;
        }

        public int FindEmail(string email, ref HolderInfo holderInfo)
        {
            _logger.Trace($"FindEmailDemo email: {email}");
            if ("test@test.ru".Equals(email, StringComparison.InvariantCultureIgnoreCase))
            {
                holderInfo.Demo();
                _logger.Trace(holderInfo.ToStringLog());
                _logger.Trace("result 0");
                return 0;
            }
            _logger.Trace(holderInfo.ToStringLog());
            _logger.Trace("result 1");
            return 1;
        }

        public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
        {
            _logger.Trace($"FindCardsLDemo {findText}");
            var info = new HolderInfo().Demo();
            if (findText != null)
                if (info.Card.ToString().Contains(findText)
                    || info.Owner.ToLower().Contains(findText.ToLower()))
                {
                    _logger.Trace($"Find account by text: {findText} ");
                    _logger.Trace(info.ToStringLog());
                    cbFind?.Invoke(backPtr, info.ClientId, info.Card, info.Owner);
                }

        }

        public void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr)
        {
            _logger.Trace($"FindAccountsByKindDemo kind: {kind} text: {findText}");
            var info = new HolderInfo().Demo();
            if (kind == FindKind.ByRoom)
                if (findText == info.Card.ToString())
                {
                    _logger.Trace($"Find account by room: {findText} ");
                    _logger.Trace(info.ToStringLog());
                    cbFind(backPtr, info.ClientId, info.Card, info.Owner);
                }
        }

        public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
        {
            _logger.Trace($"AnyInfo");
            outBuf = null;
            if (inpBuf != null && inpBuf.Length > 0)
                _logger.Trace($"InputBuf:{Encoding.UTF8.GetString(inpBuf)}");
        }

        public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
        {
            _logger.Trace($"GetDiscLevelInfoL account: {account}");
            int res = 1;
            if (account == ExtensionDemo.GetDemoAccount)
            {
                info.Demo();
                res = 0;
            }

            return res;
        }

        public int GetCardImageEx(long card, ref TextInfo info)
        {
            _logger.Trace("GetCardImageExDemo");
            if (card == 1)
            {
                var photo = Properties.Resources.no_photo;
                var photoPath = new FileInfo(nameof(Properties.Resources.no_photo) + ".jpg");
                try
                {
                    using (var fs = new FileStream(photoPath.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        photo.Save(fs, ImageFormat.Jpeg);
                        info.Text = photoPath.FullName;
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }

            return 1;
        }
    }
}
