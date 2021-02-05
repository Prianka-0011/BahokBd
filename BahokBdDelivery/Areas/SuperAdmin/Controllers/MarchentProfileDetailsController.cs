using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using Microsoft.AspNetCore.Hosting;
using BahokBdDelivery.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using static BahokBdDelivery.Helper;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class MarchentProfileDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<MarchentProfileDetailsController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MarchentProfileDetailsController(ILogger<MarchentProfileDetailsController> logger, IEmailSender emailSender, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        // GET: SuperAdmin/MarchentProfileDetail
        public async Task<IActionResult> Index()
        {
            string msg = Convert.ToString(TempData["msg"]);
            ViewBag.approve = msg;
            return View(await _context.MarchentProfileDetail.ToListAsync());
        }
  
        public async Task< IActionResult> Approve(Guid?id)
        {
            var marchent = _context.MarchentProfileDetail.FirstOrDefault(c => c.Id == id);
            var user = new ApplicationUser { UserName = marchent.Phone, Email = marchent.Email};
            try
            {
                var result = await _userManager.CreateAsync(user, marchent.Password);
                var uName = await _userManager.FindByNameAsync(marchent.Phone);
                if (result.Succeeded)
                {
                    // _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(marchent.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                        var result1 = await _userManager.ConfirmEmailAsync(user, code);
                    }
                    if (uName != null)
                    {
                        var roleName = "Marchent";
                        var mRole = await _roleManager.FindByNameAsync(roleName);
                        if (mRole != null)
                        {
                            await _userManager.AddToRoleAsync(uName, mRole.Name);
                        }
                        marchent.Status = true;
                        await _context.SaveChangesAsync();
                    }
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }

            }
            catch(Exception ex)
            {

            }
     

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllMarchent", _context.MarchentProfileDetail.ToList()) });
        }
        
        //// GET: SuperAdmin/MarchentProfileDetail/Create
        [NoDirectAccess]
        public IActionResult Create()
        {
           // ViewData["TypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName");
            return View(new MarchentProfileDetailVm());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarchentProfileDetailVm vm)
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
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllMarchent", _context.MarchentProfileDetail.ToList()) });

                
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", vm) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MarchentProfileDetailVm vm = new MarchentProfileDetailVm();
            var entity = await _context.MarchentProfileDetail.FindAsync(id);
            if (vm == null)
            {
                return NotFound();
            }
            vm.Name = entity.Name;
            vm.Email = entity.Email;
            vm.Phone = entity.Phone;
            vm.BusinessName = entity.BusinessName;
            vm.BusinessLink = entity.BusinessLink;
            vm.BusinessAddress = entity.BusinessAddress;
            vm.AccountName = entity.AccountName;
            vm.AccountNumber = entity.AccountNumber;
            vm.LastIpAddress = entity.LastIpAddress;
            vm.DateTime = entity.CreateDateTime;
            vm.DisplayImage = entity.Image;
            vm.DisplayLogo = entity.Logo;
            vm.Status = entity.Status;
            var marchant = _context.MarchentPaymentDetails.FirstOrDefault(c => c.MarchentId == entity.Id);
            var odlPaymentType = _context.PaymentBankingType.FirstOrDefault(c => c.Id == marchant.PaymentTypeId);
            vm.OdlPaymentTypeName = odlPaymentType.BankingMethodName;
            var odlBank = _context.PaymentBankingOrganization.FirstOrDefault(c => c.Id == marchant.PaymentNameId);
            vm.OdlBankName = odlBank.OrganizationName;
           // var odlBranch = _context.BankBranch.FirstOrDefault(c => c.Id == marchant.BranchId);
            vm.OdlBranchName = marchant.BranchName;
            vm.OdlRouting = marchant.RoutingName;
            return View(vm);
        }

        // POST: SuperAdmin/MarchentProfileDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MarchentProfileDetailVm vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = _context.MarchentProfileDetail.Find(id);
                entity.Name = vm.Name;
                entity.Email = vm.Email;
                entity.Phone = vm.Phone;
                entity.BusinessName = vm.BusinessName;
                entity.BusinessLink = vm.BusinessLink;
                entity.BusinessAddress = vm.BusinessAddress;
                entity.AccountName = vm.AccountName;
                entity.AccountNumber = vm.AccountNumber;
                entity.LastIpAddress = vm.LastIpAddress;
                entity.CreateDateTime = vm.DateTime;
                entity.Status = vm.Status;
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

                _context.MarchentProfileDetail.Update(entity);
                await _context.SaveChangesAsync();
                if (vm.PaymentTypeId != Guid.Empty && vm.PaymentBankingId != null && vm.BranchName != null)
                {
                    var paymentDetail = _context.MarchentPaymentDetails.FirstOrDefault(c => c.MarchentId == entity.Id);
                    if (paymentDetail != null)
                    {

                        paymentDetail.MarchentId = entity.Id;
                        paymentDetail.PaymentNameId = vm.PaymentBankingId;
                        paymentDetail.PaymentTypeId = vm.PaymentTypeId;
                        paymentDetail.BranchName = vm.BranchName;
                        paymentDetail.RoutingName = vm.RoutingName;
                        _context.MarchentPaymentDetails.Update(paymentDetail);
                    }
                    else
                    {
                        var paymentDetail1 = new MarchentPaymentDetails();
                        paymentDetail1.MarchentId = entity.Id;
                        paymentDetail1.PaymentNameId = vm.PaymentBankingId;
                        paymentDetail1.PaymentTypeId = vm.PaymentTypeId;
                        paymentDetail1.BranchName = vm.BranchName;
                        paymentDetail1.RoutingName = vm.RoutingName;
                        _context.MarchentPaymentDetails.Add(paymentDetail1);
                    }
                }
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllMarchent", _context.MarchentProfileDetail.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", vm) });
        }

        // GET: SuperAdmin/MarchentProfileDetail/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MarchentProfileDetail = await _context.MarchentProfileDetail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (MarchentProfileDetail == null)
            {
                return NotFound();
            }

            return View(MarchentProfileDetail);
        }

        // POST: SuperAdmin/MarchentProfileDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var MarchentProfileDetail = await _context.MarchentProfileDetail.FindAsync(id);
            _context.MarchentProfileDetail.Remove(MarchentProfileDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/MarchentProfileDetail/GetExistBankingType")]
        public IActionResult GetExistBankingType(Guid id)
        {
            var dvs = _context.PaymentBankingType.FirstOrDefault(x => x.Id == id);
            return Json(dvs);
        }

        [HttpGet("/MarchentProfileDetail/GetExistBankingName")]
        public IActionResult GetExistBankingName(Guid id)
        {
            var org = _context.PaymentBankingOrganization.FirstOrDefault(x => x.Id == id);
            return Json(org);
        }
        [HttpGet("/MarchentProfileDetail/GetBankingType")]
        public IActionResult GetBankingType()
        {
            var tpe = _context.PaymentBankingType;
            return Json(tpe.ToList());
        }
        [HttpGet("/MarchentProfileDetail/GetOrganizationName")]
        public IActionResult GetOrganizationName(Guid id)
        {
            var org = _context.PaymentBankingOrganization.Where(o => o.PaymentBankingTypeId == id);
            return Json(org.ToList());
        }
        private bool MarchentProfileDetailExists(Guid id)
        {
            return _context.MarchentProfileDetail.Any(e => e.Id == id);
        }
    }
}