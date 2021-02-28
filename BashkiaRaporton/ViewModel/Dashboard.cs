using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.Models;
namespace BashkiaRaporton.ViewModel
{
    public class Dashboard
    {
        public Bashkia Bashkia { get; set; }
        public List<NjesiaAdministrative> njesiaAdministratives { get; set; }
        public List<Banore> Banores { get; set; }
        public double Janar { get; set; }
        public double Shkurt { get; set; }
        public double Mars { get; set; }

        public double Prill { get; set; }
        public double Maj { get; set; }
        public double Qershor { get; set; }
        public double Korrik { get; set; }

        public double Gusht { get; set; }
        public double Shtator { get; set; }
        public double Tetor { get; set; }
        public double Nentor { get; set; }
        public double Dhjetor { get; set; }

    
    }
}
