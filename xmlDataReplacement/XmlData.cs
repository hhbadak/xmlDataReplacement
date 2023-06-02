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
            timer.Interval = 60000;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Xml verisi 
            using (WebClient client = new WebClient())
            {
                // Xml verisini indirme ve kaydetme işlemi
                client.DownloadFile(xmlUrlVaryasyonluKarg10, xmlPathVaryasyonluKarg10);
                client.DownloadFile(xmlUrlVaryasyonsuzKarg10, xmlPathVaryasyonsuzKarg10);

                // Kullanıcıya indirme işleminin gerçekleştiğinin bildirimi
                MessageBox.Show("Xml verisi başarıyla indirildi ve kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Xml verisini okuma değiştirme ve kaydetme işlemi
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlPathVaryasyonluKarg10);
                dataSet.ReadXml(xmlPathVaryasyonsuzKarg10);

                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow row in dataTable.Rows)
                {
                    string barcode = row["barcode"].ToString();
                    string brand = row["brand"].ToString();

                    row["barcode"] = "SYG-" + barcode;
                    row["brand"] = "SYG-" + brand;
                }

                dataSet.WriteXml(xmlPathVaryasyonluKarg10);
                dataSet.WriteXml(xmlPathVaryasyonsuzKarg10);

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("u1292730", "15963VeksiS-");
                    client.UploadFile("ftp://ftp.sygstore.com.tr/public_html/XML/xmlPathVaryasyonluKarg10.xml", WebRequestMethods.Ftp.UploadFile, xmlPathVaryasyonluKarg10);
                    client.UploadFile("ftp://ftp.sygstore.com.tr/public_html/XML/xmlPathVaryasyonsuzKarg10.xml", WebRequestMethods.Ftp.UploadFile, xmlPathVaryasyonsuzKarg10);
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda e-posta gönderme
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

            timer.Stop();
            timer.Dispose();
        }
    }
}
