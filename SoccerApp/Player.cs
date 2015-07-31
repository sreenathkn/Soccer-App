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
    public partial class Player : Form
    {
        public UDTProvider.UDTProvider _objUDTProvider { get; set; }
        public string Team { get; set; }
        public string selectedPlayer { get; set; }
        public Player()
        {
            InitializeComponent();
        }
        public void FillTeam()
        {
            DataRow[] dtTeams = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + Team + "' AND Playing=true");
            foreach (var item in dtTeams)
            {
                lstPlayers.Items.Add(item["Name"]);
            }
        }

        private void lstPlayers_DoubleClick(object sender, EventArgs e)
        {
            selectedPlayer = lstPlayers.SelectedItem.ToString();
            this.Close();
        }
    }
}
