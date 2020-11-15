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
        public IActionResult ApproveCreate(Guid id)
        {
            var marMarchentApprove = new MarchentApproveVm();
            marMarchentApprove.MarchentId = id;
            ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
            return View(marMarchentApprove);
        }
        [HttpPost("/MarchentApprove/ApproveCreate1")]
        public async Task< IActionResult> ApproveCreate1(List<MarchentApproveVm> postArrItem)
        {
            MarchentCharge charge;
           // MarchentProfileDetail mProfie;
            
            if (postArrItem!=null)
            {
                foreach (var item in postArrItem)
                {
                    var areaId = _context.MarchentCharge.FirstOrDefault(c=>c.DeliveryAreaPriceId==item.Id && c.MarchentId==item.MarchentId);
                    if (areaId !=null)
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
                
              await  _context.SaveChangesAsync();
            }

            return Ok();
        }

    }
}
