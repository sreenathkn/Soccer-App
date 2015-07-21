using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDTProvider;

namespace Controller
{
    public partial class MatchPart : Form
    {
        private UDTProvider.UDTProvider _objUDT;
        private Match _objController;

        public MatchPart(Match objController)
        {
            InitializeComponent();
            _objController = objController;
           _objUDT = objController.Udt;
            DataSet dt = _objController.Udt.CurrentDataSet;
            var t = dt.Tables[11];
            listBox1.DataSource = t;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatchPart = listBox1.Text;
            UdtFilter filter = new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = listBox1.Text;
            filter.TableIndex = 11;
            if (!_objController.Udt.UdtFilters.ContainsKey("Match Part"))
                _objController.Udt.UdtFilters.Add("Match Part", filter);
            else
                _objController.Udt.UdtFilters["Match Part"] = filter;
            this.Close();
        }
    }
}
