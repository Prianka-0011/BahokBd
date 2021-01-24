using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using BahokBdDelivery.Views.ViewModels;
using static BahokBdDelivery.Helper;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class PaymentBankingTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentBankingTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/PaymentBankingTypes
        [HttpGet]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentBankingType.ToListAsync());
        }
        [HttpGet("/PaymentBankingTypes/litPayType")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetAllType()
        {
            return View(await _context.PaymentBankingType.ToListAsync());
        }
  
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                return View(new PaymentBankingType());
            else
            {
                var typeModel = await _context.PaymentBankingType.FindAsync(id);
                if (typeModel == null)
                {
                    return NotFound();
                }
                return View(typeModel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid id, PaymentBankingType paymentBankingType)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    PaymentBankingType entity = new PaymentBankingType();
                    entity.Id = Guid.NewGuid();
                    entity.BankingMethodName = paymentBankingType.BankingMethodName;
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    try
                    {
                        _context.Update(paymentBankingType);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PaymentBankingTypeExists(paymentBankingType.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.PaymentBankingType.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", paymentBankingType) });

        }

        // GET: SuperAdmin/PaymentBankingTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var paymentBankingType = await _context.PaymentBankingType.FindAsync(id);
            _context.PaymentBankingType.Remove(paymentBankingType);
            await _context.SaveChangesAsync();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.PaymentBankingType.ToList()) });
        }

        // POST: SuperAdmin/PaymentBankingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentBankingType = await _context.PaymentBankingType.FindAsync(id);
            _context.PaymentBankingType.Remove(paymentBankingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentBankingTypeExists(Guid id)
        {
            return _context.PaymentBankingType.Any(e => e.Id == id);
        }
    }
}
