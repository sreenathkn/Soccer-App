using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Controller
{
    public partial class MatchSelection : Form
    {
        private UDTProvider.UDTProvider _objUDT;
        private Match _objController;
        public MatchSelection( Match objController)
        {
            InitializeComponent();
            _objController = objController;
            _objUDT = objController.Udt;

            DataSet dt = new DataSet();
            using (XmlReader reader = XmlReader.Create(new StringReader(_objUDT.CurrentUDT.FORMAT)))
            {
                dt.ReadXmlSchema(reader);
            }

            if (!string.IsNullOrEmpty(_objUDT.CurrentUDT.DATA))
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(_objUDT.CurrentUDT.DATA)))
                {
                    dt.ReadXml(reader);
                }
            }
            var t = dt.Tables[10];
            listBox1.DataSource = t;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
            /*var ab = from fn in _objUDT.CurrentUDT.UDTTABLE
                     where fn.UdtTableName == "Match"
                     select fn.RowIndex;*/
            
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatch = listBox1.Text;
            this.Close();
        }
    }
}
