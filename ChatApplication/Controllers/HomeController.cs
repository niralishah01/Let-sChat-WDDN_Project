using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApplication.Models;
using ChatApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ChatApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IFriendRepository friendRepository;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IFriendRepository friendRepository,
            AppDbContext context)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.friendRepository = friendRepository;
            this.context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Home(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            IEnumerable<friend> Friends = (IEnumerable<friend>)context.friends.Where<friend>(f => f.userID == id);
            IEnumerable<messege> messages = Enumerable.Empty<messege>();
            FriendListMessageList viewmodel = new FriendListMessageList();
            viewmodel.Fname = "";
            viewmodel.FId = -1;
            viewmodel.friends = Friends;
            viewmodel.messeges = messages;
            viewmodel.user = user;
            viewmodel.userID = user.Id;
            return View(viewmodel);
        }
        public IActionResult showMessages(string fid,string appuserid)
        {
            //var userid = userManager.GetUserId(this.User);
            int friendid = Int32.Parse(fid);
            var friend = context.friends.SingleOrDefault(f => f.friendID == friendid);
            var friendappuser = context.AspNetUsers.SingleOrDefault(u => u.PhoneNumber == friend.mobileno);
            var appuser = context.AspNetUsers.SingleOrDefault(u => u.Id == appuserid);
            IEnumerable<messege> messages = from m in context.messeges
                                            where (m.receiverId == friendappuser.Id && m.senderID == appuserid) ||
                 (m.senderID == friendappuser.Id && m.receiverId == appuserid)
                                            select m;
            IEnumerable<friend> Friends = from f in context.friends where f.userID == appuserid select f;

            FriendListMessageList viewmodel = new FriendListMessageList();
            viewmodel.Fname = friend.fname;
            viewmodel.FId = friend.friendID;
            viewmodel.friends = Friends;
            viewmodel.messeges = messages;
            viewmodel.userID = appuserid;
            viewmodel.user = appuser;
            return View("home", viewmodel);
        }
        [HttpPost]
        public async Task<ActionResult> createMessage(int friendid,string userid, string text)
        {
            //var userid = userManager.GetUserId(this.User);
            var friend = context.friends.SingleOrDefault(f => f.friendID == friendid);
            var friendappuser = context.AspNetUsers.SingleOrDefault(u => u.PhoneNumber == friend.mobileno);

            messege new_message = new messege
            {
                receiverId = friendappuser.Id,
                senderID = userid,
                text = text,
                datetime = DateTime.Now.ToString("M/dd/yyyy hh:MM tt"),
                filePath = null
            };
            await context.messeges.AddAsync(new_message);
            await context.SaveChangesAsync();
            return Redirect("/home/showMessages?fid=" + friendid + "&appuserid=" + userid);
            //return RedirectToAction("showMessages", "home", friend.friendID);
        }
        public IActionResult onlineFriends(string id)
        {
            IEnumerable<friend> Friends = from f in context.friends where f.userID == id select f;
            List<friend> onlinefriends = new List<friend>();
            foreach (var friend in Friends)
            {
                var friendappuser = context.AspNetUsers.SingleOrDefault(u => u.PhoneNumber == friend.mobileno);
                if (friendappuser.LoggedIn == true)
                {
                    onlinefriends.Add(friend);
                }
            }
            IEnumerable<messege> messages = Enumerable.Empty<messege>();
            var appuser = context.AspNetUsers.SingleOrDefault(u => u.Id == id);
            FriendListMessageList viewmodel = new FriendListMessageList();
            viewmodel.Fname = "";
            viewmodel.FId = -1;
            viewmodel.friends = onlinefriends;
            viewmodel.messeges = messages;
            viewmodel.userID = id;
            viewmodel.user = appuser;
            return View("home", viewmodel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
