using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.Models;

namespace BashkiaRaporton.Models
{
    public class TaksaVlera
    {
        public int Id { get; set; }
        public int Vlera { get; set; }
        public int BashkiaId { get; set; }
        public Bashkia Bashkia { get; set; }

        public int TaksaId { get; set; }
       public Taksa Taksa { get; set; }
    }
}
