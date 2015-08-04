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
    public partial class TeamSelection : Form
    {
        public string SelectedTeam { get; set; }
        public TeamSelection()
        {
            InitializeComponent();
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTeam = cmbTeam.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
