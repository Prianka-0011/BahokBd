using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BahokBdDelivery.Data;
using BahokBdDelivery.Models;
using BahokBdDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BahokBdDelivery.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class MarchentChargeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MarchentChargeController(ApplicationDbContext context )
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AddCharge(Guid id)
        {
            var marMarchentCharge = new MarchentChargeVm();
            marMarchentCharge.MarchentId = id;
            ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList(); 
            //string msg = Convert.ToString(TempData["charge"]);
            //ViewBag.charge = msg;
            return View(marMarchentCharge);
           
        }
        [HttpPost("/MarchentCharge/AddCharge1")]
        public ActionResult AddCharge1(List<MarchentChargeVm> postArrItem)
        {
            MarchentCharge charge;

            if (postArrItem != null)
            {
                foreach (var item in postArrItem)
                {
                    var areaId = _context.MarchentCharge.FirstOrDefault(c => c.DeliveryAreaPriceId == item.Id && c.MarchentId == item.MarchentId);
                    if (areaId != null)
                    {

                        areaId.IncreaseCharge = item.IncreaseChargePerKg;
                        areaId.BaseCharge = item.BaseChargeAmount;
                        areaId.Location = item.Area;
                        _context.MarchentCharge.Update(areaId);
                    }
                    else
                    {
                        charge = new MarchentCharge();
                        charge.MarchentId = item.MarchentId;
                        charge.IncreaseCharge = item.IncreaseChargePerKg;
                        charge.BaseCharge = item.BaseChargeAmount;
                        charge.DeliveryAreaPriceId = item.Id;
                        charge.Location = item.Area;
                        _context.MarchentCharge.Add(charge);
                    }

                    // mProfie = _context.MarchentProfileDetail.Find(item.MarchentId);
                }

                 _context.SaveChanges();
                ViewBag.charge = "Charge Add Successfully";
                return RedirectToAction("AddCharge");
            }
            return Ok();
           
        }
    }
}
