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
    public class NjesiaAdministrativesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NjesiaAdministrativesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NjesiaAdministratives
        public IActionResult Index()
        {


            return View();
        }
        public async Task<IActionResult> NjesiPartial(string search, int pageNumber = 1, int pageSize = 7)
        {

            int ExecuteRecords = (pageNumber * pageSize) - pageSize;

            var pagination = new PagedResult<NjesiaAdministrative>
            {

                Data = await _context.njesiaAdministratives
                                             .Include(s => s.Bashkia)
                                            
                                             .Where(E =>
                                              E.Bashkia.Emri.Contains(search ?? "")
                                              || E.Emri.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.njesiaAdministratives
                                             .Include(s => s.Bashkia)
                                             .Where(E =>
                                              E.Bashkia.Emri.Contains(search ?? "")
                                              || E.Emri.Contains(search ?? ""))
                                            .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return PartialView("NjesiPartial", pagination);
        }
        // GET: NjesiaAdministratives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njesiaAdministrative = await _context.njesiaAdministratives
                .Include(n => n.Bashkia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (njesiaAdministrative == null)
            {
                return NotFound();
            }

            return View(njesiaAdministrative);
        }

        // GET: NjesiaAdministratives/Create
        public IActionResult Create()
        {
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Emri");
            return View();
        }

       
        public async Task<IActionResult> CreateNjesi(NjesiaAdministrative njesiaAdministrative)
        {
            int bashkiid = await _context.Bashkia.Select(b => b.Id).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                njesiaAdministrative.BashkiaId = bashkiid;
                _context.Add(njesiaAdministrative);
                await _context.SaveChangesAsync();
                return Ok();
            }
            
            return Ok();
        }

        // GET: NjesiaAdministratives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njesiaAdministrative = await _context.njesiaAdministratives.Include(b=>b.Bashkia).Where(p=>p.Id == id).FirstOrDefaultAsync();
            if (njesiaAdministrative == null)
            {
                return NotFound();
            }
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Emri", njesiaAdministrative.BashkiaId);
            return View(njesiaAdministrative);
        }

       
        public async Task<IActionResult> EditNjesi( NjesiaAdministrative njesiaAdministrative)
        {
            
           
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(njesiaAdministrative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NjesiaAdministrativeExists(njesiaAdministrative.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var njesia1 = _context.njesiaAdministratives.Include(b => b.Bashkia).Where(s => s.Id == njesiaAdministrative.Id).FirstOrDefault();
                return PartialView("IndexPartail", njesia1);
            }
            ViewData["BashkiaId"] = new SelectList(_context.Bashkia, "Id", "Emri", njesiaAdministrative.BashkiaId);
            var njesia = _context.njesiaAdministratives.Include(b => b.Bashkia).Where(s => s.Id == njesiaAdministrative.Id).FirstOrDefault();
            return PartialView("IndexPartail", njesia);
        }

        // GET: NjesiaAdministratives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njesiaAdministrative = await _context.njesiaAdministratives
                .Include(n => n.Bashkia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (njesiaAdministrative == null)
            {
                return NotFound();
            }

            return View(njesiaAdministrative);
        }

        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var njesiaAdministrative = await _context.njesiaAdministratives.FindAsync(id);
            _context.njesiaAdministratives.Remove(njesiaAdministrative);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool NjesiaAdministrativeExists(int id)
        {
            return _context.njesiaAdministratives.Any(e => e.Id == id);
        }
    }
}
