using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHive.Data;
using TicketHive.Models;

namespace TicketHive.Controllers
{
    // restricted access
    [Authorize]
    public class ConcertsController : Controller
    {
        private readonly TicketHiveContext _context;

        //Added for file upload with server
        private readonly IWebHostEnvironment _env;

        public ConcertsController(TicketHiveContext context, IWebHostEnvironment env)
        {
            _context = context;

            _env = env;
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerName");
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcertId,Title,Description,Location,ConcertTime,OwnerId,CategoryId,Filename,FormFile")] Concert concert)
        {

            // Set the publish date
            concert.PublishDate = DateTime.Now;

            if (ModelState.IsValid)
            {

                if (concert.FormFile != null)
                {
                    // Create a unique filename using a GUID
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(concert.FormFile.FileName);

                    // Set the filename from upload file
                    concert.Filename = filename;

                    // Use Path.Combine to get the file path to save file to
                    // Was used before IWebHostEnvironment
                    //string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "concert-photos", filename);

                    // Use IWebHostEnvironment.WebRootPath and ensure folder exists
                    var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var uploads = Path.Combine(webRoot, "concert-photos");
                    Directory.CreateDirectory(uploads);

                    // Use Path.Combine to get the file path to save file to
                    string saveFilePath = Path.Combine(uploads, filename);

                    // Save file
                    using (var fileStream = new FileStream(saveFilePath, FileMode.Create))
                    {
                        await concert.FormFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(concert);
                await _context.SaveChangesAsync();
                //return RedirectToAction();
                return RedirectToAction("Index", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerName", concert.OwnerId);
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerName", concert.OwnerId);
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConcertId,Title,Description,Location,ConcertTime,OwnerId,CategoryId,Filename,FormFile")] Concert concert)
        {
            // Set the publish date
            concert.PublishDate = DateTime.Now;

            if (id != concert.ConcertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get the existing concert from the database
                var existingConcert = await _context.Concert.AsNoTracking().FirstOrDefaultAsync(c => c.ConcertId == id);
                if (existingConcert == null)
                {
                    return NotFound();
                }

                if (concert.FormFile != null)
                {
                    // Create a unique filename using a GUID
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(concert.FormFile.FileName); 

                    // Set the filename from upload file
                    concert.Filename = filename;

                    // Use IWebHostEnvironment.WebRootPath and ensure folder exists
                    var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var uploads = Path.Combine(webRoot, "concert-photos");
                    Directory.CreateDirectory(uploads);

                    // Use Path.Combine to get the file path to save file to
                    //string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "concert-photos", filename);

                    // Use Path.Combine to get the file path to save file to
                    string saveFilePath = Path.Combine(uploads, filename);

                    // Save file
                    using (var fileStream = new FileStream(saveFilePath, FileMode.Create))
                    {
                        await concert.FormFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    // Preserve the existing filename if no new file is uploaded
                    concert.Filename = existingConcert.Filename;
                }
                

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
                return RedirectToAction("Index", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", concert.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerName", concert.OwnerId);
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
                // Delete the photo file if it exists
                if (!string.IsNullOrEmpty(concert.Filename))
                {
                    // Use IWebHostEnvironment.WebRootPath
                    var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var uploads = Path.Combine(webRoot, "concert-photos");
                    var photoPath = Path.Combine(uploads, concert.Filename);

                    // Used before IWebHostEnvironment
                    //var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "concert-photos", concert.Filename);
                    if (System.IO.File.Exists(photoPath))
                    {
                        System.IO.File.Delete(photoPath);
                    }
                }
                _context.Concert.Remove(concert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); ;
        }

        private bool ConcertExists(int id)
        {
            return _context.Concert.Any(e => e.ConcertId == id);
        }
    }
}
