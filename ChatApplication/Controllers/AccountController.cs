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
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext context;
        private readonly IFriendRepository friendRepository;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,AppDbContext context, 
            IFriendRepository friendRepository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
            this.friendRepository = friendRepository;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newuser = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.MobileNo
                };
                var result = await userManager.CreateAsync(newuser, model.PassWord);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newuser, isPersistent: false);
                    return RedirectToAction("login","account");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.PassWord, model.RememberMe, false);
                if (result.Succeeded)
                {
                    ApplicationUser user = context.AspNetUsers.SingleOrDefault<ApplicationUser>(u => u.UserName == model.UserName);
                    //return Ok(user);
                    user.LoggedIn = true;
                    context.SaveChanges();
                    return Redirect("/Home/home/" + user.Id);
                    //return RedirectToAction("Home","Home",user.Id);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            user.LoggedIn = false;
            context.SaveChanges();
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "Account");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateProfile(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            EditProfileViewModel viewmodel = new EditProfileViewModel
            {
                userID=id,
                UserName = user.UserName,
                Email = user.Email,
                MobileNo = user.PhoneNumber
            };
            return View(viewmodel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(EditProfileViewModel model)
        {
            //var id = userManager.GetUserId(User);

            ApplicationUser user = await userManager.FindByIdAsync(model.userID);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.MobileNo;

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return Redirect("/home/home/"+model.userID);
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ModelState.AddModelError("", "User Not Updated");
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}               
