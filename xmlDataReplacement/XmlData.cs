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
        string xmlUrlVaryasyonluKarg10 = "https://karg10.com/disari-al/l2h3sdb3/55";
        #endregion

        #region PATH

        private string xmlPathVaryasyonsuzKarg10 = "varyasyonsuzKarg10.xml";
        string xmlPathVaryasyonluKarg10 = "varyasyonluKarg10.xml";
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
                }

                // Xml verisini okuma, değiştirme ve kaydetme işlemi
                #region Varyasyonsuz

                Varyasyonsuz(xmlUrlVaryasyonsuzKarg10, xmlPathVaryasyonsuzKarg10);

                #endregion

                #region Varyasyonlu

                Varyasyonlu(xmlUrlVaryasyonluKarg10, xmlPathVaryasyonluKarg10);

                #endregion

                string ftpUrl = "ftp://ftp.sygstore.com.tr/public_html/XML/";
                string ftpUsername = "u1292730";
                string ftpPassword = "15963VeksiS-";

                // FTP Klasörüne yükleme işlemi

                #region FTP
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
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
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
    }
}

