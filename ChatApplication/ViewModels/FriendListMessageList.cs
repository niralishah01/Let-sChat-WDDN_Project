using ChatApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.ViewModels
{
    public class FriendListMessageList
    {
        public IEnumerable<friend> friends { get; set; }
        public IEnumerable<messege> messeges { get; set; }
        public string Fname { get; set; }
        public int FId { get; set; }
        public ApplicationUser user { get; set; }
        public string userID { get; set; }
    }
}
