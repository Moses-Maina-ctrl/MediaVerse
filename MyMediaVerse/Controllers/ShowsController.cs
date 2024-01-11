using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMediaVerse.Data;
using MyMediaVerse.Models;

namespace MyMediaVerse.Controllers
{
    public class ShowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowsController(ApplicationDbContext context)
        {
            _context = context;
        }
        List<string> ShowStatus = new List<string>
        {
            "Watching",
            "Still Watching",
            "Not yet Watched"
        };
        // GET: Shows
        public async Task<IActionResult> Index(string sortOrder)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";
            var show = from s in _context.Shows
                       where s.OwnerUserID == UserId    
                      select s;
            switch (sortOrder)
            {
                case "title_desc":
                    show = show.OrderByDescending(show => show.Title);
                    break;
                case "genre":
                    show = show.OrderBy(show => show.Genre);
                    break;
                case "genre_desc":
                    show =show.OrderByDescending(show=> show.Genre); 
                    break;
                default:
                    show = show.OrderBy(show => show.Title);
                    break;
            }
            return View(await show.AsNoTracking().ToListAsync());
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shows == null)
            {
                return NotFound();
            }

            return View(shows);
        }

        // GET: Shows/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(ShowStatus);
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Status")] Shows shows)
        {
            if (ModelState.IsValid)
            {
                shows.OwnerUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(shows);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shows);
        }

        // GET: Shows/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows.FindAsync(id);
            if (shows == null || shows.OwnerUserID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            return View(shows);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Status")] Shows shows)
        {
            if (id != shows.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowsExists(shows.Id))
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
            return View(shows);
        }

        // GET: Shows/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shows == null)
            {
                return NotFound();
            }

            return View(shows);
        }

        // POST: Shows/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shows = await _context.Shows.FindAsync(id);
            if (shows != null)
            {
                _context.Shows.Remove(shows);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowsExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
    }
}
