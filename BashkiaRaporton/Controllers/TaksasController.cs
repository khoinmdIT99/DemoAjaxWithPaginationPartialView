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
    public class TaksasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaksasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Taksas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taksas.ToListAsync());
        }
        public async Task<IActionResult> TaksaPartail(string search, int pageNumber = 1, int pageSize = 7)
        {

            int ExecuteRecords = (pageNumber * pageSize) - pageSize;

            var pagination = new PagedResult<Taksa>
            {

                Data = await _context.Taksas.Where(E =>
                                              E.Pershkrimin.Contains(search ?? ""))                                             
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems =  _context.Taksas.Where(E => E.Pershkrimin.Contains(search ?? "")).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return PartialView("IndexPartial", pagination);
        }
        // GET: Taksas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksa = await _context.Taksas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taksa == null)
            {
                return NotFound();
            }

            return View(taksa);
        }

        // GET: Taksas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taksas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     
        public async Task<IActionResult> CreateTaksa(Taksa taksa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taksa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok();
        }

        // GET: Taksas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksa = await _context.Taksas.FindAsync(id);
            if (taksa == null)
            {
                return NotFound();
            }
            return PartialView("EditPartial",taksa);
        }

       
        public async Task<IActionResult> EditTaksa([Bind("Id,Pershkrimin")] Taksa taksa)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taksa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaksaExists(taksa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("IndexPartial", taksa);
            }
            return PartialView("IndexPartial", taksa);
        }

        // GET: Taksas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taksa = await _context.Taksas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taksa == null)
            {
                return NotFound();
            }

            return View(taksa);
        }

        // POST: Taksas/Delete/5
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taksa = await _context.Taksas.FindAsync(id);
            _context.Taksas.Remove(taksa);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool TaksaExists(int id)
        {
            return _context.Taksas.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> EditDisplayOrderProcessesStatuses(string idList)
        {

            List<string> ids = new List<string>(idList.Split(','));
            List<int> idsInt = ids.Select(int.Parse).ToList();
            int index = 1;
            foreach (var id in idsInt)
            {
                await _context.Taksas.Where(p => p.Id == id).ForEachAsync(p => p.Renditja = index);
                index++;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
