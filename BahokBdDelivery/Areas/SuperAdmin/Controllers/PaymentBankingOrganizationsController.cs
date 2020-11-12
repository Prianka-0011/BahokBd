using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;

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

        // GET: SuperAdmin/PaymentBankingOrganizations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingOrganization = await _context.PaymentBankingOrganization
                .Include(p => p.PaymentBankingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentBankingOrganization == null)
            {
                return NotFound();
            }

            return View(paymentBankingOrganization);
        }

        // GET: SuperAdmin/PaymentBankingOrganizations/Create
        public IActionResult Create()
        {
            ViewData["PaymentBankingTypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName");
            return View();
        }

        // POST: SuperAdmin/PaymentBankingOrganizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PaymentBankingOrganization paymentBankingOrganization)
        {
            if (ModelState.IsValid)
            {
                paymentBankingOrganization.Id = Guid.NewGuid();
                _context.Add(paymentBankingOrganization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentBankingTypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName", paymentBankingOrganization.PaymentBankingTypeId);
            return View(paymentBankingOrganization);
        }

        // GET: SuperAdmin/PaymentBankingOrganizations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingOrganization = await _context.PaymentBankingOrganization.FindAsync(id);
            if (paymentBankingOrganization == null)
            {
                return NotFound();
            }
            ViewData["PaymentBankingTypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName", paymentBankingOrganization.PaymentBankingTypeId);
            return View(paymentBankingOrganization);
        }

        // POST: SuperAdmin/PaymentBankingOrganizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentBankingOrganization paymentBankingOrganization)
        {
            if (id != paymentBankingOrganization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentBankingOrganization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentBankingOrganizationExists(paymentBankingOrganization.Id))
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
            ViewData["PaymentBankingTypeId"] = new SelectList(_context.PaymentBankingType, "Id", "BankingMethodName", paymentBankingOrganization.PaymentBankingTypeId);
            return View(paymentBankingOrganization);
        }

        // GET: SuperAdmin/PaymentBankingOrganizations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingOrganization = await _context.PaymentBankingOrganization
                .Include(p => p.PaymentBankingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentBankingOrganization == null)
            {
                return NotFound();
            }

            return View(paymentBankingOrganization);
        }

        // POST: SuperAdmin/PaymentBankingOrganizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentBankingOrganization = await _context.PaymentBankingOrganization.FindAsync(id);
            _context.PaymentBankingOrganization.Remove(paymentBankingOrganization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentBankingOrganizationExists(Guid id)
        {
            return _context.PaymentBankingOrganization.Any(e => e.Id == id);
        }
    }
}
