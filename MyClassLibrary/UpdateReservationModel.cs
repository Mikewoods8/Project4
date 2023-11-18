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
    public class UpdateReservationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Restaurant { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
