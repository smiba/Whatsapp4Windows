namespace WhatsApp4WindowsCSharp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Button3 = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Destphone = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button2 = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.TextBox();
            this.Key = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.NicknameString = new System.Windows.Forms.TextBox();
            this.Phonenumber = new System.Windows.Forms.TextBox();
            this.deststatus = new System.Windows.Forms.Label();
            this.secondecheck = new System.Windows.Forms.Timer(this.components);
            this.ChatLog = new System.Windows.Forms.RichTextBox();
            this.Contacts = new System.Windows.Forms.ListBox();
            this.MsgStat = new System.Windows.Forms.Label();
            this.InfoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(183, 9);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(75, 23);
            this.Button3.TabIndex = 25;
            this.Button3.Text = "(re)Login";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(264, 12);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(50, 13);
            this.Label5.TabIndex = 24;
            this.Label5.Text = "Receiver";
            // 
            // Destphone
            // 
            this.Destphone.Location = new System.Drawing.Point(316, 10);
            this.Destphone.Name = "Destphone";
            this.Destphone.Size = new System.Drawing.Size(109, 20);
            this.Destphone.TabIndex = 23;
            this.Destphone.TextChanged += new System.EventHandler(this.Destphone_TextChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(11, 43);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(35, 13);
            this.Label4.TabIndex = 21;
            this.Label4.Text = "Name";
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(183, 37);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 20;
            this.Button2.Text = "Registrate";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(501, 319);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(102, 20);
            this.SendButton.TabIndex = 19;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Message
            // 
            this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Message.Location = new System.Drawing.Point(14, 320);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(481, 20);
            this.Message.TabIndex = 17;
            this.Message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Message_KeyDown);
            // 
            // Key
            // 
            this.Key.Location = new System.Drawing.Point(60, 64);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(198, 20);
            this.Key.TabIndex = 16;
            this.Key.Text = "Damn whatsapp key (Try to use the registrate or grab it from a sniffer)";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(11, 68);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(25, 13);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "Key";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(44, 13);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Number";
            // 
            // NicknameString
            // 
            this.NicknameString.Location = new System.Drawing.Point(60, 39);
            this.NicknameString.Name = "NicknameString";
            this.NicknameString.Size = new System.Drawing.Size(100, 20);
            this.NicknameString.TabIndex = 22;
            this.NicknameString.Text = "Your Name";
            // 
            // Phonenumber
            // 
            this.Phonenumber.Location = new System.Drawing.Point(61, 12);
            this.Phonenumber.Name = "Phonenumber";
            this.Phonenumber.Size = new System.Drawing.Size(99, 20);
            this.Phonenumber.TabIndex = 13;
            this.Phonenumber.Text = "987654321";
            // 
            // deststatus
            // 
            this.deststatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deststatus.AutoSize = true;
            this.deststatus.Location = new System.Drawing.Point(535, 164);
            this.deststatus.Name = "deststatus";
            this.deststatus.Size = new System.Drawing.Size(62, 13);
            this.deststatus.TabIndex = 26;
            this.deststatus.Text = "[INACTIVE]";
            // 
            // secondecheck
            // 
            this.secondecheck.Interval = 1000;
            this.secondecheck.Tick += new System.EventHandler(this.Checktiepen_Tick);
            // 
            // ChatLog
            // 
            this.ChatLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatLog.Location = new System.Drawing.Point(19, 195);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.ReadOnly = true;
            this.ChatLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ChatLog.Size = new System.Drawing.Size(476, 119);
            this.ChatLog.TabIndex = 30;
            this.ChatLog.Text = "";
            this.ChatLog.TextChanged += new System.EventHandler(this.ChatLog_TextChanged);
            // 
            // Contacts
            // 
            this.Contacts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Contacts.FormattingEnabled = true;
            this.Contacts.Items.AddRange(new object[] {
            ""});
            this.Contacts.Location = new System.Drawing.Point(501, 180);
            this.Contacts.Name = "Contacts";
            this.Contacts.Size = new System.Drawing.Size(102, 134);
            this.Contacts.TabIndex = 31;
            this.Contacts.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // MsgStat
            // 
            this.MsgStat.AutoSize = true;
            this.MsgStat.Location = new System.Drawing.Point(313, 47);
            this.MsgStat.Name = "MsgStat";
            this.MsgStat.Size = new System.Drawing.Size(72, 13);
            this.MsgStat.TabIndex = 32;
            this.MsgStat.Text = "[Debug Stats]";
            // 
            // InfoButton
            // 
            this.InfoButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoButton.Location = new System.Drawing.Point(558, 139);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(17, 22);
            this.InfoButton.TabIndex = 33;
            this.InfoButton.Text = "i";
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 351);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.MsgStat);
            this.Controls.Add(this.Contacts);
            this.Controls.Add(this.ChatLog);
            this.Controls.Add(this.deststatus);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Destphone);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.Key);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.NicknameString);
            this.Controls.Add(this.Phonenumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(507, 384);
            this.Name = "Form1";
            this.Text = "Whatsapp For Windows - InDEV";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox Destphone;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button SendButton;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox NicknameString;
        internal System.Windows.Forms.TextBox Phonenumber;
        private System.Windows.Forms.Label deststatus;
        public System.Windows.Forms.TextBox Key;
        private System.Windows.Forms.Timer secondecheck;
        public System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.RichTextBox ChatLog;
        private System.Windows.Forms.ListBox Contacts;
        private System.Windows.Forms.Label MsgStat;
        private System.Windows.Forms.Button InfoButton;
    }
}

