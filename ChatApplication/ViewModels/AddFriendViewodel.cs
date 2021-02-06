using ChatApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.ViewModels
{
    public class AddFriendViewodel
    {
        [Required]
        public string fname { get; set; }
        [Required]
        public string mobileno { get; set; }
        public string UserId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
