using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AreaPriceController : Controller
    {
        private ApplicationDbContext _context;

        public AreaPriceController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var areaPrice = await _context.DeliveryAreaPrices.ToListAsync();
            return View(areaPrice);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //HttpPost for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryAreaPrice areaDeliveryPrice)
        {
            if (ModelState.IsValid)
            {
                //For  Redandent category Name do not Create
                var sameCheck = _context.DeliveryAreaPrices.FirstOrDefault(c => c.Area == areaDeliveryPrice.Area);
                if (sameCheck != null)
                {
                    TempData["create"] = "This Category All Ready Exist";
                    return View(areaDeliveryPrice);
                }
                _context.DeliveryAreaPrices.Add(areaDeliveryPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(areaDeliveryPrice);
        }
        [HttpGet]
        public IActionResult Edit(Guid ?id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var areaDeliveryPrice = _context.DeliveryAreaPrices.Find(id);
            return View(areaDeliveryPrice);
        }
        //HttpPost for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DeliveryAreaPrice deliveryAreaPriceVm)
        {
            if (id != deliveryAreaPriceVm.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var samecheck = _context.DeliveryAreaPrices.FirstOrDefault(c => c.Area == deliveryAreaPriceVm.Area && c.Id != deliveryAreaPriceVm.Id);
                
                if (samecheck != null)
                {
                    TempData["create"] = "This Category All Ready Exist";
                    return View(deliveryAreaPriceVm);
                }
             
                var deliveryAreaPrice = _context.DeliveryAreaPrices.FirstOrDefault(c => c.Id == deliveryAreaPriceVm.Id);
                deliveryAreaPrice.Area = deliveryAreaPriceVm.Area;
                deliveryAreaPrice.Price = deliveryAreaPriceVm.Price;
                _context.DeliveryAreaPrices.Update(deliveryAreaPrice);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
              return View(deliveryAreaPriceVm);
        }
        //HttpGet for Delete
        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _context.DeliveryAreaPrices.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var areaPrice = _context.DeliveryAreaPrices.FirstOrDefault(c => c.Id == id);
            if (areaPrice == null)
            {
                return NotFound();
            }
            _context.DeliveryAreaPrices.Remove(areaPrice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
