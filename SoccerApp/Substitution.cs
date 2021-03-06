﻿using System;
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
    public partial class Substitution : Form
    {
        public UDTProvider.UDTProvider _objUDTProvider { get; set; }
        public string Team { get; set; }
        private bool isJerOutChanged;
        private bool isoutNameChanged;
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string SelectedInPlayer { get; set; }
        public string SelectedOutPlayer { get; set; }

        public Substitution()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            AutoCompleteStringCollection cmbstrName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cmbstrJer = new AutoCompleteStringCollection();
            DataRow drTeam = _objUDTProvider.CurrentDataSet.Tables[2].Select("Name = '" + Team+"'").FirstOrDefault();
            if (drTeam != null)
            {
                DataRow[] drPlayers = _objUDTProvider.CurrentDataSet.Tables[3].Select("T24_ID = '" + drTeam["T24_ID"] + "' AND Playing=true");
               // DataRow[] t = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + Team + "' AND Playing=true");
                cmbIn.Items.Clear();
                cmbOut.Items.Clear();
                cmbJerIN.Items.Clear();
                cmbJerOUT.Items.Clear();
                foreach (DataRow item in drPlayers)
                {
                    cmbIn.Items.Add(item["First Name"]);
                    cmbstrName.Add(item["First Name"].ToString());
                    cmbOut.Items.Add(item["First Name"]);
                    cmbJerIN.Items.Add(item["Jersey No"]);
                    cmbJerOUT.Items.Add(item["Jersey No"]);
                    cmbstrJer.Add(item["Jersey No"].ToString());
                }
                cmbIn.SelectedIndex = 1;
                cmbOut.SelectedIndex = 1;
                cmbJerIN.SelectedIndex = 1;
                cmbJerOUT.SelectedIndex = 1;

                cmbIn.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbIn.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbOut.AutoCompleteCustomSource = cmbstrName;
                cmbOut.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbOut.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbOut.AutoCompleteCustomSource = cmbstrName;
                cmbJerIN.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbJerIN.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbJerIN.AutoCompleteCustomSource = cmbstrJer;
                cmbJerOUT.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbJerOUT.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbJerOUT.AutoCompleteCustomSource = cmbstrJer;
                if (cmbTeam.Items.Count <= 0)
                {
                    cmbTeam.Items.Add(HomeTeam);
                    cmbTeam.Items.Add(AwayTeam);
                }
            }

        }
        private void cmbJerOUT_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                cmbOut.SelectedIndex = cmbJerOUT.SelectedIndex;
          
        }
        private void cmbOut_SelectedIndexChanged(object sender, EventArgs e)
        {
                cmbOut.SelectedIndex = cmbOut.SelectedIndex;
                SelectedOutPlayer = cmbOut.Text;
           
        }

        private void cmbJerIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIn.SelectedIndex = cmbJerIN.SelectedIndex;
        }

        private void cmbIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbJerIN.SelectedIndex = cmbIn.SelectedIndex;
            SelectedInPlayer = cmbIn.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Team = cmbTeam.Text;
            Initialize();
        }
    }
}
