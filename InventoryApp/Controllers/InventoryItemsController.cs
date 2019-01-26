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
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventoryItems
        public async Task<IActionResult> Index()
        {
            var list = await _context.InventoryItem.Include(i => i.Item).Include(i => i.Checkouts).ToListAsync();
            return View(list);
        }

        // GET: InventoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem
                .Include(i => i.Item)
                .Include(i => i.Checkouts)
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewData["isCheckedOut"] = inventoryItem.Checkouts.Where(c => c.End == null).Count() == 1;
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // GET: InventoryItems/Create
        public IActionResult Create()
        {
            ViewData["ItemID"] = new SelectList(_context.Item, "ID", "Name");
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Serial,ItemID")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemID"] = new SelectList(_context.Item, "ID", "Name", inventoryItem.ItemID);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            ViewData["ItemID"] = new SelectList(_context.Item, "ID", "Name", inventoryItem.ItemID);
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Serial,ItemID")] InventoryItem inventoryItem)
        {
            if (id != inventoryItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemExists(inventoryItem.ID))
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
            ViewData["ItemID"] = new SelectList(_context.Item, "ID", "Name", inventoryItem.ItemID);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            _context.InventoryItem.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItem.Any(e => e.ID == id);
        }
    }
}
