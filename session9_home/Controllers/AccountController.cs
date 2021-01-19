using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session9_home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session9_home.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login(string ReturnUrl)
        {
            LoginViewModel Model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(Model);
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel login)
        {
            var User = await this.userManager.FindByNameAsync(login.UserName);
            if (User !=null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = new Microsoft.AspNetCore.Identity.SignInResult();
                var PasswordCeck = await signInManager.PasswordSignInAsync(User, login.PassWord, false, false);
                if (PasswordCeck.Succeeded)
                {
                    var r = userManager.AddToRoleAsync(User, "Guest").Result;
                    return Redirect(login.ReturnUrl??"/");
                }
                ModelState.AddModelError("", "invalid username or password");
            }
            return View(login);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
