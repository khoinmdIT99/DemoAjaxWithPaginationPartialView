using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    enum TipiTakses
    {
        TaksaeUjit,
        TaksaeTruallit,
        TaksaBujqesi,
        TaksaNdertesa
       
    }
    public class Taksa
    {
        public int Id { get; set; }
        public string  Pershkrimin { get; set; }
        public int Renditja { get; set; }

    }
}
