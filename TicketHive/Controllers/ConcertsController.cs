using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketHive.Data;
using TicketHive.Models;

namespace TicketHive.Controllers
{
    public class ConcertsController : Controller
    {
        private readonly TicketHiveContext _context;

        public ConcertsController(TicketHiveContext context)
        {
            _context = context;
        }

        // GET: Concerts
        public async Task<IActionResult> Index()
        {
            var ticketHiveContext = _context.Concert.Include(c => c.Category).Include(c => c.Owner);
            return View(await ticketHiveContext.ToListAsync());
        }

        // GET: Concerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .Include(c => c.Category)
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(m => m.ConcertId == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId");
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcertId,Title,Description,Location,ConcertTime,PublishDate,OwnerId,CategoryId")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", concert.OwnerId);
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", concert.OwnerId);
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConcertId,Title,Description,Location,ConcertTime,PublishDate,OwnerId,CategoryId")] Concert concert)
        {
            if (id != concert.ConcertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.ConcertId))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", concert.OwnerId);
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .Include(c => c.Category)
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(m => m.ConcertId == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concert = await _context.Concert.FindAsync(id);
            if (concert != null)
            {
                _context.Concert.Remove(concert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(int id)
        {
            return _context.Concert.Any(e => e.ConcertId == id);
        }
    }
}
