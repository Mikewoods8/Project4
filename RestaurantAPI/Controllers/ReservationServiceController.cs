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
    [Route("api/ReservationService")]
    public class ReservationServiceController : Controller
    {
        // POST api/<ReservationService>
        [HttpPost("AddReservation")]
        public Boolean AddReservation([FromBody] ReservationModel reservation)
        {
            if (reservation != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CreateReservation";

                objCommand.Parameters.AddWithValue("@Name", reservation.Name);
                objCommand.Parameters.AddWithValue("@Restaurant", reservation.Restaurant);
                objCommand.Parameters.AddWithValue("@Date", reservation.Date);
                objCommand.Parameters.AddWithValue("@Time", reservation.Time);

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

        [HttpGet("GetReservationByRestaurant")]
        public List<ReservationModel> GetReservationByRestaurant(string selectedName)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRestaurantsByReservation";

            objCommand.Parameters.AddWithValue("@RestaurantName", selectedName);

            DataSet ds = objDB.GetDataSet(objCommand);

            List<ReservationModel> reservations = new List<ReservationModel>();
            ReservationModel reservation;

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                reservation = new ReservationModel();
                reservation.Name = record["Name"].ToString();
                reservation.Restaurant = record["Restaurant"].ToString();
                reservation.Date = record["Date"].ToString();
                reservation.Time = record["Time"].ToString();
                reservations.Add(reservation);
            }

            objDB.CloseConnection();

            return reservations;
        }

    }
}
