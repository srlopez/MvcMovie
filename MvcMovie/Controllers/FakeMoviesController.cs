using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class FakeMoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public FakeMoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: FakeMovies
        public async Task<IActionResult> Index()
        {
            return View(await _context.FakeMovie.ToListAsync());
        }

        // GET: FakeMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeMovie = await _context.FakeMovie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakeMovie == null)
            {
                return NotFound();
            }

            return View(fakeMovie);
        }

        // GET: FakeMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FakeMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Price,Genre,Rating")] FakeMovie fakeMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fakeMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fakeMovie);
        }

        // GET: FakeMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeMovie = await _context.FakeMovie.FindAsync(id);
            if (fakeMovie == null)
            {
                return NotFound();
            }
            return View(fakeMovie);
        }

        // POST: FakeMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Price,Genre,Rating")] FakeMovie fakeMovie)
        {
            if (id != fakeMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fakeMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FakeMovieExists(fakeMovie.Id))
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
            return View(fakeMovie);
        }

        // GET: FakeMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeMovie = await _context.FakeMovie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakeMovie == null)
            {
                return NotFound();
            }

            return View(fakeMovie);
        }

        // POST: FakeMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fakeMovie = await _context.FakeMovie.FindAsync(id);
            _context.FakeMovie.Remove(fakeMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FakeMovieExists(int id)
        {
            return _context.FakeMovie.Any(e => e.Id == id);
        }
    }
}
