using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Models;
using ChatApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{ 
    [Authorize]
    public class FriendController : Controller
    {
        //UserManager<ApplicationUser> userManager;
        //private readonly SignInManager<ApplicationUser> signInManager
        private readonly IFriendRepository friendRepository;
        private readonly AppDbContext context;
        //private readonly List<ApplicationUser> user = new List<ApplicationUser>();
        //public IReadOnlyCollection<ApplicationUser> Users = user;

        public FriendController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IFriendRepository friendRepository,AppDbContext context)
        {
            //this.userManager = userManager;
            //this.signInManager = signInManager;
            this.friendRepository = friendRepository;
            this.context = context;
        }
        [HttpGet]
        public ViewResult AddFriend(string id)
        {
            ApplicationUser loggedinuser = context.AspNetUsers.SingleOrDefault<ApplicationUser>(u => u.Id == id);
            AddFriendViewodel friendmodel = new AddFriendViewodel
            {
                user = loggedinuser,
                UserId=id
            };
            return View(friendmodel);
        }
        [HttpPost]
        public IActionResult AddFriend(AddFriendViewodel friendmodel)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser existfriend = context.AspNetUsers.SingleOrDefault<ApplicationUser>(u => u.PhoneNumber == friendmodel.mobileno);
                //Console.WriteLine(existfriend);
                string msg = "";
                if (existfriend == null)
                {
                    ApplicationUser appuser = context.AspNetUsers.SingleOrDefault<ApplicationUser>(u => u.Id == friendmodel.UserId);
                    friendmodel.user = appuser;
                    msg = "Friend does not register on this website, so invite him using";
                    ViewData["err"] = msg;
                    return View(friendmodel);
                }
                else
                {
                    friend f = new friend
                    {
                        fname = friendmodel.fname,
                        mobileno = friendmodel.mobileno,
                        userID = friendmodel.UserId
                    };
                    friendRepository.Add(f);
                    return Redirect("/home/home/"+friendmodel.UserId);
                }
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
           friend f = friendRepository.GetFriend(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }
        //[Route("[action]")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            friend f = friendRepository.GetFriend(id);
            friendRepository.Delete(f.friendID);
            return RedirectToAction("index","account");
        }
    }
}
