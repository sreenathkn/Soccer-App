using BeeSys.Wasp.KernelController;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace ExtensionMethods
{
    public static class UDTExtensions
    {
        public static DataSet ToDataSet(this CUdtArgs udt)
        {
            DataSet CurrentDataSet = new DataSet();
            if (!string.IsNullOrEmpty(udt.FORMAT) && !string.IsNullOrEmpty(udt.DATA))
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(udt.FORMAT)))
                {
                    CurrentDataSet.ReadXmlSchema(reader);
                }
                using (XmlReader reader = XmlReader.Create(new StringReader(udt.DATA)))
                {
                        CurrentDataSet.ReadXml(reader);
                }
                XDocument doc = XDocument.Parse(udt.TableFormat);
               /* foreach (DataTable item in CurrentDataSet.Tables)
                {
                    item.TableName = GetTableNamebyID(item.TableName, doc);
                    
                }*/
                
                return CurrentDataSet;
            }
            return null;
        }
        private static string GetTableNamebyID(string strid, XDocument m_objXDocTables)
        {
            string sTableNme = string.Empty;
            try
            {
                sTableNme = (from tbl in m_objXDocTables.XPathSelectElements("//table")
                             where string.Compare(strid, tbl.Attribute("tid").Value, StringComparison.OrdinalIgnoreCase) == 0
                             select tbl.Attribute("name").Value).FirstOrDefault();
                return sTableNme;
            }
            finally
            {

            }
        }​

        
    }
    /*public static class Tables
    {
        static CUdtArgs CurrentUDT;
        internal static void setUDT(CUdtArgs uargs)
        {
            CurrentUDT = uargs;
        }
        public DataTable this[int number]
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
        public DataTable this[string tableName]
        {
            get
            {

            }
        }

        private string GetinternalTableName(string stablenme)
        {
            XElement objXeTable = null;
            XDocument m_objXDocTables = XDocument.Parse(CurrentUDT.TableFormat);

            try
            {
                objXeTable = (from xeTable in m_objXDocTables.XPathSelectElements(CUdtConstants.Xml.TABLES)
                              where string.Compare(xeTable.Attribute(CUdtConstants.Xml.TABLE_NAME).Value, stablenme, StringComparison.OrdinalIgnoreCase) == 0 ||
                              string.Compare(xeTable.Attribute(CUdtConstants.Xml.TABLE_ID).Value, stablenme, StringComparison.OrdinalIgnoreCase) == 0
                              select xeTable).FirstOrDefault();
                return objXeTable.Attribute(CUdtConstants.Xml.TABLE_ID).Value();
            }
            finally
            {
                objXeTable = null;
            }
        }
    }*/
   static class CUdtConstants
    {

        internal class Xml
        {
            internal const string TABLES = "//table";
            internal const string TABLE_ID = "tid";
            internal const string TABLE_NAME = "name";
        }
    }
}
