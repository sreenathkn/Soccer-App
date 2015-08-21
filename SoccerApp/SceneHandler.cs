using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Beesys.Wasp.Workflow;


namespace SoccerApp
{
   public class SceneHandler
    {
       public bool isInitialized { get; set; }
       public bool isSceneLoaded { get; set; }
       ShotBox _objPlayer1;
       LinkManager _objLinkManager;
       Link _objLink;
       public void Initialize()
       {
           isInitialized = false;
           if (ConfigurationManager.AppSettings["w3dscene"] != null)
           {
               if (File.Exists(ConfigurationManager.AppSettings["w3dscene"]))
               {
                   if (ConfigurationManager.AppSettings["stingserver"] != null)
                   {
                        string sLinkID = string.Empty;
                       _objLinkManager = new LinkManager();
                       _objLink = _objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                       _objLink.OnEngineConnected += _objLink_OnEngineConnected;
                       _objLink.Connect(ConfigurationManager.AppSettings["stingserver"]);
                       _objLinkManager.OnEngineDisConnected += _objLinkManager_OnEngineDisConnected;
                   }
               }

           }
       }
       public void LoadScene()
       {
           string sXml = string.Empty;
           string sShotBoxID = null;
           bool isTicker;
           string filetype = string.Empty;
           sXml = Util.getSGFromWSL(ConfigurationManager.AppSettings["w3dscene"]);
           filetype = Path.GetExtension(ConfigurationManager.AppSettings["w3dscene"]).Split(new string[] { "." }, StringSplitOptions.None)[1];
           if (!string.IsNullOrEmpty(sXml))
           {
               _objPlayer1 = _objLink.GetShotBox(sXml, out sShotBoxID, out isTicker) as ShotBox;
               if (!Equals(_objPlayer1, null))
               {
                   _objPlayer1.SetEngineUrl(ConfigurationManager.AppSettings["stingserver"]);
                   if (_objPlayer1 is IAddinInfo)
                       (_objPlayer1 as IAddinInfo).Init(new InstanceInfo() { Type = filetype, InstanceId = string.Empty, TemplateId = ConfigurationManager.AppSettings["w3dscene"], ThemeId = "default" });
                   _objPlayer1.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                   _objPlayer1.Prepare(ConfigurationManager.AppSettings["stingserver"], 0, RENDERMODE.PROGRAM);

               }//end (if)
           }
           
       }
       void _objPlayer1_OnShotBoxStatus(object sender, SHOTBOXARGS e)
       {
           if(e.SHOTBOXRESPONSE== SHOTBOXMSG.PREPARED)
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
           if(e.ENGINEIP == ConfigurationManager.AppSettings["stingserver"])
           {
               isInitialized = true;
               LoadScene();
           }
       }

       public void UpdateScore(string Team,string score)
       {
           if(isInitialized && isSceneLoaded)
           {
               TagData tg=new TagData();
               if(Team.ToLower()=="home")
               {
                   tg.UserTags = new string[] { ConfigurationManager.AppSettings["homescoreusertag"] };
                   tg.Values=new string[]{score};
                   _objPlayer1.UpdateSceneGraph(tg);
               }
               else
               {
                   tg.UserTags = new string[] { ConfigurationManager.AppSettings["awayscoreusertag"] };
                   tg.Values = new string[] { score };
                   _objPlayer1.UpdateSceneGraph(tg);
               }
           }
       }
       public void TimerAction(string actiontype,string counter)
       {
           if (isInitialized && isSceneLoaded)
           {
               TagData tg = new TagData();
               switch (actiontype.ToLower())
               {
                   case "start":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["counterstartaction"]);
                       break;
                   case "stop":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["counterstopaction"]);
                       break;
                   case "update":
                       tg.UserTags = new string[] { ConfigurationManager.AppSettings["counterusertag"] };
                       tg.Values = new string[] { counter };
                       _objPlayer1.UpdateSceneGraph(tg);
                       break;
                   case "updateextra":
                       tg.UserTags = new string[] { ConfigurationManager.AppSettings["extratimeusertag"] };
                       tg.Values = new string[] { counter };
                       _objPlayer1.UpdateSceneGraph(tg);
                       break;
                   case "extrain":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["extratimein"]);
                       break;
                   case "extraout":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["extratimeout"]);
                       break;
                   case "extrastart":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["startextratime"]);
                       break;
                   case "extrastop":
                       _objPlayer1.PlayActionSet(ConfigurationManager.AppSettings["stopextratime"]);
                       break;
                   default:
                       break;
               }
               
           }
       }
    }
}
