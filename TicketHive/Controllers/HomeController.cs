using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using TicketHive.Data;
using TicketHive.Models;
using Microsoft.EntityFrameworkCore;


namespace TicketHive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TicketHiveContext _context;

        public HomeController(ILogger<HomeController> logger, TicketHiveContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        // GET: concerts
        public async Task<IActionResult> Index()
        {
            // Get all concerts
            var concerts = await _context.Concert
                .OrderByDescending(m => m.PublishDate)
                .ToListAsync();

            return View(concerts);
        }

        // GET: concerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // URL is missing the 3rd parameter ID
            if (id == null)
            {
                return NotFound();
            }

            // Get record where PK = id
            var concert = await _context.Concert.FirstOrDefaultAsync(m => m.ConcertId == id);

            // Record not found in the database
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }
    }
}
