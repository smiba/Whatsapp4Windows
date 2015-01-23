//TODO:
//
// 1. Group Info (Gebruikers in de groep etc.)
// 2. Maps ontvangen (Link naar google maps?)
// 3. Voice kunnen ontvangen (Ik gok dat je een acc / mp3 / m4a ontvangt en daar naar toe kan linken)
// 4. Pictures verzenden (Simple file selector - Let op het controleren van bestanden zodat het zeker goed gaat!)
//


using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using WhatsAppApi;
using WhatsAppApi.Account;
using WhatsAppApi.Helper;
using WhatsAppApi.Register;
using WhatsAppApi.Response;

namespace WhatsApp4WindowsCSharp{

public partial class Form1 : Form
    {

        private class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        static WhatsApp wa;
        static Thread thRecv;
        public string number;
        protected string cc;
        protected string phone;
        public string password;
        public string LastMessage;
        public int messageid = 0;
        



        public Form1()
        {
            InitializeComponent();
        }

        public void Login()
        {
            var tmpEncoding = Encoding.UTF8;
            string nickname = NicknameString.Text;
            string senderphone = Phonenumber.Text; // Mobile number with country code (but without + or 00)
            string password = Key.Text;//v2 password
            string target = Destphone.Text;// Mobile number to send the message to
            wa = new WhatsApp(senderphone, password, nickname, true);
            //event bindings
            wa.OnLoginSuccess += wa_OnLoginSuccess;
            wa.OnLoginFailed += wa_OnLoginFailed;
            wa.OnGetMessage += wa_OnGetMessage;
            wa.OnGetMessageReceivedClient += wa_OnGetMessageReceivedClient;
            wa.OnGetMessageReceivedServer += wa_OnGetMessageReceivedServer;
            wa.OnNotificationPicture += wa_OnNotificationPicture;
            wa.OnGetPresence += wa_OnGetPresence;
            wa.OnGetGroupParticipants += wa_OnGetGroupParticipants;
            wa.OnGetLastSeen += wa_OnGetLastSeen;
            wa.OnGetTyping += wa_OnGetTyping;
            wa.OnGetPaused += wa_OnGetPaused;
            wa.OnGetMessageImage += wa_OnGetMessageImage;
            wa.OnGetMessageAudio += wa_OnGetMessageAudio;
            wa.OnGetMessageVideo += wa_OnGetMessageVideo;
            wa.OnGetMessageLocation += wa_OnGetMessageLocation;
            wa.OnGetMessageVcard += wa_OnGetMessageVcard;
            wa.OnGetPhoto += wa_OnGetPhoto;
            wa.OnGetPhotoPreview += wa_OnGetPhotoPreview;
            wa.OnGetGroups += wa_OnGetGroups;
            wa.OnGetSyncResult += wa_OnGetSyncResult;
            wa.OnGetStatus += wa_OnGetStatus;
            
            thRecv = new Thread(t =>
            {
            try
            {
                while (wa != null)
                {
                    wa.PollMessages();
                    Thread.Sleep(100);
                    continue;
                }

            }
            catch (ThreadAbortException)
            {
                
            }
        }) { IsBackground = true };

            wa.Connect();

            string datFile = getDatFileName(senderphone);
            byte[] nextChallenge = null;
            if (File.Exists(datFile))
            {
                try
                {
                    string foo = File.ReadAllText(datFile);
                    nextChallenge = Convert.FromBase64String(foo);
                }
                catch (Exception) { };
            }

            wa.Login(nextChallenge);
            wa.SendGetGroups();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            Login();
        }
        static void wa_OnGetStatus(string from, string type, string name, string status)
        {
            SQLiteDatabase db = new SQLiteDatabase();
            string query = "INSERT INTO CONTACTSTATUS(PHONE, STATUS) VALUES ('" + from + "', '" + status + "');";
            db.ExecuteNonQuery(query);
        }

        static string getDatFileName(string pn)
        {
            string filename = string.Format("{0}.next.dat", pn);
            return Path.Combine(Directory.GetCurrentDirectory(), filename);
        }

        static void wa_OnGetSyncResult(int index, string sid, Dictionary<string, string> existingUsers, string[] failedNumbers)
        {
        //    Console.WriteLine("Sync result for {0}:", sid);
            foreach (KeyValuePair<string, string> item in existingUsers)
            {
        //       Console.WriteLine("Existing: {0} (username {1})", item.Key, item.Value);
            }
            foreach (string item in failedNumbers)
            {
        //        Console.WriteLine("Non-Existing: {0}", item);
            }
        }

        public void wa_OnGetGroups(WaGroupInfo[] groups)
        {
            foreach (WaGroupInfo info in groups)
            {
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                String JID = "";
                String Creator = "";
                int count = 0;
                string[] JIDCreator = info.id.Split('-');
                foreach (string Idthing in JIDCreator)
                {
                    if (count == 0){
                        Creator = Idthing;
                    }else if (count == 1){
                        JID = Idthing;
                    }
                    count++;
                }
                data.Add("JID", JID);
                data.Add("CREATOR", Creator);
                String title = info.subject.Replace("'", "''");
                data.Add("TITLE", title);
                try
                {
                    db.Insert("GROUPS", data);
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Contacts.Items.Add(info.subject); });
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }
        }

        static void wa_OnGetPhotoPreview(string from, string id, byte[] data)
        {
        //    Console.WriteLine("Got preview photo for {0}", from);
        //    File.WriteAllBytes(string.Format("preview_{0}.jpg", from), data);
        }

        public void wa_OnGetPhoto(string from, string id, byte[] data)
        {
        //    Console.WriteLine("Got full photo for {0}", from);
        //    File.WriteAllBytes(string.Format("{0}.jpg", from), data);
        //    ImageConverter ic = new ImageConverter();
        //    Image img = (Image)ic.ConvertFrom(data);
        //    Bitmap bitmap = new Bitmap(img);
        //    Clipboard.SetImage(img);
        //    Clipboard.Clear();
            string picture = System.Convert.ToBase64String(data);
            SQLiteDatabase db = new SQLiteDatabase();
            string query = "INSERT INTO CONTACTPICTURE(PHONE, PICTURE) VALUES ('" + from + "', '" + picture + "');";
            db.ExecuteNonQuery(query);
        }

        static void wa_OnGetMessageVcard(ProtocolTreeNode vcardNode, string from, string id, string name, byte[] data)
        {
         //   Console.WriteLine("Got vcard \"{0}\" from {1}", name, from);
         //   File.WriteAllBytes(string.Format("{0}.vcf", name), data);
        }

        static void wa_OnGetMessageLocation(ProtocolTreeNode locationNode, string from, string id, double lon, double lat, string url, string name, byte[] preview)
        {
         //   Console.WriteLine("Got location from {0} ({1}, {2})", from, lat, lon);
            if (!string.IsNullOrEmpty(name))
            {
         //       Console.WriteLine("\t{0}", name);
            }
         //   File.WriteAllBytes(string.Format("{0}{1}.jpg", lat, lon), preview);
        }

        static void wa_OnGetMessageVideo(ProtocolTreeNode mediaNode, string from, string id, string fileName, int fileSize, string url, byte[] preview)
        {
         //   Console.WriteLine("Got video from {0}", from, fileName);
         //   OnGetMedia(fileName, url, preview);
        }

        static void OnGetMedia(string file, string url, byte[] data)
        {
            //save preview
            //File.WriteAllBytes(Path.GetTempPath() + string.Format("preview_{0}.jpg", file), data);
            //download
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFileAsync(new Uri(url), Path.GetTempPath() + file, null);
            //}
        }

        static void wa_OnGetMessageAudio(ProtocolTreeNode mediaNode, string from, string id, string fileName, int fileSize, string url, byte[] preview)
        {
         //   Console.WriteLine("Got audio from {0}", from, fileName);
         //   OnGetMedia(fileName, url, preview);
        }

        [STAThread]
        public void wa_OnGetMessageImage(ProtocolTreeNode mediaNode, string from, string id, string fileName, int size, string url, byte[] preview)
        {
         //   Console.WriteLine("Got image from {0}", from, fileName);
            var pos = from.LastIndexOf('@');
            String fromuser;
            if (pos >= 0)
            {
                fromuser = from.Substring(0, pos);
            }
            else
            {
                fromuser = from;
            }
            if (from.Contains("g.us"))
            {
                //Group message
                String JID = "";
                String Sender = "";
                int count = 0;
                string[] JIDCreator = fromuser.Split('-');
                foreach (string Idthing in JIDCreator)
                {
                    if (count == 0)
                    {
                        Sender = Idthing;
                    }
                    else if (count == 1)
                    {
                        JID = Idthing;
                    }
                    count++;
                }
                if (Destphone.Text == JID)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.AppendText("(" + Sender + ") Sent you a image: " + url + Environment.NewLine); });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
                }
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("JID", JID);
                data.Add("SENDER", Sender);
                data.Add("MESSAGE", "Sent you a image: " + url);
                data.Add("DATE", ((int)ts.TotalSeconds).ToString());
                try
                {
                    db.Insert("GROUPCHAT", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }
            else
            {
                //Normal message
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.AppendText("(" + fromuser + ") Sent you a image: " + url + Environment.NewLine); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("ID", messageid.ToString());
                data.Add("USER", fromuser);
                data.Add("MESSAGE", "Sent you a image: " + url);
                data.Add("DATE", ((int)ts.TotalSeconds).ToString());
                messageid = messageid++;
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { messageid = messageid + 1; });
                try
                {
                    db.Insert("MESSAGES", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }
        }

        private void ChatLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public void wa_OnGetPaused(string from)
        {
            var pos = from.LastIndexOf('@');
            String fromuser;
            if (pos >= 0)
            {
                fromuser = from.Substring(0, pos);
            }
            else
            {
                fromuser = from;
            }
            if (Destphone.Text == fromuser)
            {
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { deststatus.Text = "Active"; });
            }
        }

        public void wa_OnGetTyping(string from)
        {
            var pos = from.LastIndexOf('@');
            String fromuser;
            if (pos >= 0)
            {
                fromuser = from.Substring(0, pos);
            }
            else
            {
                fromuser = from;
            }
            if (Destphone.Text == fromuser)
            {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { deststatus.Text = "Typing"; });
            }
        }

        public void wa_OnGetLastSeen(string from, DateTime lastSeen)
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { deststatus.Text = ("Last Seen at " + lastSeen); });
        }

        public void wa_OnGetMessageReceivedServer(string from, string id)
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { MsgStat.Text = "Received By Server (1/2)"; });
        }

        public void wa_OnGetMessageReceivedClient(string from, string id)
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { MsgStat.Text = "Received By Client (2/2)"; });
        }

        static void wa_OnGetGroupParticipants(string gjid, string[] jids)
        {
         //   Console.WriteLine("Got participants from {0}:", gjid);
            foreach (string jid in jids)
            {
          //      Console.WriteLine("\t{0}", jid);
            }
        }

        static void wa_OnGetPresence(string from, string type)
        {
          //  Console.WriteLine("Presence from {0}: {1}", from, type);
        }

        static void wa_OnNotificationPicture(string type, string jid, string id)
        {
            //TODO
            //throw new NotImplementedException();
        }

        public void wa_OnGetMessage(ProtocolTreeNode node, string from, string id, string name, string message, bool receipt_sent)
        {
            var pos = from.LastIndexOf('@');
            String fromuser;
            if (pos >= 0)
            {
                fromuser = from.Substring(0, pos);
            }
            else
            {
                fromuser = from;
            }
            if (from.Contains("g.us")){
                //Group message
                String JID = "";
                String Sender = node.NodeString();
                Sender = Sender.Substring(Sender.IndexOf("participant=") + 13);
                Sender = Sender.Substring(0, Sender.LastIndexOf("@"));
                int count = 0;
                string[] JIDCreator = fromuser.Split('-');
                foreach (string Idthing in JIDCreator)
                {
                    if (count == 1)
                    {
                        JID = Idthing;
                    }
                    count++;
                }
                if (Destphone.Text == JID)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.AppendText("(" + Sender + "): " + message + Environment.NewLine); });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
                }
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                SQLiteDatabase db = new SQLiteDatabase();
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("JID", JID);
                data.Add("SENDER", Sender);
                message = message.Replace("'", "''");
                data.Add("MESSAGE", message);
                data.Add("DATE", ((int)ts.TotalSeconds).ToString());
                try
                {
                    db.Insert("GROUPCHAT", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }else{
                //Normal message
            if (Destphone.Text == fromuser)
            {
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.AppendText("(" + fromuser + "): " + message + Environment.NewLine); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
            }
            DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
            TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT

            SQLiteDatabase db = new SQLiteDatabase();
            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("ID", messageid.ToString());
            data.Add("USER", fromuser);
            data.Add("USERTO", "You");
            message = message.Replace("'", "''");
            data.Add("MESSAGE", message);
            data.Add("DATE", ((int)ts.TotalSeconds).ToString());
            messageid = messageid++;
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { messageid = messageid + 1; });
            try
            {
                db.Insert("MESSAGES", data);
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
            }
        }

        private static void wa_OnLoginFailed(string data)
        {
            MessageBox.Show("Login Failed");
        }

        public void wa_OnLoginSuccess(string phoneNumber, byte[] data)
        {
            Button3.Text = "Logged in";
            Button3.Enabled = false;
            string sdata = Convert.ToBase64String(data);
            //Console.WriteLine(sdata);
            try
            {
                File.WriteAllText(getDatFileName(phoneNumber), sdata);
            }
            catch (Exception) { }
            thRecv.Start();
            secondecheck.Enabled = true;
            secondecheck.Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        public void Button2_Click(object sender, EventArgs e)
        {
            Registrate Registrate = new Registrate();
            Registrate.Show();

        }

        private void Checktiepen_Tick(object sender, EventArgs e)
        {
            WhatsUserManager usrMan = new WhatsUserManager();
            var tmpUser = usrMan.CreateUser(Destphone.Text);
            if (LastMessage != Message.Text)
            {
                wa.SendComposing(tmpUser.GetFullJid());
            }
            else
            {
                wa.SendPaused(tmpUser.GetFullJid());
            }
            LastMessage = Message.Text;
        }

        private void ChatLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChatLog.LinkClicked += ChatLog_LinkClicked;
            SQLiteDatabase db = new SQLiteDatabase();
            DataTable querytable;
            String query;
            try
            {
                query = "SELECT ID from MESSAGES ORDER BY ID DESC LIMIT 0, 1;";
                querytable = db.GetDataTable(query);
                query = "DROP TABLE GROUPS";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE GROUPS(JID int(16), CREATOR bigint, TITLE varchar(512));";
                db.ExecuteNonQuery(query);
                //For testing contact information receiving (REMOVE BEFORE REALEASE / IMPLEMENATION OF ADDRESS BOOK!)
                query = "DROP TABLE IF EXISTS CONTACTS";
                db.ExecuteNonQuery(query);
                query = "DROP TABLE IF EXISTS CONTACTSTATUS";
                db.ExecuteNonQuery(query);
                query = "DROP TABLE IF EXISTS CONTACTPICTURE";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTS(NAME varchar, PHONE bigint);";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTSTATUS(PHONE bigint, STATUS varchar(4096));";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTPICTURE(PHONE bigint, PICTURE longtext);";
                db.ExecuteNonQuery(query);
            }
            catch (Exception fail)
            {
                String error = "Something went horrbly wrong reading the database!\n The database will be ereased and recreated\n\nThe error was:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error, "Whoops...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                query = "CREATE TABLE IF NOT EXISTS MESSAGES(ID int, USER varchar(30), USERTO varchar(30), MESSAGE varchar(4096), DATE int);";
                db.ExecuteNonQuery(query);
                query = "DROP TABLE IF EXISTS GROUPS";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE GROUPS(JID int, CREATOR bigint, TITLE varchar(512));";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS GROUPCHAT(JID int(16), SENDER varchar(30), MESSAGE varchar(4096), DATE int);";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTS(NAME varchar, PHONE bigint);";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTSTATUS(PHONE bigint, STATUS varchar(4096));";
                db.ExecuteNonQuery(query);
                query = "CREATE TABLE IF NOT EXISTS CONTACTPICTURE(PHONE bigint, PICTURE longtext);";
                db.ExecuteNonQuery(query);
                //this.Close();
            }

            //Login(); You don't want this without prefilled in login details - key and stuff

            AddContact("Contact Number one", "123456789"); //EDIT THIS! Or even better; finally make a function for it!

            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Contacts.Items.Clear(); });
            query = "SELECT * from CONTACTS";
            querytable = db.GetDataTable(query);

            int intcontacts = 0;
            foreach (DataRow results in querytable.Rows)
            {
                intcontacts++;
            }

            string[] contactarray;
            contactarray = new string[intcontacts];
            int currentlocation = 0;
            foreach (DataRow results in querytable.Rows)
            {
                contactarray[currentlocation] = results["NAME"].ToString();
                currentlocation++;
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Contacts.Items.Add(results["NAME"].ToString()); });
            }
    
            //wa.SendSync(contactarray); You don't want this without prefilled in login details AND a SUCCESFULL connection
        }

        public void AddContact(string name, string phone)
        {
            SQLiteDatabase db = new SQLiteDatabase();
            String query;
            try
            {
                query = "SELECT * from CONTACTS WHERE PHONE='" + phone + "';";
                DataTable querytable;
                querytable = db.GetDataTable(query);
                Boolean result = false;
                foreach (DataRow results in querytable.Rows)
                {
                    result = true;
                }
                if (result == false)
                {
                    query = "INSERT INTO CONTACTS(NAME, PHONE) VALUES ('" + name + "', '" + phone + "');";
                    db.ExecuteNonQuery(query);
                    WhatsUserManager usrMan = new WhatsUserManager();
                    var tmpUser = usrMan.CreateUser(phone);
                    wa.SendGetPhoto(tmpUser.GetFullJid(), null, true);
                    wa.SendGetStatuses(new string[] { tmpUser.GetFullJid() });
                }
            }
            catch (Exception fail)
            {
                //Make sure Contact is not added?
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                String query = "SELECT * from GROUPS WHERE TITLE='" + Contacts.Text + "'";
                DataTable querytable;
                querytable = db.GetDataTable(query);
                Boolean result = false;
                String JID = "0";
                foreach (DataRow r in querytable.Rows)
                {
                    result = true;
                    JID = r["JID"].ToString();
                }
                ChatLog.Clear();
                if (result == true)
                {
                    //Yeah! Its a group!
                    Destphone.Text = JID;
                    InfoButton.Enabled = false;
                    query = "SELECT * from GROUPCHAT WHERE JID='" + JID + "' ORDER BY DATE;";
                    querytable = db.GetDataTable(query);
                    foreach (DataRow r in querytable.Rows)
                    {
                        ChatLog.AppendText("(" + r["SENDER"].ToString() + "): " + r["MESSAGE"].ToString() + Environment.NewLine);
                    }
                    ChatLog.ScrollToCaret();
                    deststatus.Text = "[Group]";
                }
                else
                {
                    //Nope, regulair message detected
                    InfoButton.Enabled = true;
                    query = "SELECT PHONE from CONTACTS WHERE NAME='" + Contacts.Text + "';";
                    querytable = db.GetDataTable(query);
                    foreach (DataRow r in querytable.Rows)
                    {
                        Destphone.Text = r["PHONE"].ToString();
                    }
                    query = "SELECT * from MESSAGES WHERE USER='" + Destphone.Text + "' OR USERTO='" + Destphone.Text + "' ORDER BY DATE;";
                    querytable = db.GetDataTable(query);
                    foreach (DataRow r in querytable.Rows)
                    {
                        ChatLog.AppendText("(" + r["USER"].ToString() + "): " + r["MESSAGE"].ToString() + Environment.NewLine);
                    }
                    ChatLog.ScrollToCaret();
                    deststatus.Text = "[Status?]";
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        private void Destphone_TextChanged(object sender, EventArgs e)
        {
            if (Destphone.Text != ""){
                SendButton.Enabled = true;
            }else{
                SendButton.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Contacts.Text != "")
            {
                String nameString = "";
                String statusString = "";
                String phoneString = Destphone.Text;
                String imageString = "";
                String JID = Destphone.Text + "@s.whatsapp.net";
                SQLiteDatabase db = new SQLiteDatabase();
                String query = "SELECT * from CONTACTSTATUS WHERE PHONE='" + JID + "' LIMIT 1;";
                DataTable querytable = db.GetDataTable(query);
                foreach (DataRow r in querytable.Rows)
                {
                    statusString = r["STATUS"].ToString();
                }
                query = "SELECT * from CONTACTPICTURE WHERE PHONE='" + JID + "' LIMIT 1;";
                querytable = db.GetDataTable(query);
                foreach (DataRow r in querytable.Rows)
                {
                    imageString = r["PICTURE"].ToString();
                }
                query = "SELECT * from CONTACTS WHERE PHONE='" + phoneString + "' LIMIT 1;";
                querytable = db.GetDataTable(query);
                foreach (DataRow r in querytable.Rows)
                {
                    nameString = r["NAME"].ToString();
                }
                Contact contactForm = new Contact(nameString, statusString, phoneString, imageString);
                contactForm.Show();
                //Get contact info and open new window to show
            }
        }

        private void Message_KeyDown(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendMessage();   
            }
        }

        private void SendMessage()
        {
            SQLiteDatabase db = new SQLiteDatabase();
            String query = "SELECT * from GROUPS WHERE JID='" + Destphone.Text + "'";
            DataTable querytable;
            querytable = db.GetDataTable(query);
            Boolean result = false;
            String JID = "0";
            String Owner = "0";
            foreach (DataRow results in querytable.Rows)
            {
                result = true;
                JID = results["JID"].ToString();
                Owner = results["CREATOR"].ToString();
            }
            if (result == true)
            {
                //Yeah! Its a group!
                wa.SendMessage(Owner + "-" + JID, Message.Text);
                ChatLog.AppendText("(You): " + Message.Text + Environment.NewLine);
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("JID", JID);
                data.Add("SENDER", "You");
                String message = Message.Text.Replace("'", "''");
                data.Add("MESSAGE", message);
                data.Add("DATE", ((int)ts.TotalSeconds).ToString());
                try
                {
                    db.Insert("GROUPCHAT", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }
            else
            {
                WhatsUserManager usrMan = new WhatsUserManager();
                var tmpUser = usrMan.CreateUser(Destphone.Text);
                wa.SendMessage(tmpUser.GetFullJid(), Message.Text);
                ChatLog.AppendText("(You): " + Message.Text + Environment.NewLine);
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.Select(ChatLog.Text.Length, 0); });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.SelectionStart = ChatLog.Text.Length; });
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { ChatLog.ScrollToCaret(); });
                DateTime nx = new DateTime(1970, 1, 1); // UNIX epoch date
                TimeSpan ts = DateTime.UtcNow - nx; // UtcNow, because timestamp is in GMT
                Dictionary<String, String> data = new Dictionary<String, String>();
                data.Add("ID", messageid.ToString());
                data.Add("USER", "You");
                data.Add("USERTO", Destphone.Text);
                String message = Message.Text.Replace("'", "''");
                data.Add("MESSAGE", message);
                data.Add("DATE", ((int)ts.TotalSeconds).ToString());
                messageid++;
                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { messageid = messageid + 1; });
                try
                {
                    db.Insert("MESSAGES", data);
                }
                catch (Exception crap)
                {
                    MessageBox.Show(crap.Message);
                }
            }
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Message.Text = ""; });
        }
    }
}
