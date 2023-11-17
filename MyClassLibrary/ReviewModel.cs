using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Utilities;

namespace MyClassLibrary
{
    public class ReviewModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Restaurant { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
        public int AtmosphereRating { get; set; }
        public int PriceRating { get; set; }
        public string Comments { get; set; }

        }

    }
