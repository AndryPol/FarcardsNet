using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Windows.Forms;

namespace FarcardContract.Farcard6
{
    [Export(typeof(IFarcards6))]
    public class Farcards6Demo : IFarcards6
    {

        bool UI = false;
        public void Init_dll()
        {
            try
            {
                //UI = Environment.UserInteractive;
                //if (UI)
                //{
                //    var t = new Thread(startform);
                //    t.IsBackground = false;
                //    t.SetApartmentState(ApartmentState.STA);
                //    t.Start();
                //    Thread.Sleep(100);
                //}
            }
            catch { }
        }

        void startform()
        {
            //var m = new Test();
            //m.FormClosed += M_FormClosed;
           
            //Application.Run(m);


        }

        private void M_FormClosed(object sender, FormClosedEventArgs e)
        {
            var process = Process.GetCurrentProcess();
            var m = process.MainModule;

            // process.CloseMainWindow();
            // process.WaitForExit();
            process.Kill();

        }

        public void Done_dll()
        {

        }

        public int GetCardInfoEx_dll(long Card, uint Restaurant, uint UnitNo, ref CardInfoEx CardInfo, Byte[] InpBuf, ushort InpKind,
            out string pOutBuf, out ushort OutKind)
        {
            pOutBuf = String.Empty;
            OutKind = 0;
            if (Card == 1)
            {
                CardInfo.Demo();
                var results = CardInfo.ToStringLog();

                return 0;
            }

            return 1;

        }

        public int TransactionsEx_dll(List<TransactionInfoEx> TransactionInfo, Byte[] InpBuf, ushort InpKind, out string OutBuf, out ushort OutKind)
        {




            OutBuf = string.Empty;
            OutKind = 0;
            return 0;
        }

        public int FindEmail_dll(string Email, ref HolderInfo OutInfo)
        {
            var size = Marshal.SizeOf(typeof(HolderInfo));
            OutInfo.Demo();
            return 0;
        }

        public void FindCardsL(string FindText, CBFind CbFind, IntPtr Back)
        {
            CbFind?.Invoke(Back, 1234, 1, "Тестовый Клиент");
        }


    }
}
