using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoccerApp
{
   public class SceneHandler
    {
       public bool isInitialized { get; set; }
       public bool isSceneLoaded { get; set; }
       public bool Initialize()
       {
           isInitialized = false;
           if (ConfigurationManager.AppSettings["w3dscene"] != null)
           {
               if(File.Exists(ConfigurationManager.AppSettings["w3dscene"]))
               {
                   isInitialized = true;

               }
           }
           
           return isInitialized;
       }
       public void LoadScene()
       {
           isSceneLoaded = false;
           if(isInitialized)
           {

           }

       }
       public void UpdateScore(string Team,string score)
       {
           if(isInitialized && isSceneLoaded)
           {

           }
       }
       public void TimerAction(string actiontype,string counter)
       {
           if (isInitialized && isSceneLoaded)
           {

           }
       }
    }
}
