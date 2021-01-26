using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class MarchentStoresController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MarchentStoresController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMarhentStore()
        {
            //ViewData["areaList"] = new SelectList(_context.DeliveryAreaPrices, "Id", "Area");
            return View();
        }
        [HttpGet("/MarchentStores/GetArea")]
        public IActionResult GetArea(Guid id)
        {
            var area = _context.DeliveryAreaPrices.ToList();
            return Json(area);
        }
    }
}
