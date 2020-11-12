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

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class MarchentProfileDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MarchentProfileDetailsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: SuperAdmin/MarchentProfileDetail
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarchentProfileDetail.ToListAsync());
        }

        // GET: SuperAdmin/MarchentProfileDetail/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: SuperAdmin/MarchentProfileDetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmin/MarchentProfileDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarchentProfileDetailVm vm)
        {
            if (ModelState.IsValid)
            {
                MarchentProfileDetail entity = new MarchentProfileDetail();
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
                paymentDetail.BranchId = vm.BranchId;
                paymentDetail.RoutingName = vm.RoutingName;
                _context.MarchentPaymentDetails.Add(paymentDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: SuperAdmin/MarchentProfileDetail/Edit/5
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
            var odlBranch = _context.BankBranch.FirstOrDefault(c => c.Id == marchant.BranchId);
            vm.OdlBranchName = odlBranch.BranchName;
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
                if (vm.PaymentTypeId!=Guid.Empty && vm.PaymentBankingId!=null && vm.BranchId!=null)
                {
                    var paymentDetail = _context.MarchentPaymentDetails.FirstOrDefault(c => c.MarchentId == entity.Id);
                    if (paymentDetail!=null)
                    {
            
                        paymentDetail.MarchentId = entity.Id;
                        paymentDetail.PaymentNameId = vm.PaymentBankingId;
                        paymentDetail.PaymentTypeId = vm.PaymentTypeId;
                        paymentDetail.BranchId = vm.BranchId;
                        paymentDetail.RoutingName = vm.RoutingName;
                        _context.MarchentPaymentDetails.Update(paymentDetail);
                    }
                    else
                    {
                        var paymentDetail1 = new MarchentPaymentDetails();
                        paymentDetail1.MarchentId = entity.Id;
                        paymentDetail1.PaymentNameId = vm.PaymentBankingId;
                        paymentDetail1.PaymentTypeId = vm.PaymentTypeId;
                        paymentDetail1.BranchId = vm.BranchId;
                        paymentDetail1.RoutingName = vm.RoutingName;
                        _context.MarchentPaymentDetails.Add(paymentDetail1);
                    }
                }
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
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
            var org = _context.PaymentBankingOrganization.Where(o=>o.PaymentBankingTypeId==id);
            return Json(org.ToList());
        }
        [HttpGet("/MarchentProfileDetail/GetBranch")]
        public IActionResult GetBranch(Guid id)
        {
            var dis = _context.BankBranch.Where(x => x.BankId == id);
            return Json(dis.ToList());
        }
        private bool MarchentProfileDetailExists(Guid id)
        {
            return _context.MarchentProfileDetail.Any(e => e.Id == id);
        }
    }
}