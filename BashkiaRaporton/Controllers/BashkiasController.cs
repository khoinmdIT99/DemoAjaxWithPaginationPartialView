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
using Microsoft.AspNetCore.Authorization;

namespace BashkiaRaporton.Controllers
{
   [Authorize(Roles = "Administrator,Admin")]
    public class BashkiasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Banore> _userManager;

        public BashkiasController(UserManager<Banore> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bashkias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bashkia.FirstOrDefaultAsync());
        }

        // GET: Bashkias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bashkia = await _context.Bashkia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bashkia == null)
            {
                return NotFound();
            }

            return View(bashkia);
        }

        // GET: Bashkias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bashkias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri,EmriKryetarit,Posta,Popullsia,Siperfaqja,Pershkrimi")] Bashkia bashkia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bashkia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bashkia);
        }

        // GET: Bashkias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bashkia = await _context.Bashkia.FindAsync(id);
            if (bashkia == null)
            {
                return NotFound();
            }
            return View(bashkia);
        }

        // POST: Bashkias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,EmriKryetarit,Posta,Popullsia,Siperfaqja,Pershkrimi")] Bashkia bashkia)
        {
            if (id != bashkia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bashkia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BashkiaExists(bashkia.Id))
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
            return View(bashkia);
        }

        // GET: Bashkias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bashkia = await _context.Bashkia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bashkia == null)
            {
                return NotFound();
            }

            return View(bashkia);
        }

        // POST: Bashkias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bashkia = await _context.Bashkia.FindAsync(id);
            _context.Bashkia.Remove(bashkia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BashkiaExists(int id)
        {
            return _context.Bashkia.Any(e => e.Id == id);
        }

       
    }
}
