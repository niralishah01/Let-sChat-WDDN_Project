using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class messege
    {
        public int messegeID { get; set; }
        public string text { get; set; }
        public string datetime { get; set; }
        public string filePath { get; set; }
        public string receiverId { get; set; }
        public string senderID { get; set; }
        public ApplicationUser user { get; set; }
    }
}
