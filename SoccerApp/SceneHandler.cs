using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Beesys.Wasp.Workflow;
using System.Threading;


namespace SoccerApp
{
    public class SceneHandler
    {
        #region Class Variables

        public bool isInitialized { get; set; }
        public bool isSceneLoaded { get; set; }
        ShotBox m_objBugPlayer;
        LinkManager _objLinkManager;
        const string m_surl = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        const string m_sport = "50011";
        const string m_IP = "192.168.1.192";
        string m_serverip = string.Format(m_surl, ConfigurationManager.AppSettings["stingserverip"], m_sport);
        string m_scorebugscenepath = string.Empty;
        IPlayer objBGPlayer = null;
        IPlayer objScorePlayer = null;

        public Link AppLink
        {
            get;
            set;
        }

        public CWaspFileHandler FileHandler
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            isInitialized = false;
            if (File.Exists(ConfigurationManager.AppSettings["scorebugscenepath"]))
            {
                m_scorebugscenepath = ConfigurationManager.AppSettings["scorebugscenepath"];
            }
            else if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Score Bug.wsl")))
            {
                m_scorebugscenepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Score Bug.wsl");
            }

            if (!string.IsNullOrEmpty(m_scorebugscenepath))
            {
                if (ConfigurationManager.AppSettings["stingserverip"] != null)
                {
                    string sLinkID = string.Empty;
                    _objLinkManager = new LinkManager();
                    AppLink = _objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                    AppLink.OnEngineConnected += new EventHandler<EngineArgs>(_objLink_OnEngineConnected);
                    AppLink.Connect(m_serverip);
                    _objLinkManager.OnEngineDisConnected += _objLinkManager_OnEngineDisConnected;
                }
            }
        }

        /// <summary>
        /// added for template loading
        /// </summary>
        public void Init()
        {
            try
            {
                _objLinkManager = new LinkManager();
                string sLinkID = string.Empty;
                AppLink = _objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                AppLink.OnEngineConnected += new EventHandler<EngineArgs>(_objLink_OnEngineConnected);
                AppLink.Connect(m_serverip);
            }
            catch(Exception ex)
            {

            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        private void LoadTemplate()
        {
            try
            {
                string templateid = ConfigurationManager.AppSettings["scorebugid"];
                STemplateDetails obj = FileHandler.GetTemplatePlayerInfo(templateid, "");
                if (obj != null)
                {
                    objScorePlayer = Activator.CreateInstance(obj.TemplatePlayerInfo) as IPlayer;
                    if (objScorePlayer != null)
                    {
                        objScorePlayer.Init("", "", templateid, "");
                        objScorePlayer.SetLink(AppLink, obj.Scene);
                        if (objScorePlayer is IAddinInfo)
                            (objScorePlayer as IAddinInfo).Init(new InstanceInfo() { InstanceId = templateid });

                        IChannelShotBox objChannelShotBox = objScorePlayer as IChannelShotBox;
                        if (objChannelShotBox != null)
                        {
                            objChannelShotBox.SetEngineUrl(m_serverip);
                        }

                        objScorePlayer.OnShotBoxControllerStatus += _objPlayer1_OnShotBoxControllerStatus;
                        objScorePlayer.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                        objScorePlayer.Prepare(m_serverip, Convert.ToInt32(objScorePlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Load background template
        /// </summary>
        /// <param name="templateid"></param>
        public void LoadBackground(string templateid)
        {
            try
            {
                STemplateDetails obj = FileHandler.GetTemplatePlayerInfo(templateid, "");
                if (obj != null)
                {
                    objBGPlayer = Activator.CreateInstance(obj.TemplatePlayerInfo) as IPlayer;
                    if (objBGPlayer != null)
                    {
                        objBGPlayer.Init("", "", templateid, "");
                        objBGPlayer.SetLink(AppLink, obj.Scene);
                        if (objBGPlayer is IAddinInfo)
                            (objBGPlayer as IAddinInfo).Init(new InstanceInfo() { InstanceId = templateid });

                        IChannelShotBox objChannelShotBox = objBGPlayer as IChannelShotBox;
                        if (objChannelShotBox != null)
                        {
                            objChannelShotBox.SetEngineUrl(m_serverip);
                        }

                        objBGPlayer.OnShotBoxControllerStatus += _objPlayer1_OnShotBoxControllerStatus;
                        objBGPlayer.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                        objBGPlayer.Prepare(m_serverip, Convert.ToInt32(objBGPlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnloadBackground()
        {
            if(objBGPlayer!=null)
            {
                objBGPlayer.PlayActionSet("Unload");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadScene()
        {
            string stemplateID = string.Empty;
            string sXml = string.Empty;
            string sDataXml = "<dgn><data></data></dgn>";
            string sShotBoxID = null;
            bool isTicker;
            sXml = Util.getSGFromWSL(m_scorebugscenepath);
            string filetype = Path.GetExtension(m_scorebugscenepath).Split(new string[] { "." }, StringSplitOptions.None)[1];
            if (!string.IsNullOrEmpty(sXml))
            {

                m_objBugPlayer = AppLink.GetShotBox(sXml, out sShotBoxID, out isTicker) as ShotBox;
                if (!Equals(m_objBugPlayer, null))
                {
                    m_objBugPlayer.SetEngineUrl(m_serverip);

                    InstanceInfo o = new InstanceInfo() { Type = filetype, InstanceId = string.Empty, TemplateId = m_scorebugscenepath, ThemeId = "default" };

                    if (m_objBugPlayer is IAddinInfo)
                        (m_objBugPlayer as IAddinInfo).Init(o);

                    //if (m_objBugPlayer == null)
                    //{
                    //m_objBugPlayer = new Beesys.Wasp.Workflow.Player();
                    //m_objBugPlayer.SetLink(_objLink, sXml);
                    //m_objBugPlayer.Prepare(m_serverip, 0, RENDERMODE.PROGRAM);
                    //}


                    m_objBugPlayer.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                    m_objBugPlayer.OnShotBoxControllerStatus += _objPlayer1_OnShotBoxControllerStatus;
                    m_objBugPlayer.Prepare(m_serverip, 0, RENDERMODE.PROGRAM);


                }//end (if)
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _objPlayer1_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                isInitialized = true;
                isSceneLoaded = true;

                if (sender != null)
                {
                    IPlayer playerobj = sender as IPlayer;
                    if (playerobj != null)
                        playerobj.Play();
                }
            }
            else if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PLAYCOMPLETE)
            {
                IPlayer playerobj = sender as IPlayer;
                if (playerobj.Equals(objBGPlayer)||playerobj.Equals(objScorePlayer))
                {
                    playerobj.DeleteSg();
                }
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _objPlayer1_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                isInitialized = true;
                isSceneLoaded = true;
                
                if (sender != null)
                {
                    IPlayer playerobj = sender as IPlayer;
                    if (playerobj != null)
                        playerobj.Play();
                }
            }
        }
       

        void _objLinkManager_OnEngineDisConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == ConfigurationManager.AppSettings["stingserver"])
            {
                isInitialized = false;
            }
        }

        void _objLink_OnEngineConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == ConfigurationManager.AppSettings["stingserver"])
            {
                isInitialized = true;
                //LoadScene();
                LoadTemplate();
            }
        }

        void objplayercontainer_OnEngineConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == ConfigurationManager.AppSettings["stingserver"])
            {
                isInitialized = true;
                //LoadScene();
                LoadTemplate();
            }
        }


        //public void UpdateScore(string Team,string score)
        //{
        //    if(isInitialized && isSceneLoaded)
        //    {
        //        TagData tg=new TagData();
        //        if(Team.ToLower()=="home")
        //        {
        //            tg.UserTags = new string[] { ConfigurationManager.AppSettings["homescoreusertag"] };
        //            tg.Values=new string[]{score};
        //            objplayer.UpdateSceneGraph(tg);
        //        }
        //        else
        //        {
        //            tg.UserTags = new string[] { ConfigurationManager.AppSettings["awayscoreusertag"] };
        //            tg.Values = new string[] { score };
        //            objplayer.UpdateSceneGraph(tg);
        //        }
        //    }
        //}
        public void TimerAction(string actiontype, string counter)
        {

            try
            {
                TagData tg = new TagData();
                switch (actiontype.ToLower())
                {
                    case "start":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["counterstartaction"]);
                        break;
                    case "stop":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["counterstopaction"]);
                        break;
                    case "update":
                        tg.UserTags = new string[] { ConfigurationManager.AppSettings["counterusertag"] };
                        tg.Values = new string[] { counter };

                        m_objBugPlayer.UpdateSceneGraph(tg);

                        break;
                    case "updateextra":
                        tg.UserTags = new string[] { ConfigurationManager.AppSettings["extratimeusertag"] };
                        tg.Values = new string[] { counter };
                        m_objBugPlayer.UpdateSceneGraph(tg);
                        break;
                    case "extrain":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["extratimein"]);
                        break;
                    case "extraout":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["extratimeout"]);
                        break;
                    case "extrastart":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["startextratime"]);
                        break;
                    case "extrastop":
                        m_objBugPlayer.PlayActionSet(ConfigurationManager.AppSettings["stopextratime"]);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }


    }

}

#region Ununsed Code
//private void InitializePalycontainer()
//{
//     objPlayer = obj_Player as IPlayer;
//             if (objPlayer != null)
//             {
//                 //what to do for tag???
//                 //objPlayer.Tag = newplaylistinstance.InstanceSlug;

//                 if (m_bRenderMode)
//                     objPlayer.RenderPath = CreateRenderPath(newplaylistinstance);

//                 objPlayer.RecordCount = newplaylistinstance.IterationRecordCount;
//                 objPlayer.Init(newplaylistinstance.ItemID, newplaylistinstance.InstanceSlug, newplaylistinstance.TemplateID, newplaylistinstance.TemplateSlug);                        
//                 objPlayer.SetLink(m_objLink, objTemplateDetails.Scene);
//                 //S. No 116: Added for AddIn
//                 try
//                 {
//                     //S.No.	: -	128
//                     if (obj_Player is IAddinInfo)
//                     {
//                         //S.No.: -			206
//                         //S.No.: -			177
//                         //S.No.: -			153                                 
//                         //(objPlayer as IAddinInfo).Init(new InstanceInfo() { Type = "wsp", InstanceId = (newplaylistinstance as IPlaylistInstance).ID, TemplateId = newplaylistinstance.TemplateID, ThemeId = objTemplateDetails.CurrentTheme, ViewPort = ViewPort, IsWired = IsWired });
//                         //S.No.			: -	233
//                         sTemplateType = newplaylistinstance.TemplateSlug.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
//                         (objPlayer as IAddinInfo).Init(new InstanceInfo() { Type = sTemplateType, InstanceId = (newplaylistinstance as IPlaylistInstance).ID, TemplateId = newplaylistinstance.TemplateID, ThemeId = objTemplateDetails.CurrentTheme, ViewPort = ViewPort, IsWired = IsWired });

//                         // S.No. : -	132
//                         if ((objPlayer.RecordCount != -9999) &&
//                             ((objPlayer as Player).EnableRequeryTimer == false))
//                         {
//                             (objPlayer as IAddinInfo).bIsSequencer = true;
//                         }                           
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     LogWriter.WriteLog(CConstants.LOGNAME, ex);
//                 }
//                 objPlayer.IsRenderMode = m_bRenderMode;
//                 objPlayer.SetRender(m_bPlaylistStatus);
//                 objPlayer.IsRunLog = WriteAsRunLog(sDataXml);
//                 InitializeNormalPlayer(objPlayer, sPreviousPlayerId);
//                 sNewPlayerLocation = InitializePlayer(objPlayer, sDataXml, newplaylistinstance, sPlayerLocation, objTemplateDetails, sPreviousPlayerId);

//                 objRendermode = GetRendermode(sNewPlayerLocation);

//                 objChannelShotBox = objPlayer as IChannelShotBox;
//                 if (objChannelShotBox != null)
//                 {
//                     objChannelShotBox.SetOutputChannel(newplaylistinstance.ActiveServer.OutputChannel);
//                     //S.No.: -			163
//                     //S.No.: -			161
//                     if (m_objLinkType == LINKTYPE.TCP)
//                         sEngineUrl = newplaylistinstance.ActiveServer.GetUrl(CConstants.Constants.TCP);
//                     if(string.IsNullOrEmpty(sEngineUrl))
//                         sEngineUrl = newplaylistinstance.ActiveServer.EnigneIP;
//                     //S.No.: -	147
//                     objChannelShotBox.SetEngineUrl(sEngineUrl);
//                 }


//                 //string info = "Playlist = {0} In CreateNormalPlayer calling prepare with new PlayerID = {1}";
//                 //Beesys.Wasp.WorkFlow.WaspPlaylistLogger.WriteLog(string.Format(info, new object[] { m_sPlaylistSlug, objPlayer.ID }));

//                 objPlayer.Prepare(newplaylistinstance.ActiveServer.EnigneIP, Convert.ToInt32(objPlayer.ZORDER), sDataXml, objRendermode);
//             }//end if (objPlayer != null)

//             return objPlayer;
//         }//end (if (newplaylistinstance != null))

#endregion
