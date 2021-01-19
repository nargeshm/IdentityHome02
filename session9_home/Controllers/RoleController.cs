using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session9_home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session9_home.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole>roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            roleManager.Roles.ToList();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            AppUser appUser = new AppUser();
            var result = roleManager.CreateAsync(new IdentityRole(roleViewModel.Name)).Result;
            return View();
        }
    }
}
