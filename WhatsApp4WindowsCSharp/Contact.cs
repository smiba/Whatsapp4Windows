using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WhatsApp4WindowsCSharp
{
    public partial class Contact : Form
    {
        String nameString;
        String statusString;
        String phoneString;
        String imageString;
        public Contact(string name1, string status1, string phone1, string image1)
        {
            nameString = name1;
            statusString = status1;
            phoneString = "+" + phone1;
            imageString = image1;
            InitializeComponent();
        }

        private void Contact_Load(object sender, EventArgs e)
        {
            PhoneNumber.Text = phoneString;
            Nametxt.Text = nameString;
            Status.Text = statusString;
            try
            {
                byte[] Image = System.Convert.FromBase64String(imageString);
                Picture.Image = ByteToImage(Image);
            }
            catch
            {
                //Kek, so there is no image?
            }
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }
    }
}
