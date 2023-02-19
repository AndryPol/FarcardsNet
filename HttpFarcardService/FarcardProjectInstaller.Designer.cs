namespace HttpFarcardService
{
    partial class FarcardProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FarcardServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FarcardServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // FarcardServiceProcessInstaller
            // 
            this.FarcardServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.FarcardServiceProcessInstaller.Password = null;
            this.FarcardServiceProcessInstaller.Username = null;
            // 
            // FarcardServiceInstaller
            // 
            this.FarcardServiceInstaller.ServiceName = "FarcardService";
            this.FarcardServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.FarcardServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.FarcardServiceInstaller_AfterInstall);
            // 
            // FarcardProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FarcardServiceProcessInstaller,
            this.FarcardServiceInstaller});
            this.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.FarcardProjectInstaller_BeforeUninstall);

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller FarcardServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller FarcardServiceInstaller;
    }
}