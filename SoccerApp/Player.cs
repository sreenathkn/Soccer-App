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
            DataRow drTeam = _objUDTProvider.CurrentDataSet.Tables[2].Select("Name = '" + Team+"'").FirstOrDefault();
            if (drTeam != null)
            {
                DataRow[] drPlayers = _objUDTProvider.CurrentDataSet.Tables[3].Select("T24_ID = '" + drTeam["T24_ID"] + "' AND Playing=true");
                foreach (var item in drPlayers)
                {
                    lstPlayers.Items.Add(item["First Name"]);
                }
            }
        }

        private void lstPlayers_DoubleClick(object sender, EventArgs e)
        {
            selectedPlayer = lstPlayers.SelectedItem.ToString();
            this.Close();
        }
    }
}
