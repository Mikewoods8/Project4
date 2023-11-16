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

namespace RestaurantAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ReviewService")]
    public class ReviewServiceController : Controller
    {
        [HttpPost()]
        [HttpPost("AddReview")]
        public Boolean AddReview([FromBody] ReviewModel review)
        {
            if (review != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CreateReview";

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@UserID", review.UserID);
                objCommand.Parameters.AddWithValue("@Name", review.Name);
                objCommand.Parameters.AddWithValue("@Restaurant", review.Restaurant);
                objCommand.Parameters.AddWithValue("@FoodRating", review.FoodRating);
                objCommand.Parameters.AddWithValue("@ServiceRating", review.ServiceRating);
                objCommand.Parameters.AddWithValue("@AtmosphereRating", review.AtmosphereRating);
                objCommand.Parameters.AddWithValue("@PriceRating", review.PriceRating);
                objCommand.Parameters.AddWithValue("@Comments", review.Comments);

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

    }
}
