using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team
{
    public partial class Team : UserControl
    {
        public Team()
        {
            InitializeComponent();

            (dataGridView1.Columns[3] as DataGridViewButtonColumn).UseColumnTextForButtonValue = false;
            (dataGridView1.Columns[4] as DataGridViewButtonColumn).UseColumnTextForButtonValue = false;
            (dataGridView1.Columns[5] as DataGridViewButtonColumn).UseColumnTextForButtonValue = false;
            (dataGridView1.Columns[7] as DataGridViewButtonColumn).UseColumnTextForButtonValue = false;
            (dataGridView1.Columns[8] as DataGridViewButtonColumn).UseColumnTextForButtonValue = false;
            (dataGridView1.Columns[6] as DataGridViewComboBoxColumn).Items.Add("SUDHEESH");
           
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
