using BeeSys.Wasp.Communicator;
using BeeSys.Wasp.KernelController;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UDTProvider
{
    public class UDTProvider
    {
        private string _CommonPath;
        CRemoteHelper _objRemoteHelper;
        ConnectionStatus info;
        CUDTManagerHelper _mObjUdtHandler;
        public CUdtArgs CurrentUDT { get; set; }

        public UDTProvider()
        {
           _CommonPath =  Environment.GetEnvironmentVariable("Wasp3.5");
        }

        public bool InitializeConnection()
        {
          
           // string url=string.Empty;
            if (string.IsNullOrEmpty(_CommonPath))
                return false;
            var configfile= Path.Combine(_CommonPath, "CommonConfig.config");

            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
                      where lv1.Attribute("key").Value == "LOCALMANAGERURL"
            select lv1.Attribute("value").Value ;
            _objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
             info = _objRemoteHelper.Connect();
            _mObjUdtHandler = new CUDTManagerHelper(CRemoteHelper.GetDisconnectedUrl("UDTManager"));
            
             return true;
        }
        public void InitializeUDT(string UdtName)
        {
            CurrentUDT = _mObjUdtHandler.GetUdtByName(UdtName);
        }

    }
}
