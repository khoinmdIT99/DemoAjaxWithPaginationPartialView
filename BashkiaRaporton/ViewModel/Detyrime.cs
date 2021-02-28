using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.Models;

namespace BashkiaRaporton.ViewModel
{
    public class Detyrime 
    {
        public Pronesia Pronesia { get; set; }
        public int TaksaBujqesi { get; set; }
        public int TaksaTraulli { get; set; }
        public int TaksaNdertesa { get; set; }

        public List<TaksaVlera> TaksaVlera { get; set; }
        public double Totali { get; set; }
        

    }
}
