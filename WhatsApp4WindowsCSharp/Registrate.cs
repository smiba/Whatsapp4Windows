using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhatsApp4WindowsCSharp
{
    public partial class Registrate : Form
    {
        public string number;
        protected string cc;
        protected string phone;
        public string password;

        public Registrate()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(number))
            {
                this.cc = this.number.Substring(0, 2);
                this.phone = this.number.Substring(2);
                this.txtPhoneNumber.Text = number;
            }
        }

        private void btnCodeRequest_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtPhoneNumber.Text))
            {
                string method = "sms";
                if (this.radVoice.Checked)
                {
                    method = "voice";
                }
                this.number = this.txtPhoneNumber.Text;
                this.cc = this.number.Substring(0, 2);
                this.phone = this.number.Substring(2);
                MessageBox.Show(this.number + " " + this.password + " " + method + " " + this.cc);
                if (WhatsAppApi.Register.WhatsRegisterV2.RequestCode(this.number, out this.password, method))
                {
                    if (!string.IsNullOrEmpty(this.password))
                    {
                        //password received
                        this.OnReceivePassword();
                    }
                    else
                    {
                        this.grpStep1.Enabled = false;
                        this.grpStep2.Enabled = true;
                    }
                }
            }
        }

        private void btnRegisterCode_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtCode.Text) && this.txtCode.Text.Length == 6)
            {
                this.number = this.txtPhoneNumber.Text;
                this.cc = this.number.Substring(0, 2);
                this.phone = this.number.Substring(2);
                btnRegisterCode.Enabled = false;
                string code = this.txtCode.Text;
                MessageBox.Show(this.cc + " " + this.phone + " " + code);
                this.password = WhatsAppApi.Register.WhatsRegisterV2.RegisterCode(this.number, code);
                btnRegisterCode.Enabled = true;
                MessageBox.Show(this.cc + " " + this.phone + " " + code + " " + this.password);
                if (!String.IsNullOrEmpty(this.password))
                {
                    this.OnReceivePassword();
                }
            }
        }

        private void OnReceivePassword()
        {
            Form1 f1 = new Form1();
            this.txtOutput.Text = String.Format("Found password\r\n" + this.password + "\r\n\r\nPlease enter this at key!");
            f1.Key.Text = this.password;
            f1.Phonenumber.Text = this.number;
            this.grpStep1.Enabled = false;
            this.grpStep2.Enabled = false;
            this.grpResult.Enabled = true;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Registrate_Load(object sender, EventArgs e)
        {
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
