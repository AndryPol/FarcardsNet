using FarcardContract.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;

namespace HttpFarcardService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLineArguments commandLine = new CommandLineArguments(args);

                WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

                if (Environment.UserInteractive)
                {
                    if (!hasAdministrativeRight)
                    {
                        StartProcess(args);
                        return;
                    }

                    if (commandLine.IsParam("i") || commandLine.IsParam("u") ||
                        commandLine.IsParam("install") || commandLine.IsParam("uninstall"))
                    {

                        var asi = new AssemblyInstaller(System.Reflection.Assembly.GetExecutingAssembly(), null);
                        var savedState = new Dictionary<String, String>();
                        if (commandLine.IsParam("i") || commandLine.IsParam("install"))
                        {
                            asi.Install(savedState);
                            MessageBox.Show("Служба успешно установлена");
                        }
                        else
                        {
                            asi.Uninstall(savedState);
                            MessageBox.Show("Служба Успешно удалена");
                        }

                        return;
                    }

                    var s = new FarcardHttpService();
                    s.Start();
                    Console.ReadLine();
                    s.Stop();

                }
                else
                {
                    ServiceBase.Run(new FarcardService());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        static void StartProcess(string[] Args)
        {
            string args = String.Join(" ", Args);
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = Application.ExecutablePath,
                Arguments = args
            };
            try
            {
                Process.Start(processInfo);
            }
            catch (Win32Exception)
            {

            }
            Application.Exit();


        }
    }
}
