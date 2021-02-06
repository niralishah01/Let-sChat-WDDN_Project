using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class friend
    {
        public int friendID { get; set; }
        public string fname { get; set; }
        public string mobileno { get; set; }
        public string userID { get; set; }
        public ApplicationUser user { get; set; }

    }
}
