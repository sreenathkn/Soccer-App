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
    public partial class TeamBuilderForm : Form
    {
        public UDTProvider.UdtProvider ObjUdtprovider { get; set; }

        public string Team { get; set; }

        public TeamBuilderForm()
        {
            InitializeComponent();
            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;

            listBox1.MouseDown += listBox1_MouseDown;
            listBox2.MouseDown += listBox2_MouseDown;
            listBox1.DragOver += listBox1_DragOver;
            listBox2.DragOver += listBox2_DragOver;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox2.DragDrop += listBox2_DragDrop;
        }

        public TeamBuilderForm(UDTProvider.UdtProvider UDTProvider)
        {
            ObjUdtprovider = UDTProvider;
        }

        public void FillTeam()
        {
            DataRow dtTeam =ObjUdtprovider.CurrentDataSet.Tables[2].Select("Name='" + Team + "'").FirstOrDefault();
            string primaryvalue =Convert.ToString(dtTeam[ObjUdtprovider.CurrentDataSet.Tables[2].PrimaryKey[0].ColumnName]);
            string parentcolumn = ObjUdtprovider.CurrentDataSet.Tables[3].ParentRelations[0].ParentColumns[0].ColumnName;
            DataRow[] dtTeams = ObjUdtprovider.CurrentDataSet.Tables[3].Select(parentcolumn + "='" + primaryvalue + "' AND Playing=false");
            foreach (var item in dtTeams)
            {
                listBox1.Items.Add(item["First Name"]);
            }
            dtTeams = ObjUdtprovider.CurrentDataSet.Tables[3].Select(parentcolumn + "='" + primaryvalue + "' AND Playing=true");
            foreach (var item in dtTeams)
            {
                listBox2.Items.Add(item["First Name"]);
            }

        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(
                    DataFormats.StringFormat);
                listBox2.Items.Add(str);
            }
        }

        private void listBox2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;
            int index = listBox1.IndexFromPoint(e.X, e.Y);
            string s = listBox1.Items[index].ToString();
            DragDropEffects dde1 = DoDragDrop(s,
                DragDropEffects.All);

            if (dde1 == DragDropEffects.All)
            {
                listBox1.Items.RemoveAt(listBox1.IndexFromPoint(e.X, e.Y));
            } 
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(
                    DataFormats.StringFormat);

                listBox1.Items.Add(str);
            }
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox2.Items.Count == 0)
                return;
            int index = listBox2.IndexFromPoint(e.X, e.Y);
            if (index != -1)
            {
                string s = listBox2.Items[index].ToString();
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);

                if (dde1 == DragDropEffects.All)
                {
                    listBox2.Items.RemoveAt(listBox2.IndexFromPoint(e.X, e.Y));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                ObjUdtprovider.UpdateUdt(3, new string[] { "Playing" }, new string[] { "false" }, "[First Name]", item.ToString(), true);
            }
            foreach (var item in listBox2.Items)
            {
                ObjUdtprovider.UpdateUdt(3, new string[] { "Playing" }, new string[] { "true" }, "[First Name]", item.ToString(), true);
            }
            this.Close();
        }
    }
}
