using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryApp.Controllers
{
    [Authorize]
    public class CheckoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checkouts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Checkout.Include(c => c.InventoryItem).Include(i => i.InventoryItem.Item);
            return View(await applicationDbContext.Where(c => c.End == null).ToListAsync());
        }

        // GET: Checkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout
                .Include(c => c.InventoryItem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // GET: Checkouts/Create
        public IActionResult Create()
        {
            ViewData["InventoryItemID"] = new SelectList(_context.InventoryItem, "ID", "ID");
            return View();
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,InventoryItemID,Start,End")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryItemID"] = new SelectList(_context.InventoryItem, "ID", "ID", checkout.InventoryItemID);
            return View(checkout);
        }

        // GET: Checkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewData["InventoryItemID"] = new SelectList(_context.InventoryItem, "ID", "ID", checkout.InventoryItemID);
            return View(checkout);
        }

        // POST: Checkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,InventoryItemID,Start,End")] Checkout checkout)
        {
            if (id != checkout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.ID))
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
            ViewData["InventoryItemID"] = new SelectList(_context.InventoryItem, "ID", "ID", checkout.InventoryItemID);
            return View(checkout);
        }

        // GET: Checkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkout
                .Include(c => c.InventoryItem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // POST: Checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkout = await _context.Checkout.FindAsync(id);
            _context.Checkout.Remove(checkout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkout.Any(e => e.ID == id);
        }
    }
}
