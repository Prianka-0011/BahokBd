using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using BahokBdDelivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BahokBdDelivery.Helper;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
           ILogger<AccountController> logger, IEmailSender emailSender, RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context, UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
         
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
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
        public IActionResult Register()
        {
            // ViewData["TypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName");
            return View(new MarchentProfileDetailVm());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MarchentProfileDetailVm vm)
        {
            if (ModelState.IsValid)
            {

                MarchentProfileDetail entity = new MarchentProfileDetail();
                var samePhone = _context.MarchentProfileDetail.Where(c => c.Phone == vm.Phone);
                var count = samePhone.Count();
                if (count > 0)
                {
                    ViewBag.error = "The phone number already exist";
                    return View(vm);
                }
                entity.Name = vm.Name;
                entity.Email = vm.Email;
                entity.Phone = vm.Phone;
                entity.BusinessName = vm.BusinessName;
                entity.BusinessLink = vm.BusinessLink;
                entity.BusinessAddress = vm.BusinessAddress;
                entity.AccountName = vm.AccountName;
                entity.AccountNumber = vm.AccountNumber;

                entity.LastIpAddress = vm.LastIpAddress;
                entity.CreateDateTime = DateTime.Now;
                entity.Password = "112345";
                string uniqueFileNameForImage = null;
                string uniqueFileNameForLogo = null;
                if (vm.Image != null)
                {

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileNameForImage = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileNameForImage);
                    await vm.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    entity.Image = "images/" + uniqueFileNameForImage;
                }
                if (vm.Logo != null)
                {

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "logos");
                    uniqueFileNameForLogo = Guid.NewGuid().ToString() + "_" + vm.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileNameForLogo);
                    await vm.Logo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    entity.Logo = "logos/" + uniqueFileNameForLogo;
                }
                entity.Id = Guid.NewGuid();
                _context.MarchentProfileDetail.Add(entity);
                await _context.SaveChangesAsync();
                var paymentDetail = new MarchentPaymentDetails();
                paymentDetail.MarchentId = entity.Id;
                paymentDetail.PaymentNameId = vm.PaymentBankingId;
                paymentDetail.PaymentTypeId = vm.PaymentTypeId;
                paymentDetail.BranchName = vm.BranchName;
                paymentDetail.RoutingName = vm.RoutingName;
                _context.MarchentPaymentDetails.Add(paymentDetail);
                await _context.SaveChangesAsync();
                return Ok();


            }
            return Ok();
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
