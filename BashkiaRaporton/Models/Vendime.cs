using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Vendime
    {
        public int Id { get; set; }
        public string Titulli { get; set; }
        public string Pershkrimi { get; set; }
        public DateTime  Data { get; set; }
        public int BashkiaId { get; set; }
        public Bashkia Bashkia { get; set; }
    }
}
