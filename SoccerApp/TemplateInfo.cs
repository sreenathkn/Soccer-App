using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beesys.Wasp.Workflow
{
    public struct TemplateInfo
    {
        public Dictionary<string, SGFileInfo> TemplateDetails
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public Type TemplatePlayerInfo
        {
            get;
            set;
        }//end (TemplateInstanceInfo)

        public Type TemplateDataInfo
        {
            get;
            set;
        }//end (TemplateInstanceInfo)

        public bool IsUnifiedForm
        {
            get;
            set;
        }//end (IsUnifiedForm)       

        public bool IsTicker
        {
            get;
            set;
        }//end (IsTicker)

        public string SgXml
        {
            get;
            set;
        }

        public string MetaDataXml
        {
            get;
            set;
        }
        //S.No.: -			07
        public string TemplateSlug
        {
            get;
            set;
        }
    }
}
