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
    public class DeliveryAreaPricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryAreaPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/DeliveryAreaPrices
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryAreaPrices.ToListAsync());
        }

        // GET: SuperAdmin/DeliveryAreaPrices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAreaPrices = await _context.DeliveryAreaPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryAreaPrices == null)
            {
                return NotFound();
            }

            return View(deliveryAreaPrices);
        }

        // GET: SuperAdmin/DeliveryAreaPrices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperAdmin/DeliveryAreaPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Area,BaseChargeAmount,IncreaseChargePerKg")] DeliveryAreaPrices deliveryAreaPrices)
        {
            if (ModelState.IsValid)
            {
                deliveryAreaPrices.Id = Guid.NewGuid();
                _context.Add(deliveryAreaPrices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryAreaPrices);
        }

        // GET: SuperAdmin/DeliveryAreaPrices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAreaPrices = await _context.DeliveryAreaPrices.FindAsync(id);
            if (deliveryAreaPrices == null)
            {
                return NotFound();
            }
            return View(deliveryAreaPrices);
        }

        // POST: SuperAdmin/DeliveryAreaPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Area,BaseChargeAmount,IncreaseChargePerKg")] DeliveryAreaPrices deliveryAreaPrices)
        {
            if (id != deliveryAreaPrices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryAreaPrices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryAreaPricesExists(deliveryAreaPrices.Id))
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
            return View(deliveryAreaPrices);
        }

        // GET: SuperAdmin/DeliveryAreaPrices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAreaPrices = await _context.DeliveryAreaPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryAreaPrices == null)
            {
                return NotFound();
            }

            return View(deliveryAreaPrices);
        }

        // POST: SuperAdmin/DeliveryAreaPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deliveryAreaPrices = await _context.DeliveryAreaPrices.FindAsync(id);
            _context.DeliveryAreaPrices.Remove(deliveryAreaPrices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryAreaPricesExists(Guid id)
        {
            return _context.DeliveryAreaPrices.Any(e => e.Id == id);
        }
    }
}
