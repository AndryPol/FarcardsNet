using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HttpFarcardService
{
    partial class FarcardService : ServiceBase
    {
        private FarcardHttpService _httpService;
        public FarcardService()
        {
            InitializeComponent();
            _httpService = new FarcardHttpService();
        }

        protected override void OnStart(string[] args)
        {
            _httpService.Start();
        }

        protected override void OnStop()
        {
            _httpService.Stop();
        }

        protected override void OnPause()
        {
            _httpService.Stop();
        }

        protected override void OnContinue()
        {
           _httpService.Start();
        }
    }
}
