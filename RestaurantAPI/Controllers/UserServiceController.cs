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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/UserService")]
    public class UserServiceController : Controller
    {

        // POST api/<UserService>
        [HttpPost()]
        [HttpPost("AddUser")]
        public Boolean AddUser([FromBody] UserModel user)
        {
            if (user != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CreateUser";

                objCommand.Parameters.AddWithValue("@UserId", user.UserId);
                objCommand.Parameters.AddWithValue("@Password", user.Password);
                objCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                objCommand.Parameters.AddWithValue("@LastName", user.LastName);
                objCommand.Parameters.AddWithValue("@Email", user.Email);
                objCommand.Parameters.AddWithValue("@Phone", user.Phone);
                objCommand.Parameters.AddWithValue("@Role", user.Role);

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