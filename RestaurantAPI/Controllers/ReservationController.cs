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
    [Route("api/Reservation")]
    public class ReservationController : Controller
    {
        // POST api/<Reservation>
        [HttpPost()]
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
                objCommand.Parameters.AddWithValue("@Date", reservation.SelectedDate);
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
    }
}
