namespace Farcards
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GetCardButton = new System.Windows.Forms.Button();
            this.CardNumber = new System.Windows.Forms.NumericUpDown();
            this.TransactionsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ObjCode = new System.Windows.Forms.NumericUpDown();
            this.RestCode = new System.Windows.Forms.NumericUpDown();
            this.stationCode = new System.Windows.Forms.NumericUpDown();
            this.PhotoPictureBox = new System.Windows.Forms.PictureBox();
            this.GetImageButton = new System.Windows.Forms.Button();
            this.FindEmailButton = new System.Windows.Forms.Button();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CardNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GetCardButton
            // 
            this.GetCardButton.Location = new System.Drawing.Point(147, 70);
            this.GetCardButton.Name = "GetCardButton";
            this.GetCardButton.Size = new System.Drawing.Size(101, 23);
            this.GetCardButton.TabIndex = 4;
            this.GetCardButton.Text = "GetCardEx";
            this.GetCardButton.UseVisualStyleBackColor = true;
            this.GetCardButton.Click += new System.EventHandler(this.GetCard_Click);
            // 
            // CardNumber
            // 
            this.CardNumber.Location = new System.Drawing.Point(12, 73);
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.Size = new System.Drawing.Size(120, 20);
            this.CardNumber.TabIndex = 3;
            this.CardNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TransactionsButton
            // 
            this.TransactionsButton.Location = new System.Drawing.Point(147, 102);
            this.TransactionsButton.Name = "TransactionsButton";
            this.TransactionsButton.Size = new System.Drawing.Size(101, 23);
            this.TransactionsButton.TabIndex = 5;
            this.TransactionsButton.Text = "TransactionsEx";
            this.TransactionsButton.UseVisualStyleBackColor = true;
            this.TransactionsButton.Click += new System.EventHandler(this.Transactions_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Сard";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rest";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Unit";
            // 
            // ObjCode
            // 
            this.ObjCode.Location = new System.Drawing.Point(12, 25);
            this.ObjCode.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjCode.Name = "ObjCode";
            this.ObjCode.Size = new System.Drawing.Size(50, 20);
            this.ObjCode.TabIndex = 0;
            this.ObjCode.Value = new decimal(new int[] {
            10010,
            0,
            0,
            0});
            // 
            // RestCode
            // 
            this.RestCode.Location = new System.Drawing.Point(68, 25);
            this.RestCode.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.RestCode.Name = "RestCode";
            this.RestCode.Size = new System.Drawing.Size(64, 20);
            this.RestCode.TabIndex = 1;
            this.RestCode.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // stationCode
            // 
            this.stationCode.Location = new System.Drawing.Point(147, 25);
            this.stationCode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.stationCode.Name = "stationCode";
            this.stationCode.Size = new System.Drawing.Size(101, 20);
            this.stationCode.TabIndex = 2;
            this.stationCode.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PhotoPictureBox
            // 
            this.PhotoPictureBox.Location = new System.Drawing.Point(12, 102);
            this.PhotoPictureBox.Name = "PhotoPictureBox";
            this.PhotoPictureBox.Size = new System.Drawing.Size(120, 121);
            this.PhotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PhotoPictureBox.TabIndex = 6;
            this.PhotoPictureBox.TabStop = false;
            // 
            // GetImageButton
            // 
            this.GetImageButton.Location = new System.Drawing.Point(147, 131);
            this.GetImageButton.Name = "GetImageButton";
            this.GetImageButton.Size = new System.Drawing.Size(101, 23);
            this.GetImageButton.TabIndex = 7;
            this.GetImageButton.Text = "Get ImageEx";
            this.GetImageButton.UseVisualStyleBackColor = true;
            this.GetImageButton.Click += new System.EventHandler(this.GetImage_Click);
            // 
            // FindEmailButton
            // 
            this.FindEmailButton.Location = new System.Drawing.Point(147, 200);
            this.FindEmailButton.Name = "FindEmailButton";
            this.FindEmailButton.Size = new System.Drawing.Size(101, 23);
            this.FindEmailButton.TabIndex = 8;
            this.FindEmailButton.Text = "Find Email";
            this.FindEmailButton.UseVisualStyleBackColor = true;
            this.FindEmailButton.Click += new System.EventHandler(this.FindEmail_Click);
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(147, 174);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(100, 20);
            this.EmailBox.TabIndex = 9;
            this.EmailBox.Text = "test@test.ru";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Email";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 235);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.FindEmailButton);
            this.Controls.Add(this.GetImageButton);
            this.Controls.Add(this.PhotoPictureBox);
            this.Controls.Add(this.stationCode);
            this.Controls.Add(this.RestCode);
            this.Controls.Add(this.ObjCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TransactionsButton);
            this.Controls.Add(this.CardNumber);
            this.Controls.Add(this.GetCardButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Card Retranslator 6";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CardNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RestCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetCardButton;
        private System.Windows.Forms.NumericUpDown CardNumber;
        private System.Windows.Forms.Button TransactionsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ObjCode;
        private System.Windows.Forms.NumericUpDown RestCode;
        private System.Windows.Forms.NumericUpDown stationCode;
        private System.Windows.Forms.PictureBox PhotoPictureBox;
        private System.Windows.Forms.Button GetImageButton;
        private System.Windows.Forms.Button FindEmailButton;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Label label4;
    }
}

