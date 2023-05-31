using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace xmlDataReplacement
{
    public partial class XmlData : Form
    {
        public XmlData()
        {
            InitializeComponent();
        }

        private void TSMI_xmlTedarik_Click(object sender, EventArgs e)
        {
            string xmlPath = @"C:\Users\HsnHs\source\repos\XmlVeriDonustur\XmlVeriDonustur\bin\Debug\file.xml";
            DataSet dataSet = new DataSet();

            try
            {
                dataSet.ReadXml(xmlPath);

                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow row in dataTable.Rows)
                {
                    string barcode = row["barcode"].ToString();
                    string brand = row["brand"].ToString();

                    row["barcode"] = "SYG-" + barcode;
                    row["brand"] = "SYG-" + brand;
                }

                dgv_list.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML dosyası okunurken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
