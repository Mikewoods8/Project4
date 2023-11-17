using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Utilities;
using MyClassLibrary;

namespace RestaurantSoapService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class GetRole : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetUserRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return "Guest";
            }
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetRole";
            cmd.Parameters.AddWithValue("@UserID", userID);
            DataSet roleDataSet = db.GetDataSetUsingCmdObj(cmd);
            if (roleDataSet.Tables.Count > 0 && roleDataSet.Tables[0].Rows.Count > 0)
            {
                return roleDataSet.Tables[0].Rows[0]["Role"].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
