using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BahokBdDelivery.Areas.Login.Controllers
{
    [Area("Login")]
    public class UserLoginController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public UserLoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            //IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,

            ILogger<UserLoginController> logger)
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
        public async Task<IActionResult> Login(MarchentLogin login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var user = _context.Users.FirstOrDefault(c => c.UserName == login.UserName || c.Email == login.UserName);
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
    }
}
