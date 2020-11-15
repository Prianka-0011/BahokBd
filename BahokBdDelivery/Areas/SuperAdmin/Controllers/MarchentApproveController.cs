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
        public IActionResult ApproveCreate(Guid id)
        {
            var marMarchentApprove = new MarchentApproveVm();
            marMarchentApprove.MarchentId = id;
            ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
            return View(marMarchentApprove);
        }
        [HttpGet("/MarchentApprove/ApproveCreate1")]
        public JsonResult ApproveCreate1( string obj)
        {
            //string[] arr = itemlist.Split(',');
            //foreach (var item in arr)
            //{
            //    var currentId = item;
            //}
           
            return Json("");
        }
        //public IActionResult Approve(Guid id)
        //{
        //    var marchent = _context.MarchentProfileDetail.Find(id);
        //    var marchentCharge = new MarchentApproveVm();
        //    if (marchent!=null)
        //    {

        //        marchentCharge.MarchentId = marchent.Id;
        //    }
        //    ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
        //    return View();
        //}
        //[HttpPost("/MarchentApprove/Aprove")]
        //public IActionResult Approve(string itemlist)
        //{
        //    string[] arr = itemlist.Split(',');
        //    foreach (var item in arr)
        //    {
        //        var currentId = item;
        //    }
        //    ViewBag.deiveryArea = _context.DeliveryAreaPrices.ToList();
        //    return View();
        //}
    }
}
