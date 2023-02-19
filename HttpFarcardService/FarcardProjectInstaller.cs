using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarcardContract;
using FarcardContract.Extensions;
using static System.Net.Mime.MediaTypeNames;


namespace HttpFarcardService
{
    [RunInstaller(true)]
    public partial class FarcardProjectInstaller : System.Configuration.Install.Installer
    {
        public FarcardProjectInstaller()
        {
            var commandLine = new CommandLineArguments(Environment.GetCommandLineArgs());
            InitializeComponent();
            if (commandLine["name"] != null || commandLine["n"] != null)
            {
                var name = commandLine["name"] != null ? commandLine["name"] : commandLine["n"];
                if (!string.IsNullOrWhiteSpace(name) && name != "true")
                {
                    FarcardServiceInstaller.ServiceName = name;
                }
            }
        }


        private void FarcardProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                new ServiceController(FarcardServiceInstaller.ServiceName).Stop();
            }
            catch
            {
            }
        }

        private void FarcardServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                new ServiceController(FarcardServiceInstaller.ServiceName).Start();
            }
            catch
            {
            }
        }
    }
}
