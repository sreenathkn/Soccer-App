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
    public partial class PlayerDetails : Form
    {
        public UDTProvider.UDTProvider _objUDTProvider { get; set; }
        public string Team { get; set; }
        public string SelectedPlayer { get; set; }
        public string Parent { get; set; }
        public PlayerDetails()
        {
            InitializeComponent();
            //Initialize();
        }

        public void Initialize()
        {
            setFormHeader();
            AutoCompleteStringCollection cmbstrName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cmbstrJer = new AutoCompleteStringCollection();
            DataRow[] t = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + Team + "' AND Playing=true");
            cmbPlayerJer.Items.Clear();
            cmbPlayerName.Items.Clear();
            foreach (DataRow item in t)
            {
                cmbPlayerName.Items.Add(item["Name"]);
                cmbstrName.Add(item["Name"].ToString());
                cmbPlayerJer.Items.Add(item["JerseyNo"]);
                cmbstrJer.Add(item["JerseyNo"].ToString());
            }
            cmbPlayerName.SelectedIndex = 1;
            cmbPlayerJer.SelectedIndex = 1;
            cmbPlayerName.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbPlayerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbPlayerName.AutoCompleteCustomSource = cmbstrName;
            cmbPlayerJer.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbPlayerJer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbPlayerJer.AutoCompleteCustomSource = cmbstrJer;
        }

        private void setFormHeader()
        {
            switch (Parent)
            {
                case "Foul":
                    this.Text = "Foul: Player Selection";
                    break;
                default:
                    break;
            }
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Team = cmbTeam.Text;
            Initialize();
        }

        private void cmbPlayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPlayer = cmbPlayerName.Text;
            cmbPlayerJer.SelectedIndex = cmbPlayerName.SelectedIndex;
        }

        private void cmbPlayerJer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPlayerName.SelectedIndex = cmbPlayerJer.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
