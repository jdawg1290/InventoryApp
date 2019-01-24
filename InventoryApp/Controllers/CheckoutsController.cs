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
            var applicationDbContext = _context.Checkout.Include(c => c.InventoryItem).ThenInclude(i => i.Item);
            return View(await applicationDbContext.Where(c => c.End == null).ToListAsync());
        }

        // GET: Checkouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Serial")] CheckoutCreateViewModel checkout)
        {
            var SearchItem = await _context.InventoryItem.FirstOrDefaultAsync(i => i.Serial == checkout.Serial);
            if (SearchItem == null)
            {
                ModelState.AddModelError("INVALID_SERIAL", "Serial Number does not exist");
            }
            var checkoutStatus = await _context.Checkout.Where(c => c.End == null).Where(c => c.InventoryItemID == SearchItem.ID).CountAsync();
            if (checkoutStatus > 0)
            {
                ModelState.AddModelError("ALREADY_OUT", "Item is currently checked out.");
            }
            if (ModelState.IsValid)
            {
                var newCheckout = new Checkout
                {
                    UserName = checkout.UserName,
                    InventoryItemID = SearchItem.ID,
                    Start = DateTime.Now,
                    End = null
                };
                _context.Add(newCheckout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkout);
        }

        public async Task<IActionResult> Checkin(string Serial)
        {
            ViewData["Serial"] = Serial;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkin([Bind("Serial")] CheckinViewModel checkin)
        {
            var InventoryItem = await _context.InventoryItem.SingleOrDefaultAsync(i => i.Serial == checkin.Serial);
            if (InventoryItem == null)
            {
                ModelState.AddModelError("INVALID_SERIAL", "Serial Number does not exist");
            }
            var pendingCheckout = await _context.Checkout.Where(c => c.End == null).SingleOrDefaultAsync(c => c.InventoryItemID == InventoryItem.ID);
            if (pendingCheckout == null)
            {
                ModelState.AddModelError("NOT_CHECKED_OUT", "Item is not checked out");
            }
            if (ModelState.IsValid)
            {
                pendingCheckout.End = DateTime.Now;
                _context.Entry(pendingCheckout).State = EntityState.Modified;
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkin);
        }

        public async Task<IActionResult> CheckoutsLog()
        {
            return View(await _context.Checkout.Include(c => c.InventoryItem).ThenInclude(i => i.Item).OrderByDescending(c => c.Start).ToListAsync());
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkout.Any(e => e.ID == id);
        }
    }
}
