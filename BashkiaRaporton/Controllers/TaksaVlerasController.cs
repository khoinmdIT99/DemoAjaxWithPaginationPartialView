using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BashkiaRaporton.Data;
using BashkiaRaporton.Models;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace BashkiaRaporton.Controllers
{
    [Authorize]
    public class TaksaVlerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaksaVlerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaksaVleras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaksaVlera.Include(t => t.Bashkia).Include(t => t.Taksa);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexPartail(string search, int pageNumber = 1, int pageSize = 7)
        {
            int ExecuteRecords = (pageNumber * pageSize) - pageSize;

            var pagination = new PagedResult<TaksaVlera>
            {

                Data = await _context.TaksaVlera
                                             .Include(s => s.Bashkia)
                                             .Include(a => a.Taksa)
                                             .Where(E =>
                                              E.Bashkia.Emri.Contains(search ?? "")
                                              || E.Taksa.Pershkrimin.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems =  _context.TaksaVlera
                                             .Include(s => s.Bashkia)
                                             .Include(a => a.Taksa)
                                             .Where(E =>
                                              E.Bashkia.Emri.Contains(search ?? "")
                                              || E.Taksa.Pershkrimin.Contains(search ?? ""))
                                            .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return PartialView("IndexPartail", pagination);
        }


        // GET: TaksaVleras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksaVlera = await _context.TaksaVlera
                .Include(t => t.Bashkia)
                .Include(t => t.Taksa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taksaVlera == null)
            {
                return NotFound();
            }

            return View(taksaVlera);
        }

        // GET: TaksaVleras/Create
        public IActionResult Create()
        {
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Emri");
            ViewData["TaksaId"] = new SelectList(_context.Taksas, "Id", "Pershkrimin");
            return View();
        }

        // POST: TaksaVleras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Vlera,BashkiaId,TaksaId")] TaksaVlera taksaVlera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taksaVlera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Id", taksaVlera.BashkiaId);
            ViewData["TaksaId"] = new SelectList(_context.Taksas, "Id", "Id", taksaVlera.TaksaId);
            return View(taksaVlera);
        }

        // GET: TaksaVleras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksaVlera = await _context.TaksaVlera.FindAsync(id);
            if (taksaVlera == null)
            {
                return NotFound();
            }
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Emri", taksaVlera.BashkiaId);
            ViewData["TaksaId"] = new SelectList(_context.Taksas, "Id", "Pershkrimin", taksaVlera.TaksaId);
            return View(taksaVlera);
        }

        // POST: TaksaVleras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Vlera,BashkiaId,TaksaId")] TaksaVlera taksaVlera)
        {
            if (id != taksaVlera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taksaVlera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaksaVleraExists(taksaVlera.Id))
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
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Id", taksaVlera.BashkiaId);
            ViewData["TaksaId"] = new SelectList(_context.Taksas, "Id", "Id", taksaVlera.TaksaId);
            return View(taksaVlera);
        }

        // GET: TaksaVleras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksaVlera = await _context.TaksaVlera
                .Include(t => t.Bashkia)
                .Include(t => t.Taksa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taksaVlera == null)
            {
                return NotFound();
            }

            return View(taksaVlera);
        }

        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taksaVlera = await _context.TaksaVlera.FindAsync(id);
            _context.TaksaVlera.Remove(taksaVlera);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool TaksaVleraExists(int id)
        {
            return _context.TaksaVlera.Any(e => e.Id == id);
        }
    }
}
