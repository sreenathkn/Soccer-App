using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatchPart = listBox1.Text;
            this.Close();
        }
    }
}
