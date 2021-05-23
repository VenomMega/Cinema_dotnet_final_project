using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imdb.Data;
using Imdb.Models;

namespace Imdb.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ImdbContext _context;

        public MoviesController(ImdbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder,
           string currentFilter,
           string searchString,
           int? pageNumber)
        {
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var movie = from s in _context.Movie
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                movie = movie.Where(s => s.MovieTitle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "code_desc":
                    movie = movie.OrderByDescending(s => s.MovieTitle);
                    break;
                default:
                    movie = movie.OrderBy(s => s.MovieTitle);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Movie>.CreateAsync(movie.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Company)
                .Include(m => m.Language)
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id");
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id");
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieTitle,LanguageId,Prod_year,MovieDescription,ProducerId,CompanyId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", movie.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movie.LanguageId);
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", movie.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movie.LanguageId);
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,LanguageId,Prod_year,MovieDescription,ProducerId,CompanyId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", movie.CompanyId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movie.LanguageId);
            ViewData["ProducerId"] = new SelectList(_context.Producer, "Id", "Id", movie.ProducerId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Company)
                .Include(m => m.Language)
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
