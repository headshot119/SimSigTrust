namespace SimSigTrust
{
    partial class MainMenu
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
            this.screenTrust = new System.Windows.Forms.ListBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtClock = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // screenTrust
            // 
            this.screenTrust.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenTrust.BackColor = System.Drawing.SystemColors.WindowText;
            this.screenTrust.Font = new System.Drawing.Font("Consolas", 11F);
            this.screenTrust.ForeColor = System.Drawing.Color.Cyan;
            this.screenTrust.FormattingEnabled = true;
            this.screenTrust.ItemHeight = 18;
            this.screenTrust.Location = new System.Drawing.Point(13, 82);
            this.screenTrust.Margin = new System.Windows.Forms.Padding(4);
            this.screenTrust.Name = "screenTrust";
            this.screenTrust.Size = new System.Drawing.Size(820, 454);
            this.screenTrust.TabIndex = 17;
            // 
            // txtUserInput
            // 
            this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserInput.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtUserInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserInput.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtUserInput.ForeColor = System.Drawing.Color.Cyan;
            this.txtUserInput.Location = new System.Drawing.Point(13, 49);
            this.txtUserInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserInput.MinimumSize = new System.Drawing.Size(4, 25);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(820, 25);
            this.txtUserInput.TabIndex = 18;
            this.txtUserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserInput_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtClock
            // 
            this.txtClock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClock.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtClock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClock.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtClock.ForeColor = System.Drawing.Color.Cyan;
            this.txtClock.Location = new System.Drawing.Point(747, 16);
            this.txtClock.Margin = new System.Windows.Forms.Padding(4);
            this.txtClock.MinimumSize = new System.Drawing.Size(4, 25);
            this.txtClock.Name = "txtClock";
            this.txtClock.Size = new System.Drawing.Size(86, 25);
            this.txtClock.TabIndex = 20;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 542);
            this.Controls.Add(this.txtClock);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.screenTrust);
            this.Name = "MainMenu";
            this.Text = "SimSig Trust";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox screenTrust;
        public System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtClock;
    }
}

