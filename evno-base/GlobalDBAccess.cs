using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC01.Code
{
    public class GolbalDBAccess : Controller
    {
        private string CS_Global = System.Configuration.ConfigurationManager.ConnectionStrings["DBCN"].ConnectionString;
        private const int r = 5; //run upto
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataSet dataset;

        private DataSet GlobalDBCall(string CS, string SP_QueryText, SqlParameter[] param, CommandType CT)
        {
            using (connection = new SqlConnection(CS))
            {
                using (command = new SqlCommand(SP_QueryText, connection))
                {
                    command.CommandType = CT;
                    if( param != null ) command.Parameters.AddRange(param);
                    adapter = new SqlDataAdapter(command);
                    dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset;
                }
            }
        }

        private DataSet GlobalDBCall(string SP_QueryText, SqlParameter[] param, CommandType CT = CommandType.StoredProcedure)
        {
            return GlobalDBCall(CS_Global, CT == CommandType.Text ? SP_QueryText : "PROC_" + SP_QueryText, param, CT);
        }
        DataSet a;
        private List<navAttributes> navConversion()
        {
            List<navAttributes> NA = new List<navAttributes>();
            a = navData();
            var b = a.Tables[0].AsEnumerable().ToList();
            for (int i = 0; i < b.Count; i++)
            {
                navAttributes na = new navAttributes();
                na.faName = b[i].Field<string>("faName");
                na.navId = b[i].Field<string>("navId");
                na.navName = b[i].Field<string>("navName");
                na.navLink = b[i].Field<string>("navLink");
                na.navChild_Attr = new List<navAttributes>();
                l(1, ref na);
                NA.Add(na);
            }
            return NA;
        }

        private void l(int c, ref navAttributes na_previous) //leveler, c = "constant -1, 2, 3, ..."
        {
            
            var b = t(c, na_previous.navId);
            for (int i = 0; i < b.Count; i++)
            {
                navAttributes na = new navAttributes();
                na.faName = b[i].Field<string>("faName");
                na.navId = b[i].Field<string>("navId");
                na.navName = b[i].Field<string>("navName");
                na.navLink = b[i].Field<string>("navLink");
                na.navChild_Attr = new List<navAttributes>();
                if (c < r) l(c + 1, ref na);
                na_previous.navChild_Attr.Add(na);
            }
        }

        private List<DataRow> t(int i, string navId) //table filter
        {
            List<DataRow> b = new List<DataRow>();
            if(a.Tables.Count > i) b = a.Tables[i].AsEnumerable().Where(e => e.Field<string>("parentId") == navId).ToList();
            return b;
        }

        private DataSet navData()
        {
            return GlobalDBCall("navData", null);
        }

        public JsonResult getNavs()
        {
            return Json(navConversion());
        }
    }

    class navAttributes
    {
        public string navId { get; set; }
        public string parentId { get; set; }
        public string faName { get; set; }
        public string navName { get; set; }
        public string navLink { get; set; }
        public string hasNavChild { get; set; }
        public List<navAttributes> navChild_Attr { get; set; }
    }
}