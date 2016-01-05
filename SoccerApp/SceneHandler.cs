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
        ShotBox _objPlayer1;
        LinkManager _objLinkManager;
        const string m_surl = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        const string m_sport = "50011";
        const string m_IP = "192.168.1.192";       
        string m_serverip = string.Format(m_surl, ConfigurationManager.AppSettings["stingserverip"], m_sport);
        #endregion
        public Link AppLink
        {
            get;
            set;
        }

        

        public void Initialize()
        {

            isInitialized = false;

            //if (ConfigurationManager.AppSettings["w3dscene"] != null)
            {
               // if (File.Exists(ConfigurationManager.AppSettings["w3dscene"]))
                {
                    if (ConfigurationManager.AppSettings["stingserverip"] != null)
                    {
                        string sLinkID = string.Empty;
                        _objLinkManager = new LinkManager();
                        AppLink = _objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                        //AppLink.OnEngineConnected += new EventHandler<EngineArgs>(_objLink_OnEngineConnected);
                        //AppLink.Connect(m_surl1);
                        //_objLinkManager.OnEngineDisConnected += _objLinkManager_OnEngineDisConnected;
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


        public void LoadScene()
        {
            string stemplateID = string.Empty;
            string sXml = string.Empty;
            string sDataXml = "<dgn><data></data></dgn>";
            string sShotBoxID = null;
            bool isTicker;
            string filetype = string.Empty;
            //sXml = Util.getSGFromWSL(ConfigurationManager.AppSettings["w3dscene"]);
            sXml = Util.getSGFromWSL(@"d:\sphere.wsp");
            filetype = Path.GetExtension(ConfigurationManager.AppSettings["w3dscene"]).Split(new string[] { "." }, StringSplitOptions.None)[1];
            if (!string.IsNullOrEmpty(sXml))
            {

                _objPlayer1 = AppLink.GetShotBox(sXml, out sShotBoxID, out isTicker) as ShotBox;
                if (!Equals(_objPlayer1, null))
                {
                    // _objPlayer1.SetEngineUrl(ConfigurationManager.AppSettings["stingserver"]);
                    _objPlayer1.SetEngineUrl(m_serverip);

                    //InstanceInfo o = new InstanceInfo() { Type = filetype, InstanceId = string.Empty, TemplateId = ConfigurationManager.AppSettings["w3dscene"], ThemeId = "default" };

                    if (_objPlayer1 is IAddinInfo)
                        (_objPlayer1 as IAddinInfo).Init(new InstanceInfo() { Type = "wsp", InstanceId = string.Empty, TemplateId = "57abeb14-4e41-4247-9808-18b73ae52f8f", ThemeId = "default" });

                    //if (m_objPlayer == null)
                    //{
                    //    //m_objPlayer = new Beesys.Wasp.Workflow.Player() ;
                    //    m_objPlayer.SetLink(_objLink, sXml);
                    //    _objPlayer1.Prepare(m_serverip, 0, RENDERMODE.PROGRAM);
                    //}


                    _objPlayer1.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                    _objPlayer1.OnShotBoxControllerStatus += _objPlayer1_OnShotBoxControllerStatus;


                    //_objPlayer1.Prepare(ConfigurationManager.AppSettings["stingserverip"], 0, RENDERMODE.PROGRAM);
                    _objPlayer1.Prepare(m_serverip, 0, RENDERMODE.PROGRAM);

                }//end (if)
            }

        }


        void _objPlayer1_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                isInitialized = true;
                isSceneLoaded = true;
            }
            else if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PLAYCOMPLETE)
            {
                _objPlayer1.DeleteSg();
            }
        }
        void _objPlayer1_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                isInitialized = true;
                isSceneLoaded = true;

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
                LoadScene();
            }
        }



        #endregion



        void objplayercontainer_OnEngineConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == ConfigurationManager.AppSettings["stingserver"])
            {
                isInitialized = true;
                //LoadScene();
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
        public void TimerAction(string actiontype, string counter, IPlayer player)
        {
           
            try
            {
                    TagData tg = new TagData();                   
                    switch (actiontype.ToLower())
                    {
                        case "start":                        
                            player.PlayActionSet(ConfigurationManager.AppSettings["counterstartaction"]);
                            break;
                        case "stop":
                            player.PlayActionSet(ConfigurationManager.AppSettings["counterstopaction"]);
                            break;
                        case "update":
                            tg.UserTags = new string[] { ConfigurationManager.AppSettings["counterusertag"] };
                            tg.Values = new string[] { counter };

                            player.UpdateSceneGraph(tg);

                            break;
                        case "updateextra":
                            tg.UserTags = new string[] { ConfigurationManager.AppSettings["extratimeusertag"] };
                            tg.Values = new string[] { counter };                            
                            player.UpdateSceneGraph(tg);
                            break;
                        case "extrain":
                            player.PlayActionSet(ConfigurationManager.AppSettings["extratimein"]);
                            break;
                        case "extraout":
                            player.PlayActionSet(ConfigurationManager.AppSettings["extratimeout"]);
                            break;
                        case "extrastart":
                            player.PlayActionSet(ConfigurationManager.AppSettings["startextratime"]);
                            break;
                        case "extrastop":                           
                            player.PlayActionSet(ConfigurationManager.AppSettings["stopextratime"]);
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
