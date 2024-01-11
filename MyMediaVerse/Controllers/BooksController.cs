using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMediaVerse.Data;
using MyMediaVerse.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyMediaVerse.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        List<string> BookStatus = new List<string>
        {
            "Finished",
            "Still Reading",
            "Not Read"
        };
        // GET: Books
        public async Task<IActionResult> Index(string sortOrder)

        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "author"? "author_desc" : "author";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";

            var book = from s in _context.Books
                        where s.OwnerUserID == userId
                        select s;
            switch (sortOrder)
            {
                case "title_desc":
                    book = book.OrderByDescending(book => book.Title); 
                    break;
                case "author":
                    book = book.OrderBy(book => book.Author);
                    break;
                case "author_desc":
                    book = book.OrderByDescending(book=> book.Author);
                    break;
                case "genre":
                    book = book.OrderBy(book => book.Genre);
                    break;
                case "genre_desc":
                    book = book.OrderByDescending(book=> book.Genre);
                    break;
                default:
                    book = book.OrderBy(book=> book.Title);
                    break;

            }
            return View(await book.AsNoTracking().ToListAsync());
        }

        // GET: Books/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        [Authorize]

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(BookStatus);
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,Status")] Books books)
        {
            if (ModelState.IsValid)
            {
                books.OwnerUserID = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null || books.OwnerUserID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,Status")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
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
            return View(books);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            if (books != null)
            {
                _context.Books.Remove(books);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
