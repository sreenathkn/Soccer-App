using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using UDTProvider;
using Beesys.Wasp.Workflow;
using BeeSys.Wasp.Communicator;
using System.Xml.Linq;
using System.IO;
using BeeSys.Wasp.KernelController;
using Beesys.Wasp.WorkFlow;
using BeeSys.Wasp3D.DesignForms2;
using System.Xml;
using BeeSys.Wasp3D.FormDesigner2;
namespace SoccerApp
{
    public partial class SoccerApp : Form
    {
        private UDTProvider.UDTProvider _objUDT;
        CUDTManagerHelper _mObjUdtHandler;
        string ActiveMatch=string.Empty;
        public DateTime CountDownTarget = DateTime.Now;
        private int SrNo = 1;
        private DateTime MatchPartStartTime;
        CWaspFileHandler objWaspFileHandler;
        CRemoteHelper _objRemoteHelper;
        private string _CommonPath;
        SceneHandler _objsceneHandler;
        public SoccerApp()
        {
            InitializeComponent();
            InitilizeUDT();
            InitializeCombos();
            objWaspFileHandler = new CWaspFileHandler();
            _objsceneHandler = new SceneHandler();
            InitializeWasp();
            _objsceneHandler.Initialize();
         }

        private void InitializeWasp()
        {
              _CommonPath =  Environment.GetEnvironmentVariable("Wasp3.5");

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
              LoadTemplates();
              
        }
        private void LoadTemplates()
        {
            XDocument xdoc = XDocument.Load("templates.xml");
            var templates = xdoc.Descendants("template");
            foreach (var item in templates)
            {
                STemplateDetails obj = objWaspFileHandler.GetTemplatePlayerInfo(item.Attribute("id").Value, "");

                if(obj != null)
                { 
                    Form obj1 = Activator.CreateInstance(obj.TemplatePlayerInfo) as Form;
                    obj1.TopLevel = false;
                    obj1.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(obj1);
                    obj1.Show();
                }
                else
                {
                    MessageBox.Show("Template " + item.Attribute("id").Value + "Not found");
                }

            }

        }
        private void InitilizeUDT()
        {
            _objUDT = new UDTProvider.UDTProvider();
            _objUDT.InitializeConnection();
            _objUDT.InitializeUDT(ConfigurationManager.AppSettings["udtname"]);
        }

        private void InitializeCombos()
        {
            cmbMatch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // Get the Match List from UDT and add to cmbstr, Fill the Combo, and set the selected Item
            FillMatchList();
            cmbMatchPart.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatchPart.AutoCompleteSource = AutoCompleteSource.CustomSource;
           
           
            // Get the Match part List from UDT and add to cmbstr1, Fill the Combo, and set the selected Item
            FillMatchPart();
 
        }

        private void FillMatchPart()
        {
            AutoCompleteStringCollection cmbstr1 = new AutoCompleteStringCollection();
            var t = _objUDT.CurrentDataSet.Tables[11];
            foreach (DataRow item in _objUDT.CurrentDataSet.Tables[11].Rows)
            {
                cmbstr1.Add(item["Name"].ToString());
                cmbMatchPart.Items.Add(item["Name"].ToString());
            }
            cmbMatchPart.AutoCompleteCustomSource = cmbstr1;
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[11].Select("Active=true");
            if (dr.Count() > 0)
            {

                int index = cmbMatchPart.FindString(dr[0]["Name"].ToString());
                if (index != -1)
                {
                    cmbMatchPart.SelectedIndex = index;
                    UdtFilter filter = new UdtFilter();
                    filter.FilterColumn = "Name";
                    filter.FilterValue = cmbMatchPart.Text;
                    filter.TableIndex = 11;
                    if (!_objUDT.UdtFilters.ContainsKey("Match Part"))
                        _objUDT.UdtFilters.Add("Match Part", filter);
                    else
                        _objUDT.UdtFilters["Match Part"] = filter;
                    _objUDT.Notify("Match Part");
                }

            }

        }

        private void FillMatchList()
        {
           
            DataSet dt = _objUDT.CurrentDataSet;
            var t = dt.Tables[10];
            AutoCompleteStringCollection cmbstr = new AutoCompleteStringCollection();
            foreach (DataRow item in _objUDT.CurrentDataSet.Tables[10].Rows)
            {
                cmbstr.Add(item["Name"].ToString());
                cmbMatch.Items.Add(item["Name"].ToString());
            }
            cmbMatch.AutoCompleteCustomSource = cmbstr;
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[10].Select("Active=true");
            if (dr.Count() > 0)
            {
               
                int index = cmbMatch.FindString(dr[0]["Name"].ToString());
                if (index != -1)
                {
                    cmbMatch.SelectedIndex=index;
                    UdtFilter filter = new UdtFilter();
                    filter.FilterColumn = "Name";
                    filter.FilterValue = cmbMatch.Text;
                    filter.TableIndex = 10;
                    if (!_objUDT.UdtFilters.ContainsKey("Active Match"))
                        _objUDT.UdtFilters.Add("Active Match", filter);
                    else
                        _objUDT.UdtFilters["Active Match"] = filter;
                    _objUDT.Notify("Active Match");
                }
    
            }
        }
        private void cmbMatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UdtFilter filter = new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = cmbMatch.Text;
            filter.TableIndex = 10;
            if (!_objUDT.UdtFilters.ContainsKey("Active Match"))
                _objUDT.UdtFilters.Add("Active Match", filter);
            else
                _objUDT.UdtFilters["Active Match"] = filter;

            foreach (string item in cmbMatch.Items)
            {
                System.Diagnostics.Trace.WriteLine(item);

                if (item == cmbMatch.Text)
                    _objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatch.Text);
                else
                    _objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "false" }, "Name", item);
            }
            _objUDT.Notify("Active Match");
            //Select Teams based on the selected Match
            SelectTeams(cmbMatch.Text);
            ActiveMatch = cmbMatch.Text;
            dataGridView1.Rows.Clear();
            FillMatchEvents();
            UpdateWaspControls();
            //If there is no entry in Match Status table, insert new row
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[17].Select("Match= '" + cmbMatch.Text + "'");
            if(dr.Length==0)
            {
                _objUDT.InsertUDTData(17, new string[] { "Match" }, new string[] { cmbMatch.Text });
            }

        }
        private void UpdateWaspControls()
        {
            foreach (Control item in tableLayoutPanel1.Controls)
            {
                IAutomationDataEntry obj = (item) as IAutomationDataEntry;
                string str = obj.GetDataXml();
                XmlDocument xdDoc = new XmlDocument();
             //   xdDoc.LoadXml(str);
               XmlNode xn;//= xdDoc.SelectSingleNode("//data/userdata/requery/connection/query");
               string str1 = String.Format(str, cmbMatch.Text);
               IDataEntry cd = (item) as IDataEntry;
              // xdDoc.RemoveAll();
              // xdDoc = null;
              // xdDoc = new XmlDocument();
               xdDoc.LoadXml(str1);
               //xn.RemoveAll();
               //xn = null;
               xn = xdDoc.DocumentElement;
               cd.SetData(xn);

            }
        }
        private void FillMatchEvents()
        {
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[12].Select("MatchID= '"+cmbMatch.Text+"'");
            foreach (DataRow item in dr)
            {
                DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                dgv.Cells[0].Value = item["EventID"].ToString(); //SrNo
                dgv.Cells[1].Value = item["Time"].ToString(); //Time
                dgv.Cells[2].Value = item["MatchPart"].ToString(); //Match Part
                dgv.Cells[3].Value = item["EventType"].ToString(); //Event memo
                dgv.Cells[4].Value = item["Team"].ToString();//team Name
                dgv.Cells[5].Value = item["Player"].ToString();//player Name
                dataGridView1.Rows.Add(dgv);
                int sr = Convert.ToInt32(item["EventID"].ToString());
                if(SrNo<sr)
                {
                    SrNo = sr;
                }
            }
            SrNo++;
        }

        private void SelectTeams(string ActiveMatch)
        {
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[10].Select("Name= '"+ActiveMatch+"'");
            if(dr.Count()>0)
            {
                lblHomeTeam.Text = dr[0]["HomeTeam"].ToString();
                lblAwayTeam.Text = dr[0]["AwayTeam"].ToString();
                // Get Team Flag from team Table
                dr = _objUDT.CurrentDataSet.Tables[6].Select("Name= '" + lblHomeTeam.Text + "'");
                if (File.Exists(dr[0]["Logo"].ToString()))
                    pnlHomeFlag.BackgroundImage = Image.FromFile(dr[0]["Logo"].ToString());

                dr = _objUDT.CurrentDataSet.Tables[6].Select("Name= '" + lblAwayTeam.Text + "'");
                if (File.Exists(dr[0]["Logo"].ToString()))
                    pnlAwayFlag.BackgroundImage = Image.FromFile(dr[0]["Logo"].ToString());
                //Get initial scores for the selected teams from UDT
                InitializeScores(ActiveMatch);
            }
        }

        private void InitializeScores(string ActiveMatch)
        {
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[10].Select("Name= '" + ActiveMatch + "'");
            lblHomeScore.Text = dr[0]["HomeScore"].ToString();
            lblAwayScore.Text = dr[0]["AwayScore"].ToString();
        }
        private void cmbMatchPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCounter.Enabled = false;
            UdtFilter filter = new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = cmbMatch.Text;
            filter.TableIndex = 11;
            if (!_objUDT.UdtFilters.ContainsKey("Match Part"))
                _objUDT.UdtFilters.Add("Match Part", filter);
            else
                _objUDT.UdtFilters["Match Part"] = filter;

            foreach (string item in cmbMatchPart.Items)
            {
                 if (item == cmbMatchPart.Text)
                    _objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatchPart.Text);
                else
                    _objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "false" }, "Name", item);
            }

            _objUDT.Notify("Match Part");
            timer1.Stop();
            lblCounter.Text = "00:00";
            btnstartstop.Text = "Start";
            if(cmbMatchPart.Text=="Extra Time")
            {
                lblCounter.Enabled = true;
            }
        }

        private void btnhomeplus_Click(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
            lblHomeScore.Text =( Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
            _objUDT.UpdateUDT(10, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text);
            Player p = new Player();
            p.Team = lblHomeTeam.Text;
            p._objUDTProvider = _objUDT;
            p.FillTeam();
            p.ShowDialog();
          
            string selectedPlayer = p.selectedPlayer;
            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Goal";
            dgv.Cells[4].Value = lblHomeTeam.Text;
            dgv.Cells[5].Value = selectedPlayer;
            dataGridView1.Rows.Add(dgv);
            SrNo++;
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblHomeTeam.Text, selectedPlayer });
            p = null;
            updateMatchStats("HomeGoal", 1, "Match", cmbMatch.Text);
        }
        private void updateMatchStats(string Column,int value,string uniqueColumn,string uniqueValue)
        {
            _objUDT.RefreshUDT(ConfigurationSettings.AppSettings["udtname"]); // It will refresh the UDT and Dataset and get latest values available in UDT
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[17].Select(uniqueColumn + "= '" + uniqueValue + "'");
            int val = Convert.ToInt32(dr[0][Column].ToString())+value;
            _objUDT.UpdateUDT(17, new string[] { Column }, new string[] { val.ToString() }, uniqueColumn, uniqueValue);
        }
        private void btnHomeminus_Click(object sender, EventArgs e)
        {
            lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) -1).ToString();
            _objUDT.UpdateUDT(10, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text);
            updateMatchStats("HomeGoal", -1, "Match", cmbMatch.Text);
        }

        private void btnawayplus_Click(object sender, EventArgs e)
        {
            lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) + 1).ToString();
            _objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
            lblAwayScore.Text = (Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
            _objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
            Player p = new Player();
            p.Team = lblAwayTeam.Text;
            p._objUDTProvider = _objUDT;
            p.FillTeam();
            p.ShowDialog();
            TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
            string selectedPlayer = p.selectedPlayer;
            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Goal";
            dgv.Cells[4].Value = lblAwayTeam.Text;
            dgv.Cells[5].Value = selectedPlayer;
            dataGridView1.Rows.Add(dgv);
            SrNo++;
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblAwayTeam.Text, selectedPlayer });
            p = null;
            updateMatchStats("AwayGoal", 1, "Match", cmbMatch.Text);
        }
         
        private void btnawayminus_Click(object sender, EventArgs e)
        {
            lblAwayScore.Text = (Convert.ToInt32(lblHomeScore.Text) - 1).ToString();
            _objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
            updateMatchStats("AwayGoal", -1, "Match", cmbMatch.Text);
        }

        private void lblHomeTeam_DoubleClick(object sender, EventArgs e)
        {
            TeamBuilderForm tf = new TeamBuilderForm();
            tf.Team = lblHomeTeam.Text;
            tf._objUDTProvider = _objUDT;
            tf.FIllTeam();
            tf.ShowDialog();
            tf = null;
        }

        private void lblAwayTeam_DoubleClick(object sender, EventArgs e)
        {
            TeamBuilderForm tf = new TeamBuilderForm();
            tf.Team = lblAwayTeam.Text;
            tf._objUDTProvider = _objUDT;
            tf.FIllTeam();
            tf.ShowDialog();
            tf = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CountDownTarget < DateTime.Now)
                timer1.Stop();
            else
            {
                TimeSpan timeLeft = CountDownTarget.Subtract(DateTime.Now);
                lblCounter.Text = timeLeft.Minutes.ToString("00") + ":" + timeLeft.Seconds.ToString("00");
               
            }
        }

        private void btnstartstop_Click(object sender, EventArgs e)
        {
            if(btnstartstop.Text=="Start")
            {
                if (lblCounter.Text.Trim() == "00:00")
                {
                    if (cmbMatchPart.Text != "Extra Time")
                    {
                        CountDownTarget = DateTime.Now.AddMinutes(45.00);
                        
                        _objsceneHandler.TimerAction("update", "45,0,0,0");
                        _objsceneHandler.TimerAction("start", "");
                    }
                    else
                    {
                        _objsceneHandler.TimerAction("updateextra", lblCounter.Text + ",0,0,0");
                        _objsceneHandler.TimerAction("extrain", "");
                        CountDownTarget = DateTime.Now.AddMinutes(Convert.ToInt32(lblCounter.Text));
                        _objsceneHandler.TimerAction("extrastart", "");
                    }
                    MatchPartStartTime = DateTime.Now;
                }
                
                timer1.Enabled=true;
                timer1.Interval = 1000;
                timer1.Start();
                btnstartstop.Text = "Stop";
            }
            else
            {
                if (cmbMatchPart.Text != "Extra Time")
                {
                    _objsceneHandler.TimerAction("stop", "");
                }
                else
                {

                    _objsceneHandler.TimerAction("stopextratime", "");
                }
                timer1.Stop();
                btnstartstop.Text = "Start";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan ts=new TimeSpan();
            if(MatchPartStartTime.Year!=0001)
            { 
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            
            Substitution sb = new Substitution();
            sb._objUDTProvider = _objUDT;
            sb.HomeTeam = lblHomeTeam.Text;
            sb.AwayTeam = lblAwayTeam.Text;
            sb.Team = lblHomeTeam.Text;
            sb.Initialize();
            sb.ShowDialog();

            //string selectedInPlayer = p.selectedPlayer;
            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "SubstituteOut";
            dgv.Cells[4].Value = sb.Team;
            dgv.Cells[5].Value = sb.SelectedOutPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "SubstituteOut", sb.Team, sb.SelectedOutPlayer });
            SrNo++;
            dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "SubstituteIN";
            dgv.Cells[4].Value = sb.Team;
            dgv.Cells[5].Value = sb.SelectedInPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "SubstituteIN", sb.Team, sb.SelectedInPlayer });
            SrNo++;
            sb = null;
        }

        private void btnFoul_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            PlayerDetails pd = new PlayerDetails();
            pd._objUDTProvider = _objUDT;
            pd.Parent = "Foul";
            pd.Team = lblHomeTeam.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Foul";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dataGridView1.Rows.Add(dgv);
             _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Foul", pd.Team, pd.SelectedPlayer });
             SrNo++;
             string FoulTeam = "";
            if(pd.Team==lblHomeTeam.Text)
            {
                FoulTeam = "HomeFoul";
            }
            else
            {
                FoulTeam = "AwayFoul";
            }
            updateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
        }

        private void cmbCorner_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            TeamSelection tms = new TeamSelection();
            tms.cmbTeam.Items.Add(lblHomeTeam.Text);
            tms.cmbTeam.Items.Add(lblAwayTeam.Text);
            tms.ShowDialog();
            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Corner";
            dgv.Cells[4].Value = tms.SelectedTeam;
            dgv.Cells[5].Value = "";
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Corner", tms.SelectedTeam, "" });
            SrNo++;
          
            string FoulTeam = "";
            if (tms.SelectedTeam == lblHomeTeam.Text)
            {
                FoulTeam = "HomeCorner";
            }
            else
            {
                FoulTeam = "AwayCorner";
            }
            updateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
            tms = null;
        }

        private void btnShots_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            PlayerDetails pd = new PlayerDetails();
            pd._objUDTProvider = _objUDT;
            pd.Parent = "Shots ON";
            pd.Team = lblHomeTeam.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Shots ON";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots ON", pd.Team, pd.SelectedPlayer });
            SrNo++;
            string FoulTeam = "";
            if (pd.Team == lblHomeTeam.Text)
            {
                FoulTeam = "HomeShotsOnGoal";
            }
            else
            {
                FoulTeam = "AwayShotsOnGoal";
            }
            updateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
        }

        private void btnShotsOff_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            PlayerDetails pd = new PlayerDetails();
            pd._objUDTProvider = _objUDT;
            pd.Parent = "Shots OFF";
            pd.Team = lblHomeTeam.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Shots OFF";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots OFF", pd.Team, pd.SelectedPlayer });
            SrNo++;
            string FoulTeam = "";
            if (pd.Team == lblHomeTeam.Text)
            {
                FoulTeam = "HomeShotsMissed";
            }
            else
            {
                FoulTeam = "AwayShotsMissed";
            }
            updateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            PlayerDetails pd = new PlayerDetails();
            pd._objUDTProvider = _objUDT;
            pd.Parent = "Yellow Card";
            pd.Team = lblHomeTeam.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Yellow Card";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Yellow Card", pd.Team, pd.SelectedPlayer });
            SrNo++;
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }
            PlayerDetails pd = new PlayerDetails();
            pd._objUDTProvider = _objUDT;
            pd.Parent = "Red Card";
            pd.Team = lblHomeTeam.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            if (MatchPartStartTime.Year != 0001)
            {
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            else
            {
                dgv.Cells[1].Value = "00:00";
            }
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Red Card";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dataGridView1.Rows.Add(dgv);
            _objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Red Card", pd.Team, pd.SelectedPlayer });
            SrNo++;
        }
  
    }
}
