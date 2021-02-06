using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool LoggedIn { get; set; }
        public ICollection<friend> friends { get; set; }
        public ICollection<messege> messeges { get; set; }
    }
}
