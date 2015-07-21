using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team
{
    public partial class TeamBuilderForm : Form
    {
        public TeamBuilderForm()
        {
            InitializeComponent();
        }
        public TeamBuilderForm(UDTProvider.UDTProvider UDTProvider)
        {
            _objUDTProvider = UDTProvider;
            
        }
        public void FIllTeam()
        {
            DataRow[] dtTeams = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + Team + "'");
            dataGridView2.DataSource = dtTeams;
        }
        public UDTProvider.UDTProvider _objUDTProvider { get; set; }
        public string Team { get; set; }
    }
}
