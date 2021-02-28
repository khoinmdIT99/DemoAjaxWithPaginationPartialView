using BashkiaRaporton.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BashkiaRaporton.Models;
using cloudscribe.Pagination.Models;
using BashkiaRaporton.ViewModel;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BashkiaRaporton.Controllers
{
    [Authorize]
    public class BanoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> manager;

        
        public BanoreController(UserManager<IdentityUser> userManager, ApplicationDbContext context,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            manager = roleManager;
            
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> BanorePartial(string search, int pageNumber = 1, int pageSize = 7)
        {

            int ExecuteRecords = (pageNumber * pageSize) - pageSize;

            var pagination = new PagedResult<Banore>
            {

                Data = await _context.Banore
                                             .Include(s => s.Status)
                                             .Include(a => a.NjesiaAdministrative)
                                             .Where(E =>
                                              E.Emri.Contains(search ?? "")
                                              || E.Mbiemri.Contains(search ?? ""))
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.Banore
                                            .Include(s => s.Status)
                                            .Include(a => a.NjesiaAdministrative)
                                            .Where(E => E.EmailConfirmed == true
                                            || E.EmailConfirmed == false)
                                            .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Role = new SelectList(_context.Roles, "Id", "Name");
            return PartialView("BanorePartial", pagination);
        }
      

        public IActionResult Details(string banoreid)
        {
            var banore = _context.Banore
                                        .Include(s => s.Status)
                                        .Include(a => a.NjesiaAdministrative).Where(B => B.Id == banoreid).FirstOrDefault();
            return View(banore);
        }
        public IActionResult Delete(string banoreid)
        {
            var banore = _context.Banore.Where(b => b.Id == banoreid).FirstOrDefault();
            var pronesia = _context.Prona.Where(b => b.BanoreId == banoreid).ToList();


            if (banore != null)
            {
                foreach (var p in pronesia)
                {
                    _context.Remove(p);
                }
                _context.Remove(banore);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Edit(string banoreid)
        {
            var banore = _context.Banore.Where(B => B.Id == banoreid).FirstOrDefault();
            return PartialView("EditBanore", banore);
        }
        public IActionResult Banori(string banoreid)
        {
            var banore = _context.Banore.Where(B => B.Id == banoreid).FirstOrDefault();
            return PartialView(banore);
        }
        public async Task<IActionResult> Profili()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var banore = _context.Prona.Include(s => s.Banore)
                                             .ThenInclude(a => a.NjesiaAdministrative)

                                             .Where(B => B.Banore.Id == user.Id)
                                             .FirstOrDefault();
            return View(banore);
        }

        public async Task<IActionResult> FaturaUjit()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var faturaujit = _context.Fatura.Include(s => s.Banore)

                                             .Where(B => B.Banore.Id == user.Id).OrderBy(s => s.Date)
                                             .ToList();

            FaturaUjitViewModel faturaUjitView = new FaturaUjitViewModel()
            {
                Faturas = faturaujit,
                TaksaUjit = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaeUjit.ToString())).Select(s => s.Vlera).FirstOrDefault()

            };
            return View(faturaUjitView);

        }
        public async Task<IActionResult> Detyrime()
        {
            double shuma;
          

            var shumaetaksave = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var prona = _context.Prona.Include(s => s.Banore)

                                             .Where(B => B.Banore.Id == user.Id && B.Banore.Detyrime == false )
                                             .FirstOrDefault();
            int TaksaBujqesi = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaBujqesi.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaTraulli = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaeTruallit.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaNdertesa = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaNdertesa.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaUjit = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaeUjit.ToString())).Select(s => s.Vlera).FirstOrDefault();
            var TaksaVlera = _context.TaksaVlera.Include(c => c.Taksa).ToList();
            var minus = TaksaBujqesi + TaksaTraulli + TaksaNdertesa + TaksaUjit;
           
            
            

            if (prona != null)
            {
                shuma = (prona.Ndertesa * TaksaTraulli) + (prona.Titulli * TaksaTraulli) + (prona.TokaBujqesore * TaksaBujqesi)+ (shumaetaksave) - (minus);
            }
            else
                shuma = 0.0;
            
            
            foreach(var t in TaksaVlera)
            {
                shuma += t.Vlera;
            }
            Detyrime detyrime = new Detyrime()
            {
                Pronesia = prona,
                TaksaBujqesi = TaksaBujqesi,
                TaksaTraulli = TaksaTraulli,
                TaksaNdertesa = TaksaNdertesa,
                TaksaVlera = TaksaVlera,
                Totali = shuma


            };
            return View(detyrime);

        }
      

            public IActionResult Save(Banore banore)
        {
            var banoreindb = _context.Banore.First(D => D.Id == banore.Id);
            if(banoreindb != null)
            {
                banoreindb.Id = banore.Id;               
                banoreindb.Emri = banore.Emri;
                banoreindb.Mbiemri = banore.Mbiemri;
                banoreindb.Email = banore.Email;
                banoreindb.PhoneNumber = banore.PhoneNumber;
            };
           
           
            _context.SaveChanges();
            return PartialView("Banori",banore);
        }
        public async Task<IActionResult> UpdateDetyrime(string id, bool @checked)
        {
            var banore = await _context.Banore.FindAsync(id);
            if (@checked == true)
            {

                banore.Detyrime = true;
                banore.Data = DateTime.Now;
            }
            else
            {
                banore.Detyrime = false;
                

            }

            _context.SaveChanges();
            return Ok();
        }
        public IActionResult Role()
        {
            return View();
        }
        public async  Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = roleViewModel.RoleName
        };
            IdentityResult identityResult = await manager.CreateAsync(identityRole);
            return Ok();
        }
        public IActionResult Njoftime()
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
                                              E.Banore.Emri.Contains(search ?? "") && E.BanoreId ==user.Id )
                                             .Skip(ExecuteRecords)
                                             .Take(pageSize)
                                             .ToListAsync(),
                TotalItems = _context.Njoftime.Include(s => s.Banore).Where(E => E.Banore.Emri.Contains(search ?? "")).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return PartialView("NjoftimePartial" , pagination);
        }

        public async Task<IActionResult> AddUserRole(UserRoleViewModel userRoleView)
        {
            IdentityUser identityUser = await _userManager.FindByIdAsync(userRoleView.UserId);
            IdentityRole identityRole = await manager.FindByIdAsync(userRoleView.RoleId);
            var result1 = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
            return Ok();
        }


    }
}
