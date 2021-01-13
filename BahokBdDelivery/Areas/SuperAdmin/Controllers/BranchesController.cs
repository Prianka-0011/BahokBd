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
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/BankBranches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Branch;
            return View(await applicationDbContext.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                
                return View(new Branch());
            }
                
            else
            {
                var branchModel = await _context.Branch.FindAsync(id);
                if (branchModel == null)
                {
                    return NotFound();
                }
                
                return View(branchModel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid id, Branch branch)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    Branch entity = new Branch();
                    entity.Id = Guid.NewGuid();
                    entity.Name = branch.Name;
                    
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    try
                    {
                        _context.Update(branch);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BankBranchExists(branch.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllBranch", _context.Branch.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", branch) });

        }

        // GET: SuperAdmin/BankBranches/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bankBranch = await _context.BankBranch
        //        .Include(b => b.Bank)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bankBranch == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bankBranch);
        //}

        // POST: SuperAdmin/BankBranches/Delete/5
        //[ ActionName("Delete")]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bankBranch = await _context.Branch.FindAsync(id);
            _context.Branch.Remove(bankBranch);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllBranch", _context.Branch.ToList()) });
        }

        private bool BankBranchExists(Guid id)
        {
            return _context.BankBranch.Any(e => e.Id == id);
        }
    }
}
