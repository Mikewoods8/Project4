using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RestaurantSoapService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SessionManagement : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void SetUserID(string userID)
        {
            Session["UserID"] = userID;
        }

        [WebMethod(EnableSession = true)]
        public string GetUserID()
        {
            if (Session["UserID"] != null)
            {
                return Session["UserID"].ToString();
            }
            return null;
        }

        [WebMethod(EnableSession = true)]
        public void SetName(string name)
        {
            Session["Name"] = name;
        }

        [WebMethod(EnableSession = true)]
        public string GetName()
        {
            if (Session["Name"] != null)
            {
                return Session["Name"].ToString();
            }
            return null;
        }

        [WebMethod(EnableSession = true)]
        public void SetRestaurantName(string restaurantName)
        {
            Session["RestaurantName"] = restaurantName;
        }

        [WebMethod(EnableSession = true)]
        public string GetRestaurantName()
        {
            if (Session["RestaurantName"] != null)
            {
                return Session["RestaurantName"].ToString();
            }
            return null;
        }
    }
}
