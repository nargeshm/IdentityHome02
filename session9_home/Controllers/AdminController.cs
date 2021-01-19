using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session9_home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session9_home.Controllers
{
  
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPasswordHasher<AppUser> hasher;

        public AdminController(UserManager<AppUser>userManager,IPasswordHasher<AppUser>hasher)
        {
            this.userManager = userManager;
            this.hasher = hasher;
        }
        [Authorize(Roles = "Admin ")]
        public IActionResult Index()
        {
            var User = userManager.Users.ToList();
            List<UserViewModel> Result = new List<UserViewModel>();
            foreach (var item in User)
            {
                Result.Add(new UserViewModel()
                {UserName=item.UserName,
                    PassWord=item.PasswordHash,
                    Email=item.Email
                }
                );
            }
            return View(Result);
        }
        public IActionResult Create ()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create (UserViewModel Model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = Model.UserName,
                    Email = Model.Email
                };
                IdentityResult Result = userManager.CreateAsync(appUser,Model.PassWord).Result;
                if (Result.Succeeded)
                {
                    return Redirect("Index");
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View(Model);
        }
        public IActionResult Delete(string UserName)
        {
            AppUser User = userManager.FindByNameAsync(UserName).Result;
            if (User!=null)
            {
                var result = userManager.DeleteAsync(User).Result;
            }

           return Redirect("index");
        }
        public IActionResult Edit(string UserName)
        {
            AppUser User = userManager.FindByNameAsync(UserName).Result;
            UserViewModel model = new UserViewModel()
            {
                Email = User.Email,
                UserName = User.UserName,
                PassWord = User.PasswordHash
            }; 
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel Model)
        {
            AppUser User = userManager.FindByNameAsync(Model.UserName).Result;
            User.PasswordHash = hasher.HashPassword(User, Model.PassWord);
            User.UserName = Model.UserName;
            User.Email = Model.Email;
            var res = userManager.UpdateAsync(User).Result;
            if (res.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in res.Errors)
            {
                ModelState.AddModelError("",item.Description);
                  
            }
            return View(Model);
        }

    }
}
