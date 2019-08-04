using System;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using pretest.cbrServ;

namespace pretest
{
    public partial class mainForm : Form
    {
        private string defaultSelectedCur = "USD\tДоллар США";
        DailyInfo di;
        XmlNode curs;
        public mainForm()
        {
            InitializeComponent();

            di = new DailyInfo();
            curs = di.GetCursOnDateXML(DateTime.Today);

            foreach (XmlNode node in curs.ChildNodes)
            {
                CursListBox.Items.Add(node["VchCode"].InnerText + "\t" + node["Vname"].InnerText.Trim());
            }

            CursListBox.SelectedItem = defaultSelectedCur;
        }

        private void MainForm_MouseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "0";
                textBox2.Text = "0";
            }

            foreach (XmlNode node in curs)
            {
                if (node["VchCode"].InnerText + "\t" + node["Vname"].InnerText.Trim() == CursListBox.SelectedItem.ToString())
                    textBox2.Text = (float.Parse(node["Vcurs"].InnerText, CultureInfo.InvariantCulture)
                                    * float.Parse(textBox1.Text, CultureInfo.InvariantCulture)).ToString();
            }
        }

        private void CursListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (XmlNode node in curs)
            {
                if (node["VchCode"].InnerText + "\t" + node["Vname"].InnerText.Trim() == CursListBox.SelectedItem.ToString())
                {
                    textBox2.Text = (float.Parse(node["Vcurs"].InnerText, CultureInfo.InvariantCulture)
                                    * float.Parse(textBox1.Text, CultureInfo.InvariantCulture)).ToString();
                    label2.Text = node["VchCode"].InnerText;
                }
            }
            
        }
    }
}
