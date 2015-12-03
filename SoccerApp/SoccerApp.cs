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
using BeeSys.Wasp3D.Controls2;
namespace SoccerApp
{
    public partial class SoccerApp : Form
    {
        #region Class Members

        private UDTProvider.UDTProvider m_objUDT;
        CUDTManagerHelper m_ObjUdtHandler;
        string ActiveMatch = string.Empty;
        public DateTime CountDownTarget = DateTime.Now;
        private int SrNo = 1;
        private DateTime MatchPartStartTime;
        CWaspFileHandler m_objWaspFileHandler;
        CRemoteHelper m_objRemoteHelper;
        private string m_sCommonPath;
        SceneHandler m_objsceneHandler = null;
        List<ScenInfo> m_lstSceneCollection = null;
        IPlayer m_objPlayer = null;
        protected static LINKTYPE m_objLinkType = LINKTYPE.TCP;     
        private const string m_surl = "net.tcp://192.168.1.192:50011/TcpBinding/WcfTcpLink";
        string m_sEngineUrl = null;

        #endregion

        #region Constructor

        public SoccerApp()
        {
            try
            {
                InitializeComponent();         
                Init();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion



        #region Private Methods

        private void Init()
        {
            try
            {
                SetUI();
                InitilizeUDT();
                InitializeCombos();
                m_objWaspFileHandler = new CWaspFileHandler();
                m_objsceneHandler = new SceneHandler();
                m_lstSceneCollection = new List<ScenInfo>();
                InitializeWasp();
                fillgrid();
                FillPlayerList();
                m_objsceneHandler.Initialize();
            }
            finally
            {

            }
        }

        private void SetUI()
        {
            lblCounter.ForeColor = Color.FromArgb(0, 142, 188);
            pnlHmTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlAwayTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlcntr.BackColor = Color.FromArgb(228, 228, 228);
            pnlMActions.BackColor = Color.FromArgb(86, 110, 123);
            //lblAwayScore.BackColor = Color.FromArgb(255, 255, 255);
            btnFoul.BackColor = Color.FromArgb(182, 182, 182);
            btnYellow.BackColor = Color.FromArgb(182, 182, 182);
            btnShots.BackColor = Color.FromArgb(182, 182, 182);
            btnShotsOff.BackColor = Color.FromArgb(182, 182, 182);
            cmbCorner.BackColor = Color.FromArgb(182, 182, 182);
            btnRed.BackColor = Color.FromArgb(182, 182, 182);
            button1.BackColor = Color.FromArgb(182, 182, 182);
            pnlPlay.BackColor = Color.FromArgb(86, 109, 123);
            pnlActions.BackColor = Color.FromArgb(86, 109, 123);
            dgvMatchevents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(176, 174, 175);
            dgvMatchevents.EnableHeadersVisualStyles = false;
            lblCounter.BackColor = Color.FromArgb(228, 228, 228);
            cmbMatch.BackColor = Color.FromArgb(182, 182, 182);
            cmbMatchPart.BackColor = Color.FromArgb(182, 182, 182);
            pnlActions.BackColor = Color.FromArgb(196, 196, 196);
            pnlLabels.BackColor = Color.FromArgb(86, 109, 123);
            lblMatchevents.ForeColor = Color.FromArgb(51, 51, 51);
            pnlMatch.BackColor = Color.FromArgb(196, 196, 196);
        }



        private void InitializeWasp()
        {
            m_sCommonPath = Environment.GetEnvironmentVariable("Wasp3.5");

            var configfile = Path.Combine(m_sCommonPath, "CommonConfig.config");

            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
                      where lv1.Attribute("key").Value == "LOCALMANAGERURL"
                      select lv1.Attribute("value").Value;
            m_objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
            ConnectionStatus info = m_objRemoteHelper.Connect();
            m_ObjUdtHandler = new CUDTManagerHelper(CRemoteHelper.GetDisconnectedUrl("UDTManager"));
            m_objWaspFileHandler = new CWaspFileHandler();
            m_objWaspFileHandler.Initialize(CRemoteHelper.GetDisconnectedUrl("TemplateManager"));
            LoadTemplates();

        }

        private void LoadTemplates()
        {
           
            XDocument xdoc = XDocument.Load("templates.xml");
            var templates = xdoc.Descendants("template");
            foreach (var item in templates)
            {
                ScenInfo sc = new ScenInfo();
                sc.Id = item.Attribute("id").Value;
                sc.Name = item.Attribute("name").Value;
                sc.Description = item.Attribute("description").Value;
                sc.inuse = false;
                m_lstSceneCollection.Add(sc);
               
            }
        }

        private void InitilizeUDT()
        {
            m_objUDT = new UDTProvider.UDTProvider();
            m_objUDT.InitializeConnection();
            m_objUDT.InitializeUDT(ConfigurationManager.AppSettings["udtname"]);
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
            var t = m_objUDT.CurrentDataSet.Tables[11];
            foreach (DataRow item in m_objUDT.CurrentDataSet.Tables[11].Rows)
            {
                cmbstr1.Add(item["Name"].ToString());
                cmbMatchPart.Items.Add(item["Name"].ToString());
            }
            cmbMatchPart.AutoCompleteCustomSource = cmbstr1;
            DataRow[] dr = m_objUDT.CurrentDataSet.Tables[11].Select("Active=true");
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
                    if (!m_objUDT.UdtFilters.ContainsKey("Match Part"))
                        m_objUDT.UdtFilters.Add("Match Part", filter);
                    else
                        m_objUDT.UdtFilters["Match Part"] = filter;
                    m_objUDT.Notify("Match Part");
                }

            }

        }

        private void FillMatchList()
        {

            DataSet dt = m_objUDT.CurrentDataSet;
            var t = dt.Tables[10];
            AutoCompleteStringCollection cmbstr = new AutoCompleteStringCollection();
            foreach (DataRow item in m_objUDT.CurrentDataSet.Tables[10].Rows)
            {
                cmbstr.Add(item["Name"].ToString());
                cmbMatch.Items.Add(item["Name"].ToString());
            }
            cmbMatch.AutoCompleteCustomSource = cmbstr;
            DataRow[] dr = m_objUDT.CurrentDataSet.Tables[10].Select("Active=true");
            if (dr.Count() > 0)
            {

                int index = cmbMatch.FindString(dr[0]["Name"].ToString());
                if (index != -1)
                {
                    cmbMatch.SelectedIndex = index;
                    UdtFilter filter = new UdtFilter();
                    filter.FilterColumn = "Name";
                    filter.FilterValue = cmbMatch.Text;
                    filter.TableIndex = 10;
                    if (!m_objUDT.UdtFilters.ContainsKey("Active Match"))
                        m_objUDT.UdtFilters.Add("Active Match", filter);
                    else
                        m_objUDT.UdtFilters["Active Match"] = filter;
                    m_objUDT.Notify("Active Match");
                }

            }
        }
        /// <summary>
        /// Update the Data Xml with the current match Id
        /// </summary>
        /// <param name="objfrm"></param>
        private void UpdateWaspControls(Form item)
        {

            try
            {

                //TODO: Replace the DataXml with the new values where id is that of active match.. and setData with the new XML..
                if (item != null)
                {
                    IAutomationDataEntry objIAutomationDataEntry = (item) as IAutomationDataEntry;
                    string sDataXml = objIAutomationDataEntry.GetDataXml();

                    XDocument xdoc = XDocument.Parse(sDataXml);


                    XElement xe = xdoc.Descendants("query").FirstOrDefault();
                    string str = xe.Value;

                    XElement tablenodes = XElement.Parse(str);
                    IEnumerable<XElement> elements = tablenodes.Descendants("table").Where(x => x.Attribute("name").Value == "13");

                    foreach (XElement node in elements)
                    {
                        //Update ID  here.....                
                        node.Attribute("customfilter").SetValue("(T1_ID In (  1 )) AND (([ID] =" + ID + "))");
                        node.Attribute("filter").SetValue("([ID] = " + ID + ")");
                        node.Attribute("actionfilter").SetValue("[ID] =" + ID);
                        //XAttribute attribute = new XAttribute("sort", "");
                        //node.Add(attribute);
                    }
                    xe.ReplaceNodes(new XCData(tablenodes.ToString()));                    
                    string updatedxml = xdoc.ToString();
                    XmlNode xn;
                    IDataEntry _objDataEntry = (item) as IDataEntry;
                    XmlDocument objdoc = new XmlDocument();
                    objdoc.LoadXml(updatedxml);
                    xn = objdoc.DocumentElement;
                    _objDataEntry.SetData(xn);
                }

            }
            catch (Exception ex)
            {

                LogWriter.WriteLog(ex);
            }

        }


        private void FillMatchEvents()
        {
            DataRow[] dr = m_objUDT.CurrentDataSet.Tables[12].Select("MatchID= '" + cmbMatch.Text + "'");
            foreach (DataRow item in dr)
            {
                DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                dgv.Cells[0].Value = item["EventID"].ToString(); //SrNo
                dgv.Cells[1].Value = item["Time"].ToString(); //Time
                dgv.Cells[2].Value = item["MatchPart"].ToString(); //Match Part
                dgv.Cells[3].Value = item["EventType"].ToString(); //Event memo
                dgv.Cells[4].Value = item["Team"].ToString();//team Name
                dgv.Cells[5].Value = item["Player"].ToString();//player Name
                dgvMatchevents.Rows.Add(dgv);
                int sr = Convert.ToInt32(item["EventID"].ToString());
                if (SrNo < sr)
                {
                    SrNo = sr;
                }
            }
            SrNo++;
        }

        /// <summary>
        /// Selects the team on the basis of active match
        /// </summary>
        /// <param name="ActiveMatch"></param>
        private void SelectTeams(string ActiveMatch)
        {
            DataRow[] dr = m_objUDT.CurrentDataSet.Tables[10].Select("Name= '" + ActiveMatch + "'");
            if (dr.Count() > 0)
            {
                lblHomeTeam2.Text = dr[0]["HomeTeam"].ToString();
                lblAwayTeam.Text = dr[0]["AwayTeam"].ToString();
                // Get Team Flag from team Table
                dr = m_objUDT.CurrentDataSet.Tables[6].Select("Name= '" + lblHomeTeam2.Text + "'");
                if (File.Exists(dr[0]["Logo"].ToString()))
                    pnlHomeFlag.BackgroundImage = Image.FromFile(dr[0]["Logo"].ToString());

                dr = m_objUDT.CurrentDataSet.Tables[6].Select("Name= '" + lblAwayTeam.Text + "'");
                if (File.Exists(dr[0]["Logo"].ToString()))
                    pnlAwayFlag.BackgroundImage = Image.FromFile(dr[0]["Logo"].ToString());
                //Get initial scores for the selected teams from UDT
                InitializeScores(ActiveMatch);
            }
        }

        private void InitializeScores(string ActiveMatch)
        {
            DataRow[] dr = m_objUDT.CurrentDataSet.Tables[10].Select("Name= '" + ActiveMatch + "'");
            lblHomeScore.Text = dr[0]["HomeScore"].ToString();
            lblAwayScore.Text = dr[0]["AwayScore"].ToString();
        }

        private void updateMatchStats(string Column, int value, string uniqueColumn, string uniqueValue)
        {
            try
            {
                m_objUDT.RefreshUDT(ConfigurationManager.AppSettings["udtname"]); // It will refresh the UDT and Dataset and get latest values available in UDT
                if (m_objUDT.CurrentDataSet != null)
                {
                    DataRow[] dr = m_objUDT.CurrentDataSet.Tables[17].Select(uniqueColumn + "= '" + uniqueValue + "'");
                    if (dr != null && dr.Length > 0 && (Convert.ToInt32(dr[0][Column])>0 ||value==1))
                    {
                        int val = Convert.ToInt32(dr[0][Column].ToString()) + value;
                        m_objUDT.UpdateUDT(17, new string[] { Column }, new string[] { val.ToString() }, uniqueColumn, uniqueValue);
                    }
                    else if(value!=-1)
                    {
                        DataRow drlast=m_objUDT.CurrentDataSet.Tables[17].Rows[m_objUDT.CurrentDataSet.Tables[17].Rows.Count - 1];
                        int id = 0;
                        if(drlast!=null)
                        {
                            int.TryParse(Convert.ToString(drlast["ID"]), out id);
                        }
                        id++;
                        int val =value;
                        m_objUDT.InsertUDTData(17, new string[] { "ID", uniqueColumn, Column }, new string[] { id.ToString(),uniqueValue, val.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {

                LogWriter.WriteLog(ex);
            }

        }

        struct ScenInfo
        {
            public string Name;
            public string Id;
            public string Description;
            public bool inuse;
        }

        /// <summary>
        /// Fills the Datagridview to select players.
        /// </summary>
        private void fillgrid()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Column2", typeof(bool));
            dt.Columns.Add("Column3", typeof(bool));
            foreach (var item in m_lstSceneCollection)
            {
                dr = dt.NewRow();
                dr["Name"] = item.Description;
                dr["Column2"] = false;
                dr["Column3"] = false;
                dt.Rows.Add(dr);
            }
            dgvSelectPlayer.DataSource = dt;
            dgvSelectPlayer.CellContentClick += dataGridView2_CellContentClick;
            dgvSelectPlayer.CurrentCellDirtyStateChanged += dataGridView2_CurrentCellDirtyStateChanged;

        }
        /// <summary>
        /// Fill list of players from templatesXml
        /// </summary>
        private void FillPlayerList()
        {

            XDocument xdoc = XDocument.Load("templates.xml");
            var templates = xdoc.Descendants("template");
            foreach (var item in templates)
            {
                ScenInfo sc = new ScenInfo();
                sc.Id = item.Attribute("id").Value;
                sc.Name = item.Attribute("name").Value;
                sc.Description = item.Attribute("description").Value;
                sc.inuse = false;
                m_lstSceneCollection.Add(sc);

            }
        }

        /// <summary>
        /// Method to get template details and player
        /// </summary>
        /// <param name="str"></param>
        /// <param name="column"></param>
        private void GetTemplate(string str, string column)
        {
            try
            {
                ScenInfo si = m_lstSceneCollection.Where(s => s.Description == str).FirstOrDefault();
                STemplateDetails obj = m_objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");

                if (obj != null)
                {
                    m_objPlayer = Activator.CreateInstance(obj.TemplatePlayerInfo) as IPlayer;

                    Form objfrm = m_objPlayer as Form;
                    UpdateWaspControls(objfrm);

                    string sLinkID = string.Empty;
                    if (m_objPlayer != null)
                    {
                        m_objPlayer.Init("", "", si.Id, "");
                        m_objPlayer.SetLink(m_objsceneHandler.AppLink, obj.Scene);
                        //S. No 116: Added for AddIn
                        //S.No.	: -	128
                        if (m_objPlayer is IAddinInfo)
                            (m_objPlayer as IAddinInfo).Init(new InstanceInfo() { Type = "wsp", InstanceId = "", TemplateId = si.Id, ThemeId = "Default", });


                        IChannelShotBox objChannelShotBox = m_objPlayer as IChannelShotBox;
                        if (objChannelShotBox != null)
                        {
                            if (m_objLinkType == LINKTYPE.TCP)
                                m_sEngineUrl = m_surl;
                            //newplaylistinstance.ActiveServer.GetUrl(CConstants.Constants.TCP);
                            //S.No.: -	147
                            objChannelShotBox.SetEngineUrl(m_surl);
                        }
                        m_objPlayer.OnShotBoxStatus += objPlayer_OnShotBoxStatus;
                        m_objPlayer.OnShotBoxControllerStatus += objPlayer_OnShotBoxControllerStatus;
                        m_objPlayer.Prepare(m_sEngineUrl, Convert.ToInt32(m_objPlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
                    }
                    objfrm.TopLevel = false;
                    objfrm.Visible = false;            
                    objfrm.Dock = DockStyle.Fill;
                    Control ctl = null;
                    Control ctl2 = null;
                    if (string.Compare(column, "Column2", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        ctl = tableLayoutPanel1.GetControlFromPosition(0, 0);
                        if (ctl != null) // Already control present there
                        {
                            //First play out the graphic.
                            //then delete the gfx                            
                            tableLayoutPanel1.Controls.Remove(ctl);                      
                            (ctl as IPlayer).DeleteSg();
                            tableLayoutPanel1.Controls.Add(objfrm, 0, 0);
                        }
                    }

                    if (string.Compare(column, "Column3", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        ctl2 = tableLayoutPanel1.GetControlFromPosition(1, 0);
                        if (ctl2 != null) // Already control present there
                        {
                            //First play out the graphic.
                            //then delete the gfx
                            tableLayoutPanel1.Controls.Remove(ctl2);
                            (ctl2 as IPlayer).DeleteSg();
                            tableLayoutPanel1.Controls.Add(objfrm, 1, 0);
                        }
                    }
                    if (ctl == null && (string.Compare(column, "Column2") == 0))
                    {
                        tableLayoutPanel1.Controls.Add(objfrm, 0, 0);
                    }
                    else if (ctl2 == null && (string.Compare(column, "Column3") == 0))
                    {
                        tableLayoutPanel1.Controls.Add(objfrm, 1, 0);
                    }
                    objfrm.Show();
                }
                else
                {
                    MessageBox.Show("Template " + si.Id + "Not found");
                }

            }

            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }//end(GetTemplates)

        void SoccerApp_OnUnloadPlayer(object sender, PlayerArgs e)
        {

        }

        /// <summary>
        /// clear objects and unbind events
        /// </summary>
        private void Shutdown()
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Events

        string ID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UdtFilter filter;
            try
            {
                filter = new UdtFilter();
                filter.FilterColumn = "Name";
                filter.FilterValue = cmbMatch.Text;
                filter.TableIndex = 10;
                if (!m_objUDT.UdtFilters.ContainsKey("Active Match"))
                    m_objUDT.UdtFilters.Add("Active Match", filter);
                else
                    m_objUDT.UdtFilters["Active Match"] = filter;

                foreach (string item in cmbMatch.Items)
                {
                    System.Diagnostics.Trace.WriteLine(item);

                    if (item == cmbMatch.Text)
                        m_objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatch.Text);
                    else
                        m_objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "false" }, "Name", item);
                }
                m_objUDT.Notify("Active Match");
                ActiveMatch = cmbMatch.Text;
                if (m_objUDT != null)
                {
                    DataTable dt = m_objUDT.CurrentDataSet.Tables["13"];
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if ((bool)row["Active"])
                            {
                                ID = row["ID"].ToString();
                            }
                        }
                    }
                }
                //Select Teams based on the selected Match
                SelectTeams(ActiveMatch);

                dgvMatchevents.Rows.Clear();
                FillMatchEvents();
                //UpdateWaspControls(m_objfrm);
                //If there is no entry in Match Status table, insert new row
                DataRow[] dr = m_objUDT.CurrentDataSet.Tables[17].Select("Match= '" + ActiveMatch + "'");
                if (dr.Length == 0)
                {
                    m_objUDT.InsertUDTData(17, new string[] { "Match" }, new string[] { ActiveMatch });
                }

            }
            catch (Exception ex)
            {
            }
        }//end(cmbMatch_SelectedIndexChanged)

        private void cmbMatchPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCounter.Enabled = false;
            UdtFilter filter = new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = cmbMatch.Text;
            filter.TableIndex = 11;
            if (!m_objUDT.UdtFilters.ContainsKey("Match Part"))
                m_objUDT.UdtFilters.Add("Match Part", filter);
            else
                m_objUDT.UdtFilters["Match Part"] = filter;

            foreach (string item in cmbMatchPart.Items)
            {
                if (item == cmbMatchPart.Text)
                    m_objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatchPart.Text);
                else
                    m_objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "false" }, "Name", item);
            }

            m_objUDT.Notify("Match Part");
            timer1.Stop();
            lblCounter.Text = "00:00";
            btnstartstop.Text = "Start";
            if (cmbMatchPart.Text == "Extra Time")
            {
                lblCounter.Enabled = true;
                
            }
        }//end(cmbMatchPart_SelectedIndexChanged)

        /// <summary>
        /// Do Action on home click, insert values to UDT. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhomeplus_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
                lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
                m_objUDT.UpdateUDT(10, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text);
                Player p = new Player();
                p.Team = lblHomeTeam2.Text;
                p._objUDTProvider = m_objUDT;
                p.FillTeam();
                p.StartPosition = FormStartPosition.CenterParent;
                p.ShowDialog();

                string selectedPlayer = p.selectedPlayer;
                DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                dgv.Cells[0].Value = SrNo;
                dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                dgv.Cells[2].Value = cmbMatchPart.Text;
                dgv.Cells[3].Value = "Goal";
                dgv.Cells[4].Value = lblHomeTeam2.Text;
                dgv.Cells[5].Value = selectedPlayer;
                dgvMatchevents.Rows.Add(dgv);
                SrNo++;
                m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblHomeTeam2.Text, selectedPlayer });
                p = null;
                updateMatchStats("HomeGoal", 1, "Match", cmbMatch.Text);
            }
            catch (Exception ex)
            {

            }
        }//end(btnhomeplus_Click)

        private void btnHomeminus_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblHomeScore.Text) != 0)
            {
                lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) - 1).ToString();
                m_objUDT.UpdateUDT(10, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text);
                updateMatchStats("HomeGoal", -1, "Match", cmbMatch.Text);
            }
        }//end(btnHomeminus_Click)

        private void btnawayplus_Click(object sender, EventArgs e)
        {
            lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) + 1).ToString();
            m_objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
            //lblAwayScore.Text = (Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
            //m_objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
            Player p = new Player();
            p.Team = lblAwayTeam.Text;
            p._objUDTProvider = m_objUDT;
            p.FillTeam();
            p.StartPosition = FormStartPosition.CenterParent;
            p.ShowDialog();
            TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
            string selectedPlayer = p.selectedPlayer;
            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Goal";
            dgv.Cells[4].Value = lblAwayTeam.Text;
            dgv.Cells[5].Value = selectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            SrNo++;
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblAwayTeam.Text, selectedPlayer });
            p = null;
            updateMatchStats("AwayGoal", 1, "Match", cmbMatch.Text);
        }

        private void btnawayminus_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblAwayScore.Text) != 0)
            {
                lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) - 1).ToString();
                m_objUDT.UpdateUDT(10, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text);
                updateMatchStats("AwayGoal", -1, "Match", cmbMatch.Text);
            }
        }

        private void lblHomeTeam_DoubleClick(object sender, EventArgs e)
        {
            TeamBuilderForm tf = new TeamBuilderForm();
            tf.Team = lblHomeTeam2.Text;
            tf._objUDTProvider = m_objUDT;
            tf.FIllTeam();
            tf.StartPosition = FormStartPosition.CenterParent;
            tf.ShowDialog();
            tf = null;
        }

        private void lblAwayTeam_DoubleClick(object sender, EventArgs e)
        {
            TeamBuilderForm tf = new TeamBuilderForm();
            tf.Team = lblAwayTeam.Text;
            tf._objUDTProvider = m_objUDT;
            tf.FIllTeam();
            tf.StartPosition = FormStartPosition.CenterParent;
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
            //string str = null;
            bool bneedStart = false;
            if (btnstartstop.Text == "Start")
            {
                if (lblCounter.Text.Trim() == "00:00")
                {
                    if (cmbMatchPart.Text != "Extra Time")
                    {
                        CountDownTarget = DateTime.Now.AddMinutes(45.00);                       
                        m_objsceneHandler.TimerAction("update", "45,0,0,0", m_objPlayer);
                        m_objsceneHandler.TimerAction("start", "", m_objPlayer);
                        bneedStart = true;
                    }
                    else
                    {
                        ExtraTimeEditor ete = new ExtraTimeEditor();
                        ete.StartPosition = FormStartPosition.CenterParent;
                        ete.ShowDialog();
                        if (ete.extraTime > 0)
                        {
                            m_objsceneHandler.TimerAction("updateextra", ete.extraTime + ",0,0,0", m_objPlayer);
                            m_objsceneHandler.TimerAction("extrain", "", m_objPlayer);
                            CountDownTarget = DateTime.Now.AddMinutes(ete.extraTime);
                            m_objsceneHandler.TimerAction("extrastart", "", m_objPlayer);
                            bneedStart = true;
                        }
                        ete = null;
                    }
                    MatchPartStartTime = DateTime.Now;
                }
                else
                {
                    bneedStart = true;
                }
                if (bneedStart)
                {
                    timer1.Enabled = true;
                    timer1.Interval = 1000;
                    timer1.Start();
                    btnstartstop.Text = "Stop";
                }
            }
            else
            {
                if (cmbMatchPart.Text != "Extra Time")
                {
                    m_objsceneHandler.TimerAction("stop", "", m_objPlayer);
                }
                else
                {

                    m_objsceneHandler.TimerAction("stopextratime", "", m_objPlayer);
                }
                timer1.Stop();
                btnstartstop.Text = "Start";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            if (MatchPartStartTime.Year != 0001)
            {
                ts = DateTime.Now.Subtract(MatchPartStartTime);
            }

            Substitution sb = new Substitution();
            sb._objUDTProvider = m_objUDT;
            sb.HomeTeam = lblHomeTeam2.Text;
            sb.AwayTeam = lblAwayTeam.Text;
            sb.Team = lblHomeTeam2.Text;
            sb.Initialize();
            sb.StartPosition = FormStartPosition.CenterParent;
            sb.ShowDialog();

            //string selectedInPlayer = p.selectedPlayer;
            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Substitution Out";
            dgv.Cells[4].Value = sb.Team;
            dgv.Cells[5].Value = sb.SelectedOutPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution Out", sb.Team, sb.SelectedOutPlayer });
            SrNo++;
            dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
            dgv.Cells[0].Value = SrNo;
            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            dgv.Cells[2].Value = cmbMatchPart.Text;
            dgv.Cells[3].Value = "Substitution In";
            dgv.Cells[4].Value = sb.Team;
            dgv.Cells[5].Value = sb.SelectedInPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution In", sb.Team, sb.SelectedInPlayer });
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
            pd._objUDTProvider = m_objUDT;
            pd.Parent = "Foul";
            pd.Team = lblHomeTeam2.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam2.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.StartPosition = FormStartPosition.CenterParent;
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Fouls";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Fouls", pd.Team, pd.SelectedPlayer });
            SrNo++;
            string FoulTeam = "";
            if (pd.Team == lblHomeTeam2.Text)
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
            tms.cmbTeam.Items.Add(lblHomeTeam2.Text);
            tms.cmbTeam.Items.Add(lblAwayTeam.Text);
            tms.StartPosition = FormStartPosition.CenterParent;
            tms.ShowDialog();
            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Corner kicks";
            dgv.Cells[4].Value = tms.SelectedTeam;
            dgv.Cells[5].Value = "";
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Corner kicks", tms.SelectedTeam, "" });
            SrNo++;

            string FoulTeam = "";
            if (tms.SelectedTeam == lblHomeTeam2.Text)
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
            pd._objUDTProvider = m_objUDT;
            pd.Parent = "Shots ON";
            pd.Team = lblHomeTeam2.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam2.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.StartPosition = FormStartPosition.CenterParent;
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Shots On Goal";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots On Goal", pd.Team, pd.SelectedPlayer });
            SrNo++;
            string FoulTeam = "";
            if (pd.Team == lblHomeTeam2.Text)
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
            pd._objUDTProvider = m_objUDT;
            pd.Parent = "Shots OFF";
            pd.Team = lblHomeTeam2.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam2.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.StartPosition = FormStartPosition.CenterParent;
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Shots Off Goal";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots Off Goal", pd.Team, pd.SelectedPlayer });
            SrNo++;
            string FoulTeam = "";
            if (pd.Team == lblHomeTeam2.Text)
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
            pd._objUDTProvider = m_objUDT;
            pd.Parent = "Yellow Card";
            pd.Team = lblHomeTeam2.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam2.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.StartPosition = FormStartPosition.CenterParent;
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Yellow Cards";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Yellow Cards", pd.Team, pd.SelectedPlayer });
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
            pd._objUDTProvider = m_objUDT;
            pd.Parent = "Red Card";
            pd.Team = lblHomeTeam2.Text;
            pd.cmbTeam.Items.Add(lblHomeTeam2.Text);
            pd.cmbTeam.Items.Add(lblAwayTeam.Text);
            pd.Initialize();
            pd.StartPosition = FormStartPosition.CenterParent;
            pd.ShowDialog();

            DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
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
            dgv.Cells[3].Value = "Red Cards";
            dgv.Cells[4].Value = pd.Team;
            dgv.Cells[5].Value = pd.SelectedPlayer;
            dgvMatchevents.Rows.Add(dgv);
            m_objUDT.InsertUDTData(12, new string[] { "EventID", "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatch.Text, cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Red Cards", pd.Team, pd.SelectedPlayer });
            SrNo++;
        }

        void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSelectPlayer.IsCurrentCellDirty)
                {
                    dgvSelectPlayer.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }

        }

        /// <summary>
        /// Event to handle cell value changed for loading corresponding templates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvSelectPlayer.Update();
                if (dgvSelectPlayer.Columns.IndexOf(dgvSelectPlayer.Columns["Column2"]) == e.ColumnIndex)
                {
                    int currentcolumnclicked = e.ColumnIndex;
                    int currentrowclicked = e.RowIndex;
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[currentcolumnclicked].Value = false;
                    }
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].Value = true;
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].ReadOnly = true;
                }

                if (dgvSelectPlayer.Columns.IndexOf(dgvSelectPlayer.Columns["Column3"]) == e.ColumnIndex)
                {
                    int currentcolumnclicked = e.ColumnIndex;
                    int currentrowclicked = e.RowIndex;
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[currentcolumnclicked].Value = false;
                    }
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].Value = true;
                }

                if (e.ColumnIndex == dgvSelectPlayer.Columns["Column2"].Index)
                {
                    //dataGridView1.EndEdit();
                    if ((bool)dgvSelectPlayer.Rows[e.RowIndex].Cells["Column2"].Value)
                    {
                        string sTemplate = dgvSelectPlayer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        string sCol = "Column2";
                        GetTemplate(sTemplate, sCol);
                    }
                }

                if (e.ColumnIndex == dgvSelectPlayer.Columns["Column3"].Index)
                {
                    if ((bool)dgvSelectPlayer.Rows[e.RowIndex].Cells["Column3"].Value)
                    {
                        string sTemplate = dgvSelectPlayer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        string sCol2 = "Column3";
                        GetTemplate(sTemplate, sCol2);
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }


        }

        void objPlayer_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.SGDELETED)
            {
                //objPlayer.DeleteSg();
            }
        }

        void objPlayer_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PLAYCOMPLETE)
            {
                //objPlayer.DeleteSg();
            }
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.SGDELETED)
            {
                //objPlayer.DeleteSg();
            }
        }

        #endregion

        #region Unused Code



        //void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    if (e.ColumnIndex == dataGridView2.Columns["Column2"].Index)
        //    {
        //        //dataGridView1.EndEdit();
        //        if ((bool)dataGridView2.Rows[e.RowIndex].Cells["Column2"].Value)
        //        {
        //            MessageBox.Show("Checked");
        //        }
        //    }




        //foreach (DataGridViewRow row in dataGridView2.Rows)
        //{
        //    DataGridViewCheckBoxCell chk = row.Cells["Column2"] as DataGridViewCheckBoxCell;
        //    DataGridViewCheckBoxCell chk2 = row.Cells["Column3"] as DataGridViewCheckBoxCell;

        //    if (Convert.ToBoolean(chk2.Value) == true)
        //        MessageBox.Show("this cell checked");

        //    bool bChecked = (null != chk && null != chk.Value && true == (bool)chk.Value);
        //    if (true == bChecked)
        //    {
        //        MessageBox.Show("Checked too");
        //    }
        //if (row.Cells["Column2"].Value != null && (bool)row.Cells["Column2"].Value)
        //{
        //    GetTemplate();
        //}

        //if (((e.ColumnIndex) == 1) && ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value))
        //{
        //    GetTemplate();

        //}

        //     if (row.Cells["Column2"].Value != null  && (bool)row.Cells["Column2"].Value)
        //     {
        //         GetTemplate();
        //     }

        //     if (((e.ColumnIndex) == 2) && ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value))
        //     {
        //         GetTemplate();

        //     }


        //if (e.ColumnIndex == [])
        //   CheckForCheckedValue();


        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvSelectPlayer.Rows)
            {

                if (Convert.ToBoolean(row.Cells[1].Value))
                {
                    // what you want to do
                }
            }


        }
        //if (e.RowType == DataControlRowType.DataRow)
        //{
        //    var control = e.Row.Cell[cellIndex].FindControl("ControlID");
        //    e.Row.Cells[1].Text = ((TypeOfControl)control).Text;
        //}


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //ScenInfo si = _SceneCollection.Where(s => s.Description == listBox1.Text).FirstOrDefault();
            //STemplateDetails obj = objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");
            //if (obj != null)
            //{
            //    Form obj1 = Activator.CreateInstance(obj.TemplatePlayerInfo) as Form;
            //    obj1.TopLevel = false;
            //    obj1.TopLevel = false;
            //    obj1.Dock = DockStyle.Fill;
            //    tableLayoutPanel1.Controls.Add(obj1);
            //    obj1.Show();                
            //}
            //else
            //{
            //    MessageBox.Show("Template " + si.Id + "Not found");
            //}

            //ScenInfo si = _SceneCollection.Where(s => s.Description == listBox1.Text).FirstOrDefault();
            //STemplateDetails obj = objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");

            //if (obj != null)
            //{
            //    Form obj1 = Activator.CreateInstance(obj.TemplatePlayerInfo) as Form;
            //    objPlayer = obj1 as IPlayer;
            //    string sLinkID = string.Empty;              
            //    if (objPlayer != null)
            //    {
            //        objPlayer.Init("", "", si.Id, "");
            //        objPlayer.SetLink(_objsceneHandler.AppLink , obj.Scene);
            //            //S. No 116: Added for AddIn
            //                //S.No.	: -	128
            //        if (objPlayer is IAddinInfo)
            //            (objPlayer as IAddinInfo).Init(new InstanceInfo() { Type = "wsp", InstanceId = "", TemplateId = si.Id, ThemeId = "Default", });


            //            IChannelShotBox objChannelShotBox = objPlayer as IChannelShotBox;
            //            if (objChannelShotBox != null)
            //            {
            //                if (m_objLinkType == LINKTYPE.TCP)
            //                    sEngineUrl = m_surl;
            //                        //newplaylistinstance.ActiveServer.GetUrl(CConstants.Constants.TCP);
            //                //S.No.: -	147
            //                objChannelShotBox.SetEngineUrl(m_surl);
            //            }                        
            //            objPlayer.Prepare(sEngineUrl, Convert.ToInt32(objPlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
            //    }
            //    obj1.TopLevel = false;
            //    obj1.Visible = true;
            //    obj1.Dock = DockStyle.Fill;
            //    var ctl = tableLayoutPanel1.GetControlFromPosition(1, RowPos);
            //    if (ctl != null) // Already control present there
            //    {
            //        //First play out the graphic.
            //        //then delete the gfx
            //        tableLayoutPanel1.Controls.Remove(ctl);                  
            //    }
            //    tableLayoutPanel1.Controls.Add(obj1, 1, RowPos);
            //    if (RowPos == 1)
            //        RowPos--;
            //    else
            //        RowPos++;
            //    obj1.Show();

            //}
            //else
            //{
            //    MessageBox.Show("Template " + si.Id + "Not found");
            //}

        }


        //ScenInfo si = _SceneCollection.Where(s => s.Description == listBox1.Text).FirstOrDefault();
        //STemplateDetails obj = objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");
        //if (obj != null)
        //{
        //    Form obj1 = Activator.CreateInstance(obj.TemplatePlayerInfo) as Form;
        //    obj1.TopLevel = false;
        //    obj1.TopLevel = false;
        //    obj1.Dock = DockStyle.Fill;
        //    tableLayoutPanel1.Controls.Add(obj1);
        //    obj1.Show();                
        //}
        //else
        //{
        //    MessageBox.Show("Template " + si.Id + "Not found");
        //}

        //Aayushi 
        public void UpdateWaspControl2()
        {
            //   xdDoc.LoadXml(str);
            //XmlNode xn;//= xdDoc.SelectSingleNode("//data/userdata/requery/connection/query");
            //string str1 = String.Format(sDataXml, cmbMatch.Text);
            //IDataEntry cd = (objfrm) as IDataEntry;
            //// xdDoc.RemoveAll();
            //// xdDoc = null;            
            //XmlDocument objdoc = new XmlDocument();
            //objdoc.LoadXml(str1);
            ////xn.RemoveAll();
            ////xn = null;
            //xn = objdoc.Document;// DocumentElement;
            //cd.SetData(xn);

            //foreach (Control item in tableLayoutPanel1.Controls)
            //{


            //}
        }


        #endregion
    }
}
