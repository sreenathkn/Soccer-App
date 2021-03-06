﻿using System;
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
using System.Reflection;
namespace SoccerApp
{
    public partial class SoccerApp : Form
    {
        #region Class Members

        private UDTProvider.UDTProvider m_objUDTMatchSchedule;
        private UDTProvider.UDTProvider m_objUDTMatch;
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
        string m_sEngineUrl = null;
        string NAME = string.Empty;
        string EVENTID = string.Empty;
        string MATCHNAME = string.Empty;
        string MATCHUDTNAME = string.Empty;
        string MATCHUDTID = string.Empty;
        IPlayer m_objplayertodelete = null;
        IPlayer m_objplayerbg = null;

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

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            try
            {
                m_objWaspFileHandler = new CWaspFileHandler();
                m_objsceneHandler = new SceneHandler();
                SetUI();
                InitializeWasp();
                InitilizeMatchScheduleUDT();
                InitializeCombos();
                InitializeMatchUDT();
                FillMatchDetails();
                //m_lstSceneCollection = new List<ScenInfo>();
                //FillPlayerList();
                Fillgrid();
                //m_objsceneHandler.Initialize();
                m_objsceneHandler.FileHandler = m_objWaspFileHandler;
                m_objsceneHandler.Init();
            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetUI()
        {
            lblCounter.ForeColor = Color.FromArgb(0, 142, 188);
            pnlHmTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlAwayTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlcntr.BackColor = Color.FromArgb(228, 228, 228);
            pnlMActions.BackColor = Color.FromArgb(86, 110, 123);
            btnFoul.BackColor = Color.FromArgb(182, 182, 182);
            btnYellow.BackColor = Color.FromArgb(182, 182, 182);
            btnShots.BackColor = Color.FromArgb(182, 182, 182);
            btnShotsOff.BackColor = Color.FromArgb(182, 182, 182);
            btnCorner.BackColor = Color.FromArgb(182, 182, 182);
            btnRed.BackColor = Color.FromArgb(182, 182, 182);
            btnSubstitute.BackColor = Color.FromArgb(182, 182, 182);
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

            cmbMatch.SelectedIndex = -1;
            cmbMatchPart.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeWasp()
        {
            try
            {
                m_sCommonPath = Environment.GetEnvironmentVariable("Wasp3.5");

                var configfile = Path.Combine(m_sCommonPath, "CommonConfig.config");

                XDocument xdoc = XDocument.Load(configfile);
                var url = from lv1 in xdoc.Descendants("add")
                          where lv1.Attribute("key").Value == "REMOTEMANAGERURL"
                          select lv1.Attribute("value").Value;
                m_objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
                ConnectionStatus info = m_objRemoteHelper.Connect();
                ServiceUrl udtmgrserviceurl = CRemoteHelper.GetDisconnectedUrl("UDTManager");
                m_ObjUdtHandler = new CUDTManagerHelper(udtmgrserviceurl);
                ServiceUrl templatemgrserviceurl = CRemoteHelper.GetDisconnectedUrl("TemplateManager");
                m_objWaspFileHandler = new CWaspFileHandler();
                m_objWaspFileHandler.Initialize(templatemgrserviceurl);
                LoadTemplates();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadTemplates()
        {
            try
            {
                if (m_lstSceneCollection == null)
                {
                    m_lstSceneCollection = new List<ScenInfo>();
                }
                XDocument xdoc = XDocument.Load("templates.xml");
                var templates = xdoc.Descendants("template");
                foreach (var item in templates)
                {
                    ScenInfo sc = new ScenInfo();
                    sc.Id = item.Attribute("id").Value;
                    sc.Name = item.Attribute("name").Value;
                    sc.Description = item.Attribute("description").Value;
                    bool ismanual = false;
                    bool.TryParse(item.Attribute("ismanual").Value, out ismanual);
                    sc.ismanual = ismanual;
                    sc.inuse = false;
                    m_lstSceneCollection.Add(sc);

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitilizeMatchScheduleUDT()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["udtname"]))
            {
                m_objUDTMatchSchedule = new UDTProvider.UDTProvider();
                m_objUDTMatchSchedule.InitializeConnection();
                m_objUDTMatchSchedule.InitializeUDT(ConfigurationManager.AppSettings["udtname"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeMatchUDT()
        {
            if (!string.IsNullOrEmpty(MATCHUDTNAME))
            {
                var udtargs = m_ObjUdtHandler.GetUdtByName(MATCHUDTNAME);
                if (udtargs != null)
                {
                    MATCHUDTID = udtargs.ID;
                }
                m_objUDTMatch = new UDTProvider.UDTProvider();
                m_objUDTMatch.InitializeConnection();
                m_objUDTMatch.InitializeUDT(MATCHUDTNAME);
            }
            else
            {
                MATCHUDTID = string.Empty;
                m_objUDTMatch = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCombos()
        {
            cmbMatch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // Get the Match List from UDT and add to cmbstr, Fill the Combo, and set the selected Item
            FillMatchList();
            cmbMatchPart.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatchPart.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbMatchPart.SelectedIndex = 0;

            // Get the Match part List from UDT and add to cmbstr1, Fill the Combo, and set the selected Item
            //FillMatchPart();

        }

        /// <summary>
        /// 
        /// </summary>
        private void FillMatchList()
        {
            try
            {
                DataSet dt = m_objUDTMatchSchedule.CurrentDataSet;
                var dtmatch = dt.Tables[1];
                AutoCompleteStringCollection cmbstr = new AutoCompleteStringCollection();
                foreach (DataRow item in dtmatch.Rows)
                {
                    cmbstr.Add(item["Name"].ToString());
                    cmbMatch.Items.Add(item["Name"].ToString());
                }
                cmbMatch.AutoCompleteCustomSource = cmbstr;
                DataRow[] dr = dtmatch.Select("Active=true");
                if (dr.Count() > 0)
                {
                    MATCHUDTNAME = dr[0]["UDT Name"].ToString();
                    int index = cmbMatch.FindString(dr[0]["Name"].ToString());
                    if (index != -1)
                    {
                        try
                        {
                            cmbMatch.SelectedIndex = index;
                        }
                        catch (Exception ex)
                        {

                        }
                        UdtFilter filter = new UdtFilter();
                        filter.FilterColumn = "Name";
                        filter.FilterValue = cmbMatch.Text;
                        filter.TableIndex = 5;
                        if (!m_objUDTMatchSchedule.UdtFilters.ContainsKey("Active Match"))
                            m_objUDTMatchSchedule.UdtFilters.Add("Active Match", filter);
                        else
                            m_objUDTMatchSchedule.UdtFilters["Active Match"] = filter;
                        m_objUDTMatchSchedule.Notify("Active Match");
                    }

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Set active match udt name and initialize its udts
        /// </summary>
        /// <param name="matchname"></param>
        private void SetMatchUDTName(string matchname)
        {
            try
            {
                DataSet dt = m_objUDTMatchSchedule.CurrentDataSet;
                var dtmatch = dt.Tables[1];
                DataRow[] dr = dtmatch.Select("Active=true");
                MATCHUDTNAME = dr[0]["UDT Name"].ToString();
                InitializeMatchUDT();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillMatchEvents()
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    DataTable dt = m_objUDTMatch.CurrentDataSet.Tables[5];
                    foreach (DataRow item in dt.Rows)
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
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Selects the team on the basis of active match
        /// </summary>
        /// <param name="ActiveMatch"></param>
        private void SelectTeams(string ActiveMatch)
        {
            try
            {
                DataTable dtmatch = m_objUDTMatchSchedule.CurrentDataSet.Tables[1];
                DataRow[] dr = dtmatch.Select("Name= '" + ActiveMatch + "'");
                if (dr.Count() > 0)
                {
                    lblHomeTeam.Text = dr[0]["Home Team"].ToString();
                    lblAwayTeam.Text = dr[0]["Away Team"].ToString();
                    if (File.Exists(dr[0]["HomeTeam_Logo"].ToString()))
                    {
                        pnlHomeFlag.BackgroundImage = Image.FromFile(dr[0]["HomeTeam_Logo"].ToString());
                    }
                    if (File.Exists(dr[0]["AwayTeam_Logo"].ToString()))
                    {
                        pnlAwayFlag.BackgroundImage = Image.FromFile(dr[0]["AwayTeam_Logo"].ToString());
                    }
                    InitializeScores(ActiveMatch);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActiveMatch"></param>
        private void InitializeScores(string ActiveMatch)
        {
            try
            {
                DataRow[] dr = m_objUDTMatch.CurrentDataSet.Tables[4].Select("Match= '" + ActiveMatch + "'");
                lblHomeScore.Text = dr[0]["HomeGoal"].ToString();
                lblAwayScore.Text = dr[0]["AwayGoal"].ToString();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="value"></param>
        /// <param name="uniqueColumn"></param>
        /// <param name="uniqueValue"></param>
        private void updateMatchStats(string Column, int value, string uniqueColumn, string uniqueValue)
        {
            try
            {
                m_objUDTMatch.RefreshUDT(MATCHUDTNAME); // It will refresh the UDT and Dataset and get latest values available in UDT
                if (m_objUDTMatch.CurrentDataSet != null)
                {
                    DataRow[] dr = m_objUDTMatch.CurrentDataSet.Tables[4].Select(uniqueColumn + "= '" + uniqueValue + "'");
                    if (dr != null && dr.Length > 0 && (Convert.ToInt32(dr[0][Column]) > 0 || value == 1))
                    {
                        int val = Convert.ToInt32(dr[0][Column].ToString()) + value;
                        m_objUDTMatch.UpdateUDT(4, new string[] { Column }, new string[] { val.ToString() }, uniqueColumn, uniqueValue);
                    }
                    else if (value != -1)
                    {
                        DataRow drlast = m_objUDTMatch.CurrentDataSet.Tables[4].Rows[m_objUDTMatch.CurrentDataSet.Tables[4].Rows.Count - 1];
                        int id = 0;
                        if (drlast != null)
                        {
                            int.TryParse(Convert.ToString(drlast["ID"]), out id);
                        }
                        id++;
                        int val = value;
                        m_objUDTMatch.InsertUDTData(4, new string[] { "ID", uniqueColumn, Column }, new string[] { id.ToString(), uniqueValue, val.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// Fills the Datagridview to select players.
        /// </summary>
        private void Fillgrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Column2", typeof(bool));
                dt.Columns.Add("Column3", typeof(bool));
                foreach (var item in m_lstSceneCollection)
                {
                    if (item.ismanual)
                    {
                        dr = dt.NewRow();
                        dr["Name"] = item.Description;
                        dr["Column2"] = false;
                        dr["Column3"] = false;
                        dt.Rows.Add(dr);
                    }
                }
                dgvSelectPlayer.DataSource = dt;
                dgvSelectPlayer.CellContentClick += dataGridView2_CellContentClick;
                dgvSelectPlayer.CurrentCellDirtyStateChanged += dataGridView2_CurrentCellDirtyStateChanged;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// Method to get template details and player
        /// </summary>
        /// <param name="str"></param>
        /// <param name="column"></param>
        private void GetTemplate(string str, string column, bool IsID)
        {
            try
            {
                ScenInfo si = m_lstSceneCollection.Where(s => s.Description == str).FirstOrDefault();

                STemplateDetails obj = m_objWaspFileHandler.GetTemplatePlayerInfo(si.Id, "");

                if (obj != null)
                {
                    m_objPlayer = Activator.CreateInstance(obj.TemplatePlayerInfo) as IPlayer;
                    Form objfrm = m_objPlayer as Form;

                    SetMatchUDT(objfrm);

                    //UpdateWaspControls(objfrm, IsID);

                    string sLinkID = string.Empty;
                    if (m_objPlayer != null)
                    {
                        m_objPlayer.Init("", "", si.Id, "");
                        m_objPlayer.SetLink(m_objsceneHandler.AppLink, obj.Scene);
                        //S. No 116: Added for AddIn
                        //S.No.	: -	128
                        if (m_objPlayer is IAddinInfo)
                            (m_objPlayer as IAddinInfo).Init(new InstanceInfo() { InstanceId = si.Id });
                        //    (m_objPlayer as IAddinInfo).Init(new InstanceInfo() { Type = "wsp", InstanceId = "", TemplateId = si.Id, ThemeId = "Default", });


                        IDataEntry objDataentry = m_objPlayer as IDataEntry;
                        if (objDataentry != null)
                        {
                            objDataentry.PostData += objDataentry_PostData;
                        }

                        IChannelShotBox objChannelShotBox = m_objPlayer as IChannelShotBox;
                        if (objChannelShotBox != null)
                        {
                            if (m_objLinkType == LINKTYPE.TCP)
                                m_sEngineUrl = ConfigurationManager.AppSettings["stingserver"];
                            //newplaylistinstance.ActiveServer.GetUrl(CConstants.Constants.TCP);
                            //S.No.: -	147
                            objChannelShotBox.SetEngineUrl(ConfigurationManager.AppSettings["stingserver"]);
                        }

                        m_objPlayer.OnShotBoxControllerStatus += objPlayer_OnShotBoxControllerStatus;
                        m_objPlayer.Prepare(m_sEngineUrl, Convert.ToInt32(m_objPlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
                    }
                    if (column != "-1")
                    {
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

        }

        /// <summary>
        /// Set match udt name in the udt sequecer object
        /// </summary>
        /// <param name="frmobj"></param>
        private void SetMatchUDT(Form frmobj)
        {

            Control frmctrl = frmobj as Control;

            foreach (Control ctrl in frmobj.Controls)
            {
                if (ctrl is BeeSys.Wasp3D.Controls2.TextBox)
                {
                    var matchudtnamectrl = ctrl as BeeSys.Wasp3D.Controls2.TextBox;
                    matchudtnamectrl.Text = MATCHUDTNAME;
                }
            }

            FieldInfo[] objfieldinfo = frmctrl.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var objfield in objfieldinfo)
            {
                Type objFieldType = objfield.FieldType;
                object o = objfield.GetValue(frmctrl);
                if (o != null)
                {
                    Component objComponent = o as Component;
                    if (objComponent != null && objComponent is UDT)
                    {
                        if (!string.IsNullOrEmpty(MATCHUDTID))
                        {
                            UDT objudt = (UDT)objComponent;
                            objudt.SelectedUdtId = MATCHUDTID;
                            objudt.UDTNames = MATCHUDTNAME;
                            objudt.ExecuteQuery(MATCHUDTID, objudt.Filter);
                            break;
                        }
                    }
                }
            }
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

        /// <summary>
        /// Loading match events player 
        /// </summary>
        /// <param name="eventid"></param>
        private void LoadMatchEventGrahicPlayer(int eventid)
        {
            try
            {
                bool oldP1 = false;
                bool oldP2 = false;
                MatchGraphicPlayerSelector mgps = new MatchGraphicPlayerSelector();
                oldP1 = PlayerControlCheck(1, 0);
                oldP2 = PlayerControlCheck(2, 1);
                mgps.P1 = oldP1;
                mgps.P2 = oldP2;
                mgps.StartPosition = FormStartPosition.CenterParent;
                mgps.ShowDialog();
                if (mgps.P1)
                {
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[1].Value = false;
                    }
                    if (oldP1 != mgps.P1)
                    {
                        GetTemplate("matchevent", "Column2", false);
                    }
                }
                if (mgps.P2)
                {
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[2].Value = false;
                    }
                    if (oldP2 != mgps.P2)
                    {
                        GetTemplate("matchevent", "Column3", false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// check player controls
        /// </summary>
        /// <param name="columnindex"></param>
        /// <param name="columnpos"></param>
        /// <returns></returns>
        private bool PlayerControlCheck(int columnindex, int columnpos)
        {

            bool isAllfalse = true;

            foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
            {
                if ((bool)dr.Cells[columnindex].Value != false)
                {
                    isAllfalse = false;
                    break;
                }
            }
            if (isAllfalse)
            {
                if (tableLayoutPanel1.GetControlFromPosition(columnpos, 0) != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillMatchDetails()
        {
            try
            {
                UdtFilter filter = new UdtFilter();
                filter.FilterColumn = "Name";
                filter.FilterValue = cmbMatch.Text;
                filter.TableIndex = 1;
                if (!m_objUDTMatchSchedule.UdtFilters.ContainsKey("Active Match"))
                    m_objUDTMatchSchedule.UdtFilters.Add("Active Match", filter);
                else
                    m_objUDTMatchSchedule.UdtFilters["Active Match"] = filter;

                foreach (string item in cmbMatch.Items)
                {
                    System.Diagnostics.Trace.WriteLine(item);

                    if (item == cmbMatch.Text)
                    {
                        m_objUDTMatchSchedule.UpdateUDT(1, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatch.Text);
                    }
                    else
                    {
                        m_objUDTMatchSchedule.UpdateUDT(1, new string[] { "Active" }, new string[] { "false" }, "Name", item);
                    }
                }
                m_objUDTMatchSchedule.Notify("Active Match");
                ActiveMatch = cmbMatch.Text;
                if (m_objUDTMatchSchedule != null)
                {
                    DataTable dt = m_objUDTMatchSchedule.CurrentDataSet.Tables["13"];
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if ((bool)row["Active"])
                            {
                                NAME = row["NAME"].ToString();
                                SetMatchUDTName(NAME);
                            }
                        }
                    }
                }
                SelectTeams(ActiveMatch);
                dgvMatchevents.Rows.Clear();
                FillMatchEvents();
                ClearPlayerControlls();
                lblMatchname.Text = lblHomeTeam.Text + " Vs " + lblAwayTeam.Text;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearPlayerControlls()
        {
            Control ctl1 = null;
            Control ctl2 = null;
            try
            {
                ctl1 = tableLayoutPanel1.GetControlFromPosition(0, 0);
                ctl2 = tableLayoutPanel1.GetControlFromPosition(1, 0);
                if (ctl1 != null)
                {
                    tableLayoutPanel1.Controls.Remove(ctl1);
                    (ctl1 as IPlayer).DeleteSg();
                }
                if (ctl2 != null)
                {
                    tableLayoutPanel1.Controls.Remove(ctl2);
                    (ctl2 as IPlayer).DeleteSg();
                }
                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                {
                    dr.Cells[1].Value = false;
                }
                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                {
                    dr.Cells[2].Value = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnabaleActions()
        {
            btnhomeplus.Enabled = true;
            btnHomeminus.Enabled = true;
            btnawayplus.Enabled = true;
            btnawayminus.Enabled = true;
            btnFoul.Enabled = true;
            btnYellow.Enabled = true;
            btnRed.Enabled = true;
            btnCorner.Enabled = true;
            btnShots.Enabled = true;
            btnShotsOff.Enabled = true;
            btnSubstitute.Enabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        private void DisableActions()
        {
            btnhomeplus.Enabled = false;
            btnHomeminus.Enabled = false;
            btnawayplus.Enabled = false;
            btnawayminus.Enabled = false;
            btnFoul.Enabled = false;
            btnYellow.Enabled = false;
            btnRed.Enabled = false;
            btnCorner.Enabled = false;
            btnShots.Enabled = false;
            btnShotsOff.Enabled = false;
            btnSubstitute.Enabled = false;
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="objNodeXml"></param>
        void objDataentry_PostData(object sender, XmlNode objNodeXml)
        {
            IPlayer objPlayer = sender as IPlayer;
            if (objNodeXml != null && objPlayer != null)
            {
                objPlayer.UpdateSceneGraph(objNodeXml.InnerXml, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillMatchDetails();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMatchPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCounter.Enabled = false;
                timer1.Stop();
                lblCounter.Text = "00:00";
                btnstartstop.Text = "Start";
                if (cmbMatchPart.Text == "Extra Time")
                {
                    lblCounter.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Do Action on home click, insert values to UDT. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhomeplus_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
                    lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
                    m_objUDTMatch.UpdateUDT(4, new string[] { "HomeGoal" }, new string[] { lblHomeScore.Text }, "Match", cmbMatch.Text);
                    Player p = new Player();
                    p.Team = lblHomeTeam.Text;
                    p._objUDTProvider = m_objUDTMatch;
                    p.FillTeam();
                    p.StartPosition = FormStartPosition.CenterParent;
                    p.ShowDialog();

                    string selectedPlayer = p.selectedPlayer;
                    DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                    dgv.Cells[0].Value = SrNo;
                    dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                    dgv.Cells[2].Value = cmbMatchPart.Text;
                    dgv.Cells[3].Value = "Goal";
                    dgv.Cells[4].Value = lblHomeTeam.Text;
                    dgv.Cells[5].Value = selectedPlayer;
                    dgvMatchevents.Rows.Add(dgv);
                    SrNo++;
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblHomeTeam.Text, selectedPlayer });
                    p = null;
                    updateMatchStats("HomeGoal", 1, "Match", cmbMatch.Text);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }//end(btnhomeplus_Click)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomeminus_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lblHomeScore.Text) != 0 && m_objUDTMatch != null)
                {
                    m_objUDTMatch.UpdateUDT(4, new string[] { "HomeGoal" }, new string[] { lblHomeScore.Text }, "Match", cmbMatch.Text);
                    lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) - 1).ToString();
                    updateMatchStats("HomeGoal", -1, "Match", cmbMatch.Text);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }//end(btnHomeminus_Click)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnawayplus_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) + 1).ToString();
                    m_objUDTMatch.UpdateUDT(4, new string[] { "AwayGoal" }, new string[] { lblAwayScore.Text }, "Match", cmbMatch.Text);
                    Player p = new Player();
                    p.Team = lblAwayTeam.Text;
                    p._objUDTProvider = m_objUDTMatch;
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblAwayTeam.Text, selectedPlayer });
                    p = null;
                    updateMatchStats("AwayGoal", 1, "Match", cmbMatch.Text);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnawayminus_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lblAwayScore.Text) != 0 && m_objUDTMatch != null)
                {
                    m_objUDTMatch.UpdateUDT(4, new string[] { "AwayGoal" }, new string[] { lblAwayScore.Text }, "Match", cmbMatch.Text);
                    lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) - 1).ToString();
                    updateMatchStats("AwayGoal", -1, "Match", cmbMatch.Text);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblHomeTeam_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                TeamBuilderForm tf = new TeamBuilderForm();
                tf.Team = lblHomeTeam.Text;
                tf._objUDTProvider = m_objUDTMatch;
                tf.FIllTeam();
                tf.StartPosition = FormStartPosition.CenterParent;
                tf.ShowDialog();
                tf = null;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAwayTeam_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TeamBuilderForm tf = new TeamBuilderForm();
                tf.Team = lblAwayTeam.Text;
                tf._objUDTProvider = m_objUDTMatch;
                tf.FIllTeam();
                tf.StartPosition = FormStartPosition.CenterParent;
                tf.ShowDialog();
                tf = null;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CountDownTarget < DateTime.Now)
                    timer1.Stop();
                else
                {
                    TimeSpan timeLeft = CountDownTarget.Subtract(DateTime.Now);
                    lblCounter.Text = timeLeft.Minutes.ToString("00") + ":" + timeLeft.Seconds.ToString("00");

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnstartstop_Click(object sender, EventArgs e)
        {
            try
            {
                bool bneedStart = false;
                if (m_objsceneHandler != null)
                {
                    if (btnstartstop.Text == "Start")
                    {
                        if (lblCounter.Text.Trim() == "00:00")
                        {
                            if (cmbMatchPart.Text != "Extra Time")
                            {
                                CountDownTarget = DateTime.Now.AddMinutes(45.00);
                                m_objsceneHandler.TimerAction("update", "45,0,0,0");
                                m_objsceneHandler.TimerAction("start", "");
                                bneedStart = true;
                            }
                            else
                            {
                                ExtraTimeEditor ete = new ExtraTimeEditor();
                                ete.StartPosition = FormStartPosition.CenterParent;
                                ete.ShowDialog();
                                if (ete.EXTRATIME > 0)
                                {
                                    m_objsceneHandler.TimerAction("updateextra", ete.EXTRATIME + ",0,0,0");
                                    m_objsceneHandler.TimerAction("extrain", "");
                                    CountDownTarget = DateTime.Now.AddMinutes(ete.EXTRATIME);
                                    m_objsceneHandler.TimerAction("extrastart", "");
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
                            EnabaleActions();
                        }
                    }
                    else
                    {
                        if (cmbMatchPart.Text != "Extra Time")
                        {
                            m_objsceneHandler.TimerAction("stop", "");
                        }
                        else
                        {

                            m_objsceneHandler.TimerAction("stopextratime", "");
                        }
                        timer1.Stop();
                        btnstartstop.Text = "Start";
                        DisableActions();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }

                    Substitution sb = new Substitution();
                    sb._objUDTProvider = m_objUDTMatch;
                    sb.HomeTeam = lblHomeTeam.Text;
                    sb.AwayTeam = lblAwayTeam.Text;
                    sb.Team = lblHomeTeam.Text;
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution Out", sb.Team, sb.SelectedOutPlayer });
                    SrNo++;
                    dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                    dgv.Cells[0].Value = SrNo;
                    dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                    dgv.Cells[2].Value = cmbMatchPart.Text;
                    dgv.Cells[3].Value = "Substitution In";
                    dgv.Cells[4].Value = sb.Team;
                    dgv.Cells[5].Value = sb.SelectedInPlayer;
                    dgvMatchevents.Rows.Add(dgv);
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution In", sb.Team, sb.SelectedInPlayer });
                    SrNo++;
                    sb = null;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFoul_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd._objUDTProvider = m_objUDTMatch;
                    pd.Parent = "Foul";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Fouls", pd.Team, pd.SelectedPlayer });
                    SrNo++;
                    string FoulTeam = "";
                    if (pd.Team == lblHomeTeam.Text)
                    {
                        FoulTeam = "HomeFoul";
                    }
                    else
                    {
                        FoulTeam = "AwayFoul";
                    }
                    updateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCorner_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    TeamSelection tms = new TeamSelection();
                    tms.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Corner kicks", tms.SelectedTeam, "" });
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
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShots_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd._objUDTProvider = m_objUDTMatch;
                    pd.Parent = "Shots ON";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots On Goal", pd.Team, pd.SelectedPlayer });
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
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShotsOff_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd._objUDTProvider = m_objUDTMatch;
                    pd.Parent = "Shots OFF";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots Off Goal", pd.Team, pd.SelectedPlayer });
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
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYellow_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd._objUDTProvider = m_objUDTMatch;
                    pd.Parent = "Yellow Card";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Yellow Cards", pd.Team, pd.SelectedPlayer });
                    SrNo++;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRed_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd._objUDTProvider = m_objUDTMatch;
                    pd.Parent = "Red Card";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
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
                    m_objUDTMatch.InsertUDTData(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Red Cards", pd.Team, pd.SelectedPlayer });
                    SrNo++;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                LogWriter.WriteLog(ex);
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
                        GetTemplate(sTemplate, sCol, true);
                    }
                }

                if (e.ColumnIndex == dgvSelectPlayer.Columns["Column3"].Index)
                {
                    if ((bool)dgvSelectPlayer.Rows[e.RowIndex].Cells["Column3"].Value)
                    {
                        string sTemplate = dgvSelectPlayer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        string sCol2 = "Column3";
                        GetTemplate(sTemplate, sCol2, true);
                    }
                }

            }

            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objPlayer_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            switch (e.SHOTBOXRESPONSE)
            {
                case SHOTBOXMSG.PLAYCOMPLETE:
                    m_objplayertodelete = sender as IPlayer;
                    if (m_objplayertodelete != null && BtnLoadBG.Text != "Unload BG")
                    {
                        m_objplayertodelete.DeleteSg();
                    }
                    break;
                case SHOTBOXMSG.SGDELETED:
                    if (m_objplayertodelete != null && m_objplayertodelete.Equals(sender as IPlayer))
                    {
                        TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(m_objplayertodelete as Form);
                        tableLayoutPanel1.Controls.Remove(m_objplayertodelete as Form);
                        switch (position.Column)
                        {
                            case 0:
                                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                                {
                                    dr.Cells[1].Value = false;
                                }
                                break;
                            case 1:
                                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                                {
                                    dr.Cells[2].Value = false;
                                }
                                break;
                        }
                        break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Match events double click event to open the player selection 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatchevents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int eventid = Convert.ToInt32(dgvMatchevents.Rows[e.RowIndex].Cells[0].Value);
                    if (eventid > 0)
                    {
                        LoadMatchEventGrahicPlayer(eventid);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadBG_Click(object sender, EventArgs e)
        {
            try
            {
                switch (BtnLoadBG.Text)
                {
                    case "Load BG":
                        BtnLoadBG.Text = "Unload BG";
                        ScenInfo si = m_lstSceneCollection.Where(s => s.Description == "bg").FirstOrDefault();
                        m_objsceneHandler.LoadBackground(si.Id);
                        break;
                    case "Unload BG":
                        BtnLoadBG.Text = "Load BG";
                        m_objsceneHandler.UnloadBackground();
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Unused Code

        ///// <summary>
        ///// Fill list of players from templatesXml
        ///// </summary>
        //private void FillPlayerList()
        //{
        //    try
        //    {
        //        XDocument xdoc = XDocument.Load("templates.xml");
        //        var templates = xdoc.Descendants("template");
        //        foreach (var item in templates)
        //        {
        //            ScenInfo sc = new ScenInfo();
        //            sc.Id = item.Attribute("id").Value;
        //            sc.Name = item.Attribute("name").Value;
        //            sc.Description = item.Attribute("description").Value;
        //            sc.inuse = false;
        //            m_lstSceneCollection.Add(sc);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogWriter.WriteLog(ex);
        //    }
        //}

        ///// <summary>
        ///// Update the Data Xml with the current match Id
        ///// </summary>
        ///// <param name="objfrm"></param>
        //private void UpdateWaspControls(Form item, bool IsID)
        //{
        //    try
        //    {
        //        if (item != null)
        //        {
        //            IAutomationDataEntry objIAutomationDataEntry = (item) as IAutomationDataEntry;
        //            string sDataXml = objIAutomationDataEntry.GetDataXml();

        //            XDocument xdoc = XDocument.Parse(sDataXml);

        //            XElement xe = xdoc.Descendants("query").FirstOrDefault();

        //            if (xe != null)
        //            {
        //                string str = xe.Value;

        //                XElement tablenodes = XElement.Parse(str);
        //                IEnumerable<XElement> elements = tablenodes.Descendants("table").Where(x => x.Attribute("name").Value == "13");

        //                foreach (XElement node in elements)
        //                {
        //                    if (IsID)
        //                    {
        //                        //Update ID  here.....    
        //                        node.Attribute("customfilter").SetValue("(T1_ID In (  1 )) AND (([NAME] =" + NAME + "))");
        //                        node.Attribute("filter").SetValue("([NAME] = " + NAME + ")");
        //                        node.Attribute("actionfilter").SetValue("[NAME] =" + NAME);
        //                    }
        //                    else
        //                    {
        //                        //Update EVENTID and MATCHNAME  here.....    
        //                        node.Attribute("customfilter").SetValue("(T1_ID In ( 1)) AND ((([EventID] = " + EVENTID + ") And ([MatchID] = '" + MATCHNAME + "')))");
        //                        node.Attribute("filter").SetValue("(([EventID] = " + EVENTID + ") And ([MatchID] ='" + MATCHNAME + "'))");
        //                        node.Attribute("actionfilter").SetValue("(([EventID] = " + EVENTID + ") And ([MatchID] ='" + MATCHNAME + "'))");
        //                    }
        //                }
        //                xe.ReplaceNodes(new XCData(tablenodes.ToString()));
        //                string updatedxml = xdoc.ToString();
        //                XmlNode xn;
        //                IDataEntry _objDataEntry = (item) as IDataEntry;
        //                XmlDocument objdoc = new XmlDocument();
        //                objdoc.LoadXml(updatedxml);
        //                xn = objdoc.DocumentElement;
        //                _objDataEntry.SetData(xn);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogWriter.WriteLog(ex);
        //    }

        //}


        ///// <summary>
        ///// 
        ///// </summary>
        //private void FillMatchPart()
        //{
        //    AutoCompleteStringCollection cmbstr1 = new AutoCompleteStringCollection();
        //    var dtmatchpart = m_objUDTMatchSchedule.CurrentDataSet.Tables[15];
        //    foreach (DataRow item in dtmatchpart.Rows)
        //    {
        //        cmbstr1.Add(item["Name"].ToString());
        //        cmbMatchPart.Items.Add(item["Name"].ToString());
        //    }
        //    cmbMatchPart.AutoCompleteCustomSource = cmbstr1;
        //    DataRow[] dr = dtmatchpart.Select("Active=true");
        //    if (dr.Count() > 0)
        //    {
        //        int index = cmbMatchPart.FindString(dr[0]["Name"].ToString());
        //        if (index != -1)
        //        {
        //            try
        //            {
        //                cmbMatchPart.SelectedIndex = index;
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //            UdtFilter filter = new UdtFilter();
        //            filter.FilterColumn = "Name";
        //            filter.FilterValue = cmbMatchPart.Text;
        //            filter.TableIndex = 5;
        //            if (!m_objUDTMatchSchedule.UdtFilters.ContainsKey("Match Part"))
        //                m_objUDTMatchSchedule.UdtFilters.Add("Match Part", filter);
        //            else
        //                m_objUDTMatchSchedule.UdtFilters["Match Part"] = filter;
        //            m_objUDTMatchSchedule.Notify("Match Part");
        //        }

        //    }

        //}


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
