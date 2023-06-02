using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace xmlDataReplacement
{
    public partial class XmlData : Form
    {
        private Timer timer;
        private string xmlUrlVaryasyonluKarg10 = "https://karg10.com/disari-al/l2h3sdb3/55";
        private string xmlUrlVaryasyonsuzKarg10 = "https://karg10.com/disari-al/qff7cm9h/54";
        private string xmlPathVaryasyonluKarg10 = "varyasyonluKarg10.xml";
        private string xmlPathVaryasyonsuzKarg10 = "varyasyonsuzKarg10.xml";

        public XmlData()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 30 * 60 * 1000; 
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Xml verisi indirme ve kaydetme işlemi
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(xmlUrlVaryasyonluKarg10, xmlPathVaryasyonluKarg10);
                    client.DownloadFile(xmlUrlVaryasyonsuzKarg10, xmlPathVaryasyonsuzKarg10);
                }

                // Xml verisini okuma, değiştirme ve kaydetme işlemi
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlPathVaryasyonluKarg10);
                dataSet.ReadXml(xmlPathVaryasyonsuzKarg10);

                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow row in dataTable.Rows)
                {
                    string barcode = row["barcode"].ToString();
                    string brand = row["product_brand"].ToString();

                    row["barcode"] = "SYG-" + barcode;
                    row["product_brand"] = "SYG-" + brand;
                }

                dataSet.WriteXml(xmlPathVaryasyonluKarg10);
                dataSet.WriteXml(xmlPathVaryasyonsuzKarg10);

                string ftpUrl = "ftp://ftp.sygstore.com.tr/public_html/XML/";
                string ftpUsername = "u1292730";
                string ftpPassword = "15963VeksiS-";

                using (var fileStream = File.OpenRead(xmlPathVaryasyonluKarg10))
                {
                    var ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl + xmlPathVaryasyonluKarg10);
                    ftpRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                    using (var ftpStream = ftpRequest.GetRequestStream())
                    {
                        fileStream.CopyTo(ftpStream);
                    }
                }

                using (var fileStream = File.OpenRead(xmlPathVaryasyonsuzKarg10))
                {
                    var ftpRequest = (FtpWebRequest)WebRequest.Create(ftpUrl + xmlPathVaryasyonsuzKarg10);
                    ftpRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                    using (var ftpStream = ftpRequest.GetRequestStream())
                    {
                        fileStream.CopyTo(ftpStream);
                    }
                }
                timer.Stop();
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);

                //string username = "iletisim@sygstore.com.tr";
                //string password = "15963VeksiS-";
                //MailMessage ePosta = new MailMessage();
                //ePosta.From = new MailAddress("iletisim@sygstore.com.tr");
                //ePosta.To.Add(new MailAddress("HsnHsyn_Esk@hotmail.com"));
                //ePosta.Subject = "XML Dosyası güncellenirken bir hata oluştu";
                //ePosta.Body = "Hata: " + ex.Message;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Credentials = new NetworkCredential(username, password);
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Port = 465;
                //smtp.Host = "mail.sygstore.com.tr";

                //ServicePointManager.ServerCertificateValidationCallback += (s, certificate, chain, sslPolicyErrors) => true;

                //smtp.Send(ePosta);
            }
        }
    }
}

