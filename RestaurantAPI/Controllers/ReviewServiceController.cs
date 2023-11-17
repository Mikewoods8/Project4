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
                objCommand.Parameters.AddWithValue("@UserID", review.UserId);
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

        [HttpGet("GetReviewByRestaurant")]
        public List<ReviewModel> GetReviewsByRestaurant(string restaurantName)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRestaurantReviews";

            objCommand.Parameters.AddWithValue("@RestaurantName", restaurantName);

            DataSet ds = objDB.GetDataSet(objCommand);

            List<ReviewModel> reviews = new List<ReviewModel>();
            ReviewModel review;

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                review = new ReviewModel();
                review.UserId = record["UserID"].ToString();
                review.Name = record["Name"].ToString();
                review.Restaurant = record["Restaurant"].ToString();
                review.FoodRating = Convert.ToInt32(record["FoodRating"]);
                review.ServiceRating = Convert.ToInt32(record["ServiceRating"]);
                review.AtmosphereRating = Convert.ToInt32(record["AtmosphereRating"]);
                review.PriceRating = Convert.ToInt32(record["PriceRating"]);
                review.Comments = record["Comments"].ToString();
                reviews.Add(review);
            }

            objDB.CloseConnection();

            return reviews;
        }

        [HttpGet("GetReviewById")]
        public List<ReviewModel> GetReviewsById(string userId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetReviewsByID";

            objCommand.Parameters.AddWithValue("@UserId", userId);

            DataSet ds = objDB.GetDataSet(objCommand);

            List<ReviewModel> reviews = new List<ReviewModel>();
            ReviewModel review;

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                review = new ReviewModel();
                review.UserId = record["UserID"].ToString();
                review.Name = record["Name"].ToString();
                review.Restaurant = record["Restaurant"].ToString();
                review.FoodRating = Convert.ToInt32(record["FoodRating"]);
                review.ServiceRating = Convert.ToInt32(record["ServiceRating"]);
                review.AtmosphereRating = Convert.ToInt32(record["AtmosphereRating"]);
                review.PriceRating = Convert.ToInt32(record["PriceRating"]);
                review.Comments = record["Comments"].ToString();
                reviews.Add(review);
            }

            objDB.CloseConnection();

            return reviews;
        }
    }

}
