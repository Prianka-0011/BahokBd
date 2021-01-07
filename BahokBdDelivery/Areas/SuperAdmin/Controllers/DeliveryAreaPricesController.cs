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
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
            
                return View(new DeliveryAreaPrices());
            }

            else
            {
                var deliveryAreaModel = await _context.DeliveryAreaPrices.FindAsync(id);
                if (deliveryAreaModel == null)
                {
                    return NotFound();
                }
                
                return View(deliveryAreaModel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid id, DeliveryAreaPrices deliveryarea)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    DeliveryAreaPrices entity = new DeliveryAreaPrices();
                    entity.Id = Guid.NewGuid();
                    entity.Area = deliveryarea.Area;
                    entity.BaseChargeAmount = deliveryarea.BaseChargeAmount;
                    entity.IncreaseChargePerKg = deliveryarea.IncreaseChargePerKg;
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    try
                    {
                        _context.Update(deliveryarea);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DeliveryAreaPricesExists(deliveryarea.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllArea", _context.DeliveryAreaPrices.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", deliveryarea) });

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
        public async Task<IActionResult> Create( DeliveryAreaPrices deliveryAreaPrices)
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
        public async Task<IActionResult> Edit(Guid id, DeliveryAreaPrices deliveryAreaPrices)
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
