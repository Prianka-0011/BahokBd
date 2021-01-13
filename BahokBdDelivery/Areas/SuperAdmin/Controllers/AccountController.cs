using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            //IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,

            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task< IActionResult> Login( MarchentLogin login)
        {
            if (ModelState.IsValid)
            {      
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var user = _context.Users.FirstOrDefault(c => c.UserName ==login .UserName || c.Email==login.UserName);
                    var role = _context.UserRoles.FirstOrDefault(c => c.UserId == user.Id);
                    var roleType = _context.Roles.FirstOrDefault(c => c.Id == role.RoleId);
                    if (roleType.Name == "Marchent")
                    {
                        return RedirectToAction("Index", "Admin", new { area = "SuperAdmin" });
                    }
                }
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
       public IActionResult  Index()
        {
            var role = _roleManager.Roles.ToList();
            return View(role);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(RoleName))
            {
                if (await _roleManager.RoleExistsAsync(RoleName))
                {
                    msg = "Role " + RoleName + " Already exists";
                }
                else
                {
                    IdentityRole Role = new IdentityRole(RoleName);
                    await _roleManager.CreateAsync(Role);
                    msg = "Role " + RoleName + " Successfully Created";

                }
                ViewBag.msg = msg;
                return View("CreateRole");
            }
            else
            {
                ViewBag.msg = "Please enter a valid role";
                return View("CreateRole");
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AssignRole()
        {
            var users = _userManager.Users;
            var roles = _roleManager.Roles;

            ViewBag.userlist = users;
            ViewBag.rolelist = roles;
            ViewBag.msg = TempData["msg"];

            return View();
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string appuser, string approle)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(appuser) && !string.IsNullOrEmpty(approle))
            {
                var user = await _userManager.FindByNameAsync(appuser);
                if (user != null)
                {
                    IdentityRole role = await _roleManager.FindByNameAsync(approle);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                        msg = role.Name + " has been assigned to user {" + user.UserName + "}.";
                    }
                    else
                    {
                        msg = "Role cannot be empty to assign to user.";
                    }
                }
                else
                {
                    msg = "Please select a User to assign Role.";
                }
            }
            else
            {
                msg = "Invalid User and/or Invalid Role.";
            }
            TempData["msg"] = msg;
            return RedirectToAction("AssignRole");

        }
        //[Authorize(Roles = "Admin")]
        public IActionResult RegisteredUserIndex(string returnUrl = null)
        {
            var user = _context.Users.ToList();
            return View(user);
        }
    }
}
