using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Info
    {
        private string name;
        private string information;

        public string Name
        {
            get { return name; }    
            set { name = value; }
        }
        public string Information
        {
            get { return information; }
            set { information = value; }
        }
    }
}
