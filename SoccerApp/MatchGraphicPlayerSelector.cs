using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerApp
{
    public partial class MatchGraphicPlayerSelector : Form
    {
        public bool P1 { get { return chkP1.Checked; } set { chkP1.Checked = value; } }
        public bool P2 { get { return chkP2.Checked; } set { chkP2.Checked = value; } }

        public MatchGraphicPlayerSelector()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
