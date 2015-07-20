using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
namespace Team
{
    public partial class Team : UserControl
    {
        private Controller.Match _objController;
        public string TeamType { get; set; }

        public Team()
        {
            InitializeComponent();

        }
        public Team(Controller.Match cnt)
        {
            _objController = cnt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var column = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                switch(e.ColumnIndex)
                {
                    case 3: //Goals
                        (senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn).Text = "GOAL";
                        column.Selected = false;
                        if (TeamType == "home")
                        {
                            _objController.HomeTeamGoal += 1;
                        }
                        else
                        {
                            _objController.AwayTeamGoal += 1;
                        }
                        break;
                    case 4: //Shots on Goal
                        break;
                    case 5: //foul
                        break;
                    case 7: //yellow
                        break;
                    case 8: //red
                        break;
                }
            }
            else if((senderGrid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex >= 0))
            {

            }
        }
    }
}
