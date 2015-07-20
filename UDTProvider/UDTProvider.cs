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

        public UDTProvider()
        {
           _CommonPath =  Environment.GetEnvironmentVariable("Wasp3.5");
        }

        public bool Initialize()
        {
            bool Result = false;
           // string url=string.Empty;
            if (string.IsNullOrEmpty(_CommonPath))
                return false;
            var configfile= Path.Combine(_CommonPath, "CommonConfig.config");

            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
            where lv1.Attribute("key").Value=="REMOTEMANAGERURL"
            select lv1.Attribute("value").Value ;
    


            _objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
             info = _objRemoteHelper.Connect();
            _mObjUdtHandler = new CUDTManagerHelper(CRemoteHelper.GetDisconnectedUrl("UDTManager"));
             var udt =_mObjUdtHandler.GetUdtByName("Soccer");
             return Result;
        }
    }
}
