using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Farcards
{
    public partial class XmlViewer : Form
    {
        private XmlViewer()
        {
            InitializeComponent();
        }

        public static void ShowXml(string xml)
        {
            XmlViewer xmlViewer = new XmlViewer();

            xmlViewer.richTextBox1.Text = xml;
            xmlViewer.ShowDialog();
        }
    }
}