using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BashkiaRaporton.Data;
using BashkiaRaporton.Models;
using Microsoft.AspNetCore.Identity;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace BashkiaRaporton.Controllers
{
    [Authorize]
    public class NjoftimeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NjoftimeController(ApplicationDbContext context ,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Njoftime
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> NjoftimePartial(string search, int pageNumber = 1, int pageSize = 7)
        {
            int ExecuteRecords = (pageNumber * pageSize) - pageSize;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pagination = new PagedResult<Njoftime>
            {

                Data = await _context.Njoftime
                                            .Include(s => s.Banore)
                                             .Where(E =>
                                              E.Banore.Emri.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.Njoftime.Include(s => s.Banore).Where(E => E.Banore.Emri.Contains(search ?? "")).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return PartialView("NjoftimePartial", pagination);
        }


        // GET: Njoftime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njoftime = await _context.Njoftime
                .Include(n => n.Banore)
                .FirstOrDefaultAsync(m => m.NjoftimeId == id);
            if (njoftime == null)
            {
                return NotFound();
            }

            return View(njoftime);
        }

        // GET: Njoftime/Create
        public IActionResult Create(string banoreid)
        {
            ViewData["BanoreId"] = banoreid;
            return View();
        }

        // POST: Njoftime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        public async Task<IActionResult> CreateNJoftime(Njoftime njoftime)
        {
            if (ModelState.IsValid)
            {
                
                njoftime.DataDergimit = DateTime.Now;
                njoftime.Statusi = false;
                _context.Add(njoftime);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        // GET: Njoftime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njoftime = await _context.Njoftime.FindAsync(id);
            if (njoftime == null)
            {
                return NotFound();
            }
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Id", njoftime.BanoreId);
            return View(njoftime);
        }

        // POST: Njoftime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NjoftimeId,BanoreId,Mesazhi,DataDergimit,Shikushmeria")] Njoftime njoftime)
        {
            if (id != njoftime.NjoftimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(njoftime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NjoftimeExists(njoftime.NjoftimeId))
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
            ViewData["BanoreId"] = new SelectList(_context.Banore, "Id", "Id", njoftime.BanoreId);
            return View(njoftime);
        }

        // GET: Njoftime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var njoftime = await _context.Njoftime
                .Include(n => n.Banore)
                .FirstOrDefaultAsync(m => m.NjoftimeId == id);
            if (njoftime == null)
            {
                return NotFound();
            }

            return View(njoftime);
        }

        // POST: Njoftime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var njoftime = await _context.Njoftime.FindAsync(id);
            _context.Njoftime.Remove(njoftime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NjoftimeExists(int id)
        {
            return _context.Njoftime.Any(e => e.NjoftimeId == id);
        }

        public async Task<IActionResult> UpdateNjoftime(int id, bool @checked)
        {
            var njoftime = await _context.Njoftime.FindAsync(id);
            if (@checked == true)
            {
                njoftime.Shikushmeria = true;
                njoftime.Statusi = true;
            }
            else
            {
                njoftime.Shikushmeria = false;

            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
