using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using FarcardContract.Data;
using FarcardContract.Data.Farcard5;
using FarcardContract.Farcard5;

namespace FarcardContract.Demo.Farcard5
{
    [Export(typeof(IFarcards5))]
    public class Farcard5Demo : IFarcards5
    {
        private readonly Logger<Farcard5Demo> _logger = new Logger<Farcard5Demo>();
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

        public void Done()
        {
            _logger.Trace("Call done");
        }

        public int GetCardInfoL(ulong card, uint restaurant, uint unitNo, ref CardInfoL cardInfo)
        {
            _logger.Trace("GetCardInfoLDemo");
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

        public int TransactionL(uint account, TransactionInfoL transactionInfo)
        {
            _logger.Trace("TransactionLDemo");

            _logger.Trace(transactionInfo.ToStringLog());

            _logger.Trace("result 0");
            return 0;
        }
    }
}
