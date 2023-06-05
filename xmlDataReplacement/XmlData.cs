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

        #region URL

        private string xmlUrlVaryasyonsuzKarg10 = "https://karg10.com/disari-al/qff7cm9h/54";
        private string mvsnXmlUrlVaryasyonsuzKarg10 = "https://karg10.com/disari-al/qff7cm9h/54";
        string xmlUrlVaryasyonluKarg10 = "https://karg10.com/disari-al/l2h3sdb3/55";
        string mvsnXmlUrlVaryasyonluKarg10 = "https://karg10.com/disari-al/l2h3sdb3/55";
        string xmlCekUrl = "https://www.xmlcek.com/wp-content/uploads/woo-feed/custom/xml/xmllink.xml";
        string teknoTokUrl = "https://teknotok.com/wp-load.php?security_token=b3203d92b4e495eb&export_id=3&action=get_data";
        string xmlTedarikUrl = "https://www.xmltedarik.com/export/1/1488S4586M1488";
        #endregion

        #region PATH

        private string xmlPathVaryasyonsuzKarg10 = "varyasyonsuzKarg10.xml";
        private string mvsnXmlPathVaryasyonsuzKarg10 = "mvsnVaryasyonsuzKarg10.xml";
        string xmlPathVaryasyonluKarg10 = "varyasyonluKarg10.xml";
        string mvsnXmlPathVaryasyonluKarg10 = "mvsnVaryasyonluKarg10.xml";
        string xmlCekPath = "xmlCek.xml";
        string teknoTokPath = "teknoTok.xml";
        string xmlTedarikPath = "xmlTedarik.xml";
        #endregion

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
                    client.DownloadFile(xmlUrlVaryasyonsuzKarg10, xmlPathVaryasyonsuzKarg10);
                    client.DownloadFile(xmlUrlVaryasyonluKarg10, xmlPathVaryasyonluKarg10);
                    client.DownloadFile(mvsnXmlUrlVaryasyonsuzKarg10, mvsnXmlPathVaryasyonsuzKarg10);
                    client.DownloadFile(mvsnXmlUrlVaryasyonluKarg10, mvsnXmlPathVaryasyonluKarg10);
                    client.DownloadFile(xmlCekUrl, xmlCekPath);
                    client.DownloadFile(teknoTokUrl, teknoTokPath);
                    client.DownloadFile(xmlTedarikUrl, xmlTedarikPath);
                }

                // Xml verisini okuma, değiştirme ve kaydetme işlemi
                #region Varyasyonsuz

                Varyasyonsuz(xmlUrlVaryasyonsuzKarg10, xmlPathVaryasyonsuzKarg10);

                #endregion

                #region Varyasyonlu

                Varyasyonlu(xmlUrlVaryasyonluKarg10, xmlPathVaryasyonluKarg10);

                #endregion

                #region mvsnVaryasyonsuz

                mvsnVaryasyonsuz(mvsnXmlUrlVaryasyonsuzKarg10, mvsnXmlPathVaryasyonsuzKarg10);

                #endregion

                #region mvsnVaryasyonlu

                mvsnVaryasyonlu(mvsnXmlUrlVaryasyonluKarg10, mvsnXmlPathVaryasyonluKarg10);

                #endregion

                #region xmlCek

                xmlCek(xmlCekUrl, xmlCekPath);

                #endregion

                #region Teknotok

                teknoTok(teknoTokUrl, teknoTokPath);
                #endregion

                #region xmlTedarik
                xmlTedarik(xmlTedarikUrl, xmlTedarikPath);
                #endregion

                string ftpUrl = "ftp://ftp.sygstore.com.tr/public_html/XML/";
                string ftpUsername = "u1292730";
                string ftpPassword = "15963VeksiS-";

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.UploadFile(ftpUrl + xmlPathVaryasyonsuzKarg10, WebRequestMethods.Ftp.UploadFile, xmlPathVaryasyonsuzKarg10);
                }
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.UploadFile(ftpUrl + xmlPathVaryasyonluKarg10, WebRequestMethods.Ftp.UploadFile, xmlPathVaryasyonluKarg10);
                }
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
        public static void Varyasyonlu(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = "SYG-" + product.Element("barcode").Value;
                product.Element("product_brand").Value = "SYG-" + product.Element("product_brand").Value;
                foreach (XElement variant in product.Elements("variants").Elements())
                {
                    variant.Element("barcode").Value = "SYG-" + variant.Element("barcode").Value;
                    variant.Element("product_brand").Value = "SYG-" + variant.Element("product_brand").Value;
                }
            }

            doc.Save(savepath);
        }
        public static void Varyasyonsuz(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = "SYG-" + product.Element("barcode").Value;
                product.Element("product_brand").Value = "SYG-" + product.Element("product_brand").Value;
            }

            doc.Save(savepath);
        }
        public static void mvsnVaryasyonlu(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = "MVSN-" + product.Element("barcode").Value;
                product.Element("product_brand").Value = "MVSN-" + product.Element("product_brand").Value;
                foreach (XElement variant in product.Elements("variants").Elements())
                {
                    variant.Element("barcode").Value = "MVSN-" + variant.Element("barcode").Value;
                    variant.Element("product_brand").Value = "MVSN-" + variant.Element("product_brand").Value;
                }
            }

            doc.Save(savepath);
        }
        public static void mvsnVaryasyonsuz(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = "MVSN-" + product.Element("barcode").Value;
                product.Element("product_brand").Value = "MVSN-" + product.Element("product_brand").Value;
            }

            doc.Save(savepath);
        }
        public static void xmlCek(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = $"MVSN- {product.Element("Barcode").Value}";
                product.Element("Marka").Value = $"MVSN- {product.Element("Marka").Value}";
            }

            doc.Save(savepath);
        }
        public static void teknoTok(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach(XElement product in root.Elements())
            {
                product.Element("Sku").Value = $"MVSN- {product.Element("Sku").Value}";
                product.Element("Markalar").Value = $"MVSN- {product.Element("Markalar").Value}";
            }
            doc.Save(savepath);
        }
        public static void xmlTedarik(string url, string savepath)
        {
            var doc = XDocument.Load(url);
            XElement root = doc.Root;

            foreach (XElement product in root.Elements())
            {
                product.Element("barcode").Value = $"MVSN- {product.Element("barcode").Value}";
                product.Element("brand").Value = $"MVSN- {product.Element("brand").Value}";
            }
            doc.Save(savepath);
        }
    }
}

