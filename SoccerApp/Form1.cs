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

namespace SoccerApp
{
    public partial class Form1 : Form
    {
        public UDTProvider.UDTProvider Udt { get; set; }
        public Form1()
        {
            InitializeComponent();
            Udt = new UDTProvider.UDTProvider();
            Udt.InitializeConnection();
            Udt.InitializeUDT("Soccer");
            team1.TeamType = "home";
            team2.TeamType = "away";
            match1.Udt = Udt;
            team1._objUDTProvider = Udt;
            team2._objUDTProvider = Udt;
        }
    }
}
