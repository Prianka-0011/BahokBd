using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using BahokBdDelivery.Views.ViewModels;
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
        public IActionResult AddMarhentStore(Guid id)
        {
            MarchentStoreVm vm = new MarchentStoreVm();
            vm.MarchentId = id;            //ViewData["areaList"] = new SelectList(_context.DeliveryAreaPrices, "Id", "Area");
            return View(vm);
        }
        [HttpGet("/MarchentStores/GetArea")]
        public IActionResult GetArea(Guid id)
        {
            var area = _context.DeliveryAreaPrices.ToList();
            return Json(area);
        }
        [HttpPost("/MarchentStores/SubmitStore")]
        public ActionResult AddCharge1(List<MarchentStoreVm> arrStoreItem)
        {
            MarchentStore store;

            if (arrStoreItem != null)
            {
                foreach (var item in arrStoreItem)
                {


                    store = new MarchentStore();
                    store.MarchentId = item.MarchentId;
                    store.StoreName = item.StoreName;
                    store.ManagerName = item.ManagerName;
                    store.Phone = item.Phone;
                    store.LocationId = item.LocationId;
                    store.Address = item.Address;
                    store.CreatedDateTime = DateTime.Now;
                    store.Status = true;
                    _context.MarchentStore.Add(store);


                    // mProfie = _context.MarchentProfileDetail.Find(item.MarchentId);
                }

                _context.SaveChanges();
                ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
                ViewBag.charge = "Charge Add Successfully";
                return Ok();
            }
            return Ok();

        }
    }
}
