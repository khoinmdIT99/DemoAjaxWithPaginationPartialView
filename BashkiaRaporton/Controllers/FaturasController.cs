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
    public class FaturasController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public FaturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Faturas
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> IndexPartail(string search, int pageNumber = 1, int pageSize = 7)
        {
            int ExecuteRecords = (pageNumber * pageSize) - pageSize;
            var pagination = new PagedResult<Fatura>
            {

                Data = await _context.Fatura
                                            .Include(s => s.Banore)
                                             .Where(E =>
                                              E.Banore.Emri.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.Fatura.Include(s => s.Banore).Where(E => E.Banore.Emri.Contains(search ?? "")).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return PartialView("IndexPartail", pagination);
        }
        // GET: Faturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Fatura
                .Include(f => f.Banore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fatura == null)
            {
                return NotFound();
            }

            return View(fatura);
        }

        // GET: Faturas/Create
        public IActionResult Create()
        {
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Emri");
            return View();
        }

        // POST: Faturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                fatura.Paguar = false;
                _context.Add(fatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Id", fatura.BanoreId);
            return View(fatura);
        }

        // GET: Faturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Fatura.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Emri", fatura.BanoreId);
            return View(fatura);
        }

        // POST: Faturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sasia,BanoreId")] Fatura fatura)
        {
            if (id != fatura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturaExists(fatura.Id))
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
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Emri", fatura.BanoreId);
            return View(fatura);
        }

        // GET: Faturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Fatura
                .Include(f => f.Banore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fatura == null)
            {
                return NotFound();
            }

            return View(fatura);
        }

       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fatura = await _context.Fatura.FindAsync(id);
            _context.Fatura.Remove(fatura);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> UpdatePagesa(int id ,bool @checked)
        {
            var fatura = await _context.Fatura.FindAsync(id);
            if (@checked ==  true)
            {
               
                fatura.Paguar = true;
            }
            else
            {
                fatura.Paguar = false;
            }
            
            _context.SaveChanges();
            return Ok();
        }

        private bool FaturaExists(int id)
        {
            return _context.Fatura.Any(e => e.Id == id);
        }
    }
}
