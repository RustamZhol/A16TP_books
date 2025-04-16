using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_A16Book_Store.Context;
using TP_A16Book_Store.Models;

namespace TP_A16Book_Store.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string sortOrder, string searchTerm, string filterColumn, string filterValue)
        {
            IQueryable<Books> booksQuery = _context.Books;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchTerm = searchTerm;
            ViewBag.CurrentFilterColumn = filterColumn;
            ViewBag.CurrentFilterValue = filterValue;

            // Filter
            if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterValue))
            {
                booksQuery = _context.Books.FromSqlRaw("EXECUTE dbo.FilterBooks @FilterColumn = {0}, @FilterValue = {1}", filterColumn, filterValue);
            }

            // Search 
            if (!string.IsNullOrEmpty(searchTerm))
            {
                booksQuery = _context.Books.FromSqlRaw("EXECUTE dbo.SearchBooks @SearchTerm = {0}", searchTerm);
            }

            // Sort
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "TitleAsc":
                        booksQuery = booksQuery.OrderBy(b => b.Title);
                        break;
                    case "TitleDesc":
                        booksQuery = booksQuery.OrderByDescending(b => b.Title);
                        break;
                    case "AuthorAsc":
                        booksQuery = booksQuery.OrderBy(b => b.Author);
                        break;
                    case "AuthorDesc":
                        booksQuery = booksQuery.OrderByDescending(b => b.Author);
                        break;
                    case "YearAsc":
                        booksQuery = booksQuery.OrderBy(b => b.Year);
                        break;
                    case "YearDesc":
                        booksQuery = booksQuery.OrderByDescending(b => b.Year);
                        break;
                    case "PriceAsc":
                        booksQuery = booksQuery.OrderBy(b => b.Price);
                        break;
                    case "PriceDesc":
                        booksQuery = booksQuery.OrderByDescending(b => b.Price);
                        break;
                    case "IdAsc": 
                        booksQuery = booksQuery.OrderBy(b => b.Id);
                        break;
                    default:
                        booksQuery = booksQuery.OrderBy(b => b.Id); 
                        break;
                }
            }
            else
            {
                booksQuery = booksQuery.OrderBy(b => b.Id); 
            }

            return View(await booksQuery.ToListAsync());
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            var books = await _context.Books
                .FromSqlRaw("EXECUTE dbo.SearchBooks @SearchTerm = {0}", searchTerm)
                .ToListAsync();

            ViewBag.CurrentSearchTerm = searchTerm;
            return View("Index", books); 
        }



        // GET: Books/Details/5
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

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Year,Genre,Type,Price,Description,ImageUrl,Quantity")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Year,Genre,Type,Price,Description,ImageUrl,Quantity")] Books books)
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
