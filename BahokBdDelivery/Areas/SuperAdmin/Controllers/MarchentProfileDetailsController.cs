﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using BahokBdDelivery.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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

        // GET: SuperAdmin/MarchentProfileDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarchentProfileDetails.ToListAsync());
        }

        // GET: SuperAdmin/MarchentProfileDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marchentProfileDetails = await _context.MarchentProfileDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marchentProfileDetails == null)
            {
                return NotFound();
            }

            return View(marchentProfileDetails);
        }

        // GET: SuperAdmin/MarchentProfileDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmin/MarchentProfileDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarchentProfileDetailsVm vm)
        {
            if (ModelState.IsValid)
            {
                MarchentProfileDetails entity = new MarchentProfileDetails();
                entity.Name = vm.Name;
                entity.Email = vm.Email;
                entity.Phone = vm.Phone;
                entity.BranchName = vm.BranchName;
                entity.BusinessName = vm.BusinessName;
                entity.BusinessLink = vm.BusinessLink;
                entity.BusinessAddress = vm.BusinessAddress;
                entity.AccountName = vm.AccountName;
                entity.AccountNumber = vm.AccountNumber;
                entity.RoutingName = vm.RoutingName;
                entity.ProfileStatus = vm.ProfileStatus;
                entity.LastIpAddress = vm.LastIpAddress;
                entity.DateTime = vm.DateTime;
                entity.PaymentTypeId = vm.PaymentTypeId;
                entity.PaymentBankingId = vm.PaymentBankingId;
                string uniqueFileNameForImage = null;
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
                    uniqueFileNameForImage = Guid.NewGuid().ToString() + "_" + vm.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileNameForImage);
                    await vm.Logo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    entity.Logo = "logos/" + uniqueFileNameForImage;
                }
                entity.Id = Guid.NewGuid();
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: SuperAdmin/MarchentProfileDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MarchentProfileDetailsVm vm = new MarchentProfileDetailsVm();
            var entity = await _context.MarchentProfileDetails.FindAsync(id);
            if (vm == null)
            {
                return NotFound();
            }
            vm.Name = entity.Name;
            vm.Email = entity.Email;
            vm.Phone = entity.Phone;
            vm.BranchName = entity.BranchName;
            vm.BusinessName = entity.BusinessName;
            vm.BusinessLink = entity.BusinessLink;
            vm.BusinessAddress = entity.BusinessAddress;
            vm.AccountName = entity.AccountName;
            vm.AccountNumber = entity.AccountNumber;
            vm.RoutingName = entity.RoutingName;
            vm.ProfileStatus = entity.ProfileStatus;
            vm.LastIpAddress = entity.LastIpAddress;
            vm.DateTime = entity.DateTime;
            vm.DisplayImage = entity.Image;
            vm.DisplayLogo = entity.Logo;
            vm.PaymentTypeId = entity.PaymentTypeId;
            vm.PaymentBankingId = entity.PaymentBankingId;
            var TypeNameObj = _context.PaymentBankingType.Find(entity.PaymentTypeId);
            ViewBag.TypeName = TypeNameObj.BankingMethodName;
            var Organize = _context.PaymentBankingOrganization.Find(entity.PaymentBankingId);
            ViewBag.Organize = Organize.OrganizationName;
            return View(vm);
        }

        // POST: SuperAdmin/MarchentProfileDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Image,Logo,Phone,BusinessName,BusinessLink,BusinessAddress,AccountName,AccountNumber,RoutingName,BranchName,ProfileStatus,LastIpAddress,DateTime,PaymentTypeId,PaymentBankingId")] MarchentProfileDetails marchentProfileDetails)
        {
            if (id != marchentProfileDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marchentProfileDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarchentProfileDetailsExists(marchentProfileDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(marchentProfileDetails);
        }

        // GET: SuperAdmin/MarchentProfileDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marchentProfileDetails = await _context.MarchentProfileDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marchentProfileDetails == null)
            {
                return NotFound();
            }

            return View(marchentProfileDetails);
        }

        // POST: SuperAdmin/MarchentProfileDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marchentProfileDetails = await _context.MarchentProfileDetails.FindAsync(id);
            _context.MarchentProfileDetails.Remove(marchentProfileDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        //[HttpGet("/MarchentProfileDetails/GetBankingTypeById")]
        //public IActionResult GetBankingTypeById(Guid id)
        //{
        //    var dvs = _context.PaymentBankingType.Where(x => x.Id == id);
        //    return Json(dvs.ToList());
        //}
        [HttpGet("/MarchentProfileDetails/GetBankingType")]
        public IActionResult GetBankingType()
        {
            var dvs = _context.PaymentBankingType;
            return Json(dvs.ToList());
        }
        [HttpGet("/MarchentProfileDetails/GetOrganizationName")]
        public IActionResult GetOrganizationName(Guid id)
        {
            var dis = _context.PaymentBankingOrganization.Where(x => x.PaymentBankingTypeId == id);
            return Json(dis.ToList());
        }
        private bool MarchentProfileDetailsExists(Guid id)
        {
            return _context.MarchentProfileDetails.Any(e => e.Id == id);
        }
    }
}