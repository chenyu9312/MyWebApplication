using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Data;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers
{
    public class CashesController : Controller
    {
        private readonly MyDbContext _context;

        public CashesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Cashes
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Receipts.Include(c => c.Branch);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Cashes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Receipts
                .Include(c => c.Branch)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (cash == null)
            {
                return NotFound();
            }

            return View(cash);
        }

        // GET: Cashes/Create
        //public IActionResult Create()
        //{
        //    ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchID");
        //    return View();


        //}



        // POST: Cashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult Create()
        {
            PopulateBranchesDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("ID,ReceiptNumber,Amount,ClientName,Remarks,ReceiptDate,Payment,CurrencyCode,BranchID")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateBranchesDropDownList(cash.BranchID);
            return View(cash);
        }
        // GET: Cashes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Receipts.SingleOrDefaultAsync(m => m.ID == id);
            if (cash == null)
            {
                return NotFound();
            }
            //ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchID", cash.BranchID);
            PopulateBranchesDropDownList(cash.BranchID);
            return View(cash);
        }

        // POST: Cashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReceiptNumber,Amount,ClientName,Remarks,ReceiptDate,Payment,CurrencyCode,BranchID")] Cash cash)
        {
            if (id != cash.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashExists(cash.ID))
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
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchID", cash.BranchID);
            return View(cash);
        }
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashToUpdate = await _context.Receipts
                .SingleOrDefaultAsync(c => c.ID == id);

            if (await TryUpdateModelAsync<Cash>(cashToUpdate, "",
                 c => c.ID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateBranchesDropDownList(cashToUpdate.BranchID);
            return View(cashToUpdate);

        }

        // GET: Cashes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Receipts
                .Include(c => c.Branch)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (cash == null)
            {
                return NotFound();
            }

            return View(cash);
        }

        // POST: Cashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cash = await _context.Receipts.SingleOrDefaultAsync(m => m.ID == id);
            _context.Receipts.Remove(cash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashExists(int id)
        {
            return _context.Receipts.Any(e => e.ID == id);
        }

        private void PopulateBranchesDropDownList(object selectedBranch = null)
        {
            var branchQuery = from d in _context.Branches
                              orderby d.BranchID
                              select d;
            ViewBag.BranchID = new SelectList(branchQuery.AsNoTracking(), "BranchID","Address", selectedBranch);
        }
       
    }
}
