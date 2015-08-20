using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Beesys.Wasp.Workflow;
using BeeSys.Wasp.Communicator;
using BeeSys.Wasp.KernelController;

namespace SoccerApp
{
    public partial class PlayerHandler : Form
    {
        private string _CommonPath;
        CRemoteHelper _objRemoteHelper;
        CUDTManagerHelper _mObjUdtHandler;
        CWaspFileHandler objWaspFileHandler;
        int RowPos = 0;
        List<ScenInfo> _SceneCollection;
        public PlayerHandler()
        {
            InitializeComponent();
            InitializeWasp();
            tableLayoutPanel1.SetRowSpan(listBox1, 2);
            _SceneCollection = new List<ScenInfo>();
            GetTemplateList();
        }

        private void GetTemplateList()
        {
            listBox1.Items.Clear();
            XDocument xdoc = XDocument.Load("templates.xml");
            var templates = xdoc.Descendants("template");
            foreach (var item in templates)
            {
                ScenInfo sc = new ScenInfo();
                sc.Id = item.Attribute("id").Value;
                sc.Name = item.Attribute("name").Value;
                sc.Description = item.Attribute("description").Value;
                sc.inuse = false;
                _SceneCollection.Add(sc);
                listBox1.Items.Add(item.Attribute("description").Value);
            }
        }
        struct ScenInfo
        {
            public string Name;
            public string Id;
            public string Description;
            public bool inuse;
        }
        private void InitializeWasp()
        {
            _CommonPath = Environment.GetEnvironmentVariable("Wasp3.5");

            var configfile = Path.Combine(_CommonPath, "CommonConfig.config");

            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
                      where lv1.Attribute("key").Value == "LOCALMANAGERURL"
                      select lv1.Attribute("value").Value;
            _objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
            ConnectionStatus info = _objRemoteHelper.Connect();
            _mObjUdtHandler = new CUDTManagerHelper(CRemoteHelper.GetDisconnectedUrl("UDTManager"));
            objWaspFileHandler = new CWaspFileHandler();
            objWaspFileHandler.Initialize(CRemoteHelper.GetDisconnectedUrl("TemplateManager"));
           

        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ScenInfo si = _SceneCollection.Where(s => s.Description == listBox1.Text).FirstOrDefault();
            STemplateDetails obj = objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");
            if (obj != null)
            {
                Form obj1 = Activator.CreateInstance(obj.TemplatePlayerInfo) as Form;
                obj1.TopLevel = false;
                obj1.Visible = true;
               // obj1.Dock = DockStyle.Fill;
                var ctl = tableLayoutPanel1.GetControlFromPosition(1, RowPos);
                if(ctl !=null) // Already control present there
                {
                    //First play out the graphic.
                    //then delete the gfx
                    tableLayoutPanel1.Controls.Remove(ctl);

                }
                tableLayoutPanel1.Controls.Add(obj1,1,RowPos);
                if (RowPos == 1)
                    RowPos--;
                else
                    RowPos++;
                obj1.Show();
            }
            else
            {
                MessageBox.Show("Template " + si.Id + "Not found");
            }

        }
    }
}
