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
    public class PronesiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PronesiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pronesia
        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> IndexPartail(string search, int pageNumber = 1, int pageSize = 7)
        {
            int ExecuteRecords = (pageNumber * pageSize) - pageSize;
            var pagination = new PagedResult<Pronesia>
            {

                Data = await _context.Prona
                                            .Include(s => s.Banore)                                             
                                             .Where(E =>
                                              E.Banore.Emri.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.Prona.Include(s => s.Banore).Where(E =>E.Banore.Emri.Contains(search ?? "")).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

           
            return PartialView("IndexPartail", pagination);
        }



        // GET: Pronesia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronesia = await _context.Prona
                .Include(p => p.Banore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pronesia == null)
            {
                return NotFound();
            }

            return View(pronesia);
        }

        // GET: Pronesia/Create
        public IActionResult Create()
        {
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Emri");
            return View();
        }

        // POST: Pronesia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ndertesa,TokaBujqesore,Titulli,BanoreId")] Pronesia pronesia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pronesia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Id", pronesia.BanoreId);
            return View(pronesia);
        }

        // GET: Pronesia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronesia = await _context.Prona.FindAsync(id);
            if (pronesia == null)
            {
                return NotFound();
            }
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Emri", pronesia.BanoreId);
            return View(pronesia);
        }

        // POST: Pronesia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ndertesa,TokaBujqesore,Titulli,BanoreId")] Pronesia pronesia)
        {
            if (id != pronesia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pronesia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PronesiaExists(pronesia.Id))
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
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Id", pronesia.BanoreId);
            return View(pronesia);
        }

        // GET: Pronesia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronesia = await _context.Prona
                .Include(p => p.Banore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pronesia == null)
            {
                return NotFound();
            }

            return View(pronesia);
        }

        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pronesia = await _context.Prona.FindAsync(id);
            _context.Prona.Remove(pronesia);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool PronesiaExists(int id)
        {
            return _context.Prona.Any(e => e.Id == id);
        }
    }
}
