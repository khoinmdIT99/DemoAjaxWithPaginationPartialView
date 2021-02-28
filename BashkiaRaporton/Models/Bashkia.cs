using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Bashkia
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string  EmriKryetarit { get; set; }
        public string Posta { get; set; }
        public int Popullsia { get; set; }
        public decimal Siperfaqja { get; set; }
        public string Pershkrimi { get; set; }
    }
}
