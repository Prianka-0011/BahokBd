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
    public class PaymentBankingTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentBankingTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/PaymentBankingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentBankingType.ToListAsync());
        }

        // GET: SuperAdmin/PaymentBankingTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingType = await _context.PaymentBankingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentBankingType == null)
            {
                return NotFound();
            }

            return View(paymentBankingType);
        }

        // GET: SuperAdmin/PaymentBankingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmin/PaymentBankingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BankingMethodName")] PaymentBankingType paymentBankingType)
        {
            if (ModelState.IsValid)
            {
                paymentBankingType.Id = Guid.NewGuid();
                _context.Add(paymentBankingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentBankingType);
        }

        // GET: SuperAdmin/PaymentBankingTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingType = await _context.PaymentBankingType.FindAsync(id);
            if (paymentBankingType == null)
            {
                return NotFound();
            }
            return View(paymentBankingType);
        }

        // POST: SuperAdmin/PaymentBankingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BankingMethodName")] PaymentBankingType paymentBankingType)
        {
            if (id != paymentBankingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View(paymentBankingType);
        }

        // GET: SuperAdmin/PaymentBankingTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentBankingType = await _context.PaymentBankingType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentBankingType == null)
            {
                return NotFound();
            }

            return View(paymentBankingType);
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
