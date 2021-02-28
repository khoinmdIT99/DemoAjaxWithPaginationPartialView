using BashkiaRaporton.Data;
using BashkiaRaporton.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BashkiaRaporton.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View(_context.Bashkia.FirstOrDefault());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            double shumajanar = 0.0;
            double shumashkurt = 0.0;
            double shumamars = 0.0;
            double shumaprill = 0.0;
            double shumamaj = 0.0;
            double shumaqershor = 0.0;
            double shumakorrik = 0.0;
            double shumagusht = 0.0;
            double shumashtator = 0.0;
            double shumatetor = 0.0;
            double shumanentor = 0.0;
            double shumadhjethor = 0.0;
            var shumaetaksave = 0;
            var bashkia = _context.Bashkia.FirstOrDefault();
           
            int TaksaBujqesi = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaBujqesi.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaTraulli = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaeTruallit.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaNdertesa = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaNdertesa.ToString())).Select(s => s.Vlera).FirstOrDefault();
            int TaksaUjit = _context.TaksaVlera.Include(t => t.Taksa).Where(t => t.Taksa.Pershkrimin.Contains(TipiTakses.TaksaeUjit.ToString())).Select(s => s.Vlera).FirstOrDefault();
            var TaksaVlera = _context.TaksaVlera.Include(c => c.Taksa).ToList();
            var minus = TaksaBujqesi + TaksaTraulli + TaksaNdertesa + TaksaUjit;
            var pronajanar = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 1);
            var pronashkurt = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 2);
            var pronamars = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 3);
            var pronaprill = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 4);
            var pronamaj = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 5);
            var pronaqershor = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 6);
            var pronakorrik = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 7);
            var pronagusht = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 8);
            var pronashtator = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 9);
            var pronatetor = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 10);
            var pronanentor = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 11);
            var pronadhjetor = _context.Prona.Include(b => b.Banore).Where(a => a.Banore.Detyrime == true && a.Banore.Data.Month == 12);
            foreach (var t in TaksaVlera)
            {
                shumaetaksave = shumaetaksave + t.Vlera;
            }
            foreach (var p in pronajanar)
            {
                shumajanar = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }

            foreach (var p in pronashkurt)
            {
                shumashkurt = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }

            foreach (var p in pronamars)
            {
                shumamars = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronaprill)
            {
                shumaprill = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronamaj)
            {
                shumamaj = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var t in TaksaVlera)
            {
                shumaetaksave = shumaetaksave + t.Vlera;
            }
            foreach (var p in pronaqershor)
            {
                shumaqershor = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }

            foreach (var p in pronakorrik)
            {
                shumakorrik = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }

            foreach (var p in pronagusht)
            {
                shumagusht = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronashtator)
            {
                shumashtator = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronatetor)
            {
                shumatetor = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronanentor)
            {
                shumanentor = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }
            foreach (var p in pronadhjetor)
            {
                shumadhjethor = (p.Ndertesa * TaksaTraulli) + (p.Titulli * TaksaTraulli) + (p.TokaBujqesore * TaksaBujqesi) - (minus);
            }

            Dashboard dashboard = new Dashboard
            {
                Bashkia = bashkia,
                njesiaAdministratives = _context.njesiaAdministratives.ToList(),
                Banores = _context.Banore.Where(s => s.Detyrime == false).ToList(),
                Janar = shumajanar,
                Shkurt = shumashkurt,
                Mars= shumamars,
                Prill = shumaprill,
                Maj = shumamars,
                Qershor = shumaqershor,
                Korrik = shumakorrik,
                Gusht = shumagusht,
                Shtator= shumashtator,
                Tetor = shumatetor,
                Nentor = shumanentor,
                Dhjetor = shumadhjethor

               
              


            };
            return View(dashboard);
        }


    }
}
