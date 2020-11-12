using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class MarchentApproveController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly IWebHostEnvironment _webHostEnvironment;

        public MarchentApproveController(ApplicationDbContext context /*IWebHostEnvironment webHostEnvironment*/)
        {
            _context = context;
            //_webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Aprove(Guid id)
        {
            var marchent = _context.MarchentProfileDetail.Find(id);
            var marchentCharge = new MarchentApproveVm();
            if (marchent!=null)
            {
               
                marchentCharge.MarchentId = marchent.Id;
            }
            ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
            return View(marchentCharge);
        }
        [HttpPost]
        public async Task<IActionResult> Aprove(MarchentApproveVm vm)
        {
            //foreach (Guid areaId in vm.Where(m => m.IsSelected == true).Select(m => m.model.Id))
            //{
            //}
            ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
            return View();
        }
    }
}
