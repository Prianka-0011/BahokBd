using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using static BahokBdDelivery.Helper;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class PaymentBankingOrganizationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentBankingOrganizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/PaymentBankingOrganizations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentBankingOrganization.Include(p => p.PaymentBankingType);
            return View(await applicationDbContext.ToListAsync());
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                ViewData["TypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName");
                return View(new PaymentBankingOrganization());
            }

            else
            {
                var bankModel = await _context.PaymentBankingOrganization.FindAsync(id);
                if (bankModel == null)
                {
                    return NotFound();
                }
                ViewData["TypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName", bankModel.PaymentBankingTypeId);
                return View(bankModel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid id, PaymentBankingOrganization bank)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    PaymentBankingOrganization entity = new PaymentBankingOrganization();
                    entity.Id = Guid.NewGuid();
                    entity.OrganizationName = bank.OrganizationName;
                    entity.PaymentBankingTypeId = bank.PaymentBankingTypeId;
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    try
                    {
                        _context.Update(bank);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PaymentBankingOrganizationExists(bank.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllBank", _context.PaymentBankingOrganization.Include(c => c.PaymentBankingType).ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", bank) });

        }


        
     //   GET: SuperAdmin/PaymentBankingOrganizations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paymentBankingOrganization = await _context.PaymentBankingOrganization.FindAsync(id);
               _context.PaymentBankingOrganization.Remove(paymentBankingOrganization);
             await _context.SaveChangesAsync();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllBank", _context.PaymentBankingOrganization.Include(c => c.PaymentBankingType).ToList()) });

        }

        // POST: SuperAdmin/PaymentBankingOrganizations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var paymentBankingOrganization = await _context.PaymentBankingOrganization.FindAsync(id);
        //    _context.PaymentBankingOrganization.Remove(paymentBankingOrganization);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PaymentBankingOrganizationExists(Guid id)
        {
            return _context.PaymentBankingOrganization.Any(e => e.Id == id);
        }
    }
}
