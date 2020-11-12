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
    public class BankBranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankBranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuperAdmin/BankBranches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BankBranch.Include(b => b.Bank);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SuperAdmin/BankBranches/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankBranch = await _context.BankBranch
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankBranch == null)
            {
                return NotFound();
            }

            return View(bankBranch);
        }

        // GET: SuperAdmin/BankBranches/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.PaymentBankingOrganization, "Id", "OrganizationName");
            return View();
        }

        // POST: SuperAdmin/BankBranches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchName,RoutingName,BankId")] BankBranch bankBranch)
        {
            if (ModelState.IsValid)
            {
                bankBranch.Id = Guid.NewGuid();
                _context.Add(bankBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.PaymentBankingOrganization, "Id", "OrganizationName", bankBranch.BankId);
            return View(bankBranch);
        }

        // GET: SuperAdmin/BankBranches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankBranch = await _context.BankBranch.FindAsync(id);
            if (bankBranch == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.PaymentBankingOrganization, "Id", "OrganizationName", bankBranch.BankId);
            return View(bankBranch);
        }

        // POST: SuperAdmin/BankBranches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BranchName,RoutingName,BankId")] BankBranch bankBranch)
        {
            if (id != bankBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankBranchExists(bankBranch.Id))
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
            ViewData["BankId"] = new SelectList(_context.PaymentBankingOrganization, "Id", "OrganizationName", bankBranch.BankId);
            return View(bankBranch);
        }

        // GET: SuperAdmin/BankBranches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankBranch = await _context.BankBranch
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankBranch == null)
            {
                return NotFound();
            }

            return View(bankBranch);
        }

        // POST: SuperAdmin/BankBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bankBranch = await _context.BankBranch.FindAsync(id);
            _context.BankBranch.Remove(bankBranch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankBranchExists(Guid id)
        {
            return _context.BankBranch.Any(e => e.Id == id);
        }
    }
}
