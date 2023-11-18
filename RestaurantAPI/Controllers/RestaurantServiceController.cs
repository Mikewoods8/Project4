using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using MyClassLibrary;
using Utilities;
using System.Xml.Linq;

namespace RestaurantAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/RestaurantService")]
    public class RestaurantServiceController : Controller
    {
        // POST api/<UserService>
        [HttpPost("AddRestaurant")]
        public Boolean AddRestaurant([FromBody] RestaurantModel restaurant)
        {
            if (restaurant != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CreateRestaurant";

                objCommand.Parameters.AddWithValue("@Name", restaurant.Name);
                objCommand.Parameters.AddWithValue("@Category", restaurant.Category);
                objCommand.Parameters.AddWithValue("@RepresentativeID", restaurant.RepresentativeID);

                int retVal = objDB.DoUpdateUsingCmdObj(objCommand);
                if (retVal > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("GetRestaurant")]
        public List<RestaurantModel> GetRestaurant()
        {
            DBConnect objDB = new DBConnect();
            DataSet ds = objDB.GetDataSet("SELECT * FROM RESTAURANTS");
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            RestaurantModel restaurant;

            foreach(DataRow record in ds.Tables[0].Rows)
            {
                restaurant = new RestaurantModel();
                restaurant.Name = record["Name"].ToString();
                restaurant.Category = record["Category"].ToString();
                restaurant.RepresentativeID = record["RepresentativeID"].ToString();
                restaurants.Add(restaurant);
            }
            return restaurants;
        }

        [HttpGet("GetRestaurantById")]
        public List<UpdateRestaurantModel> GetRestaurantById(string userId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRestaurantsByID";

            objCommand.Parameters.AddWithValue("@UserId", userId);

            DataSet ds = objDB.GetDataSet(objCommand);

            List<UpdateRestaurantModel> restaurants = new List<UpdateRestaurantModel>();
            UpdateRestaurantModel restaurant;

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                restaurant = new UpdateRestaurantModel();
                restaurant.Id = Convert.ToInt32(record["Id"]);
                restaurant.Name = record["Name"].ToString();
                restaurant.Category = record["Category"].ToString();
                restaurant.RepresentativeId = record["RepresentativeID"].ToString();
                restaurants.Add(restaurant);
            }

            objDB.CloseConnection();

            return restaurants;
        }

        [HttpGet("GetRestaurantByCategory")]
        public List<RestaurantModel> GetRestaurantByCategory(string category)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRestaurantByCategory";

            objCommand.Parameters.AddWithValue("@Category", category);

            DataSet ds = objDB.GetDataSet(objCommand);

            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            RestaurantModel restaurant;

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                restaurant = new RestaurantModel();
                restaurant.Name = record["Name"].ToString();
                restaurant.Category = record["Category"].ToString();
                restaurants.Add(restaurant);
            }

            objDB.CloseConnection();

            return restaurants;
        }

        [HttpDelete("DeleteRestaurant")]
        public Boolean DeleteReview(int restaurantId)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "DeleteRestaurant";

                objCommand.Parameters.AddWithValue("@RestaurantId", restaurantId);

                int retVal = objDB.DoUpdateUsingCmdObj(objCommand);

                if (retVal > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
