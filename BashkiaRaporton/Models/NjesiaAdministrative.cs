using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class NjesiaAdministrative
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public int BashkiaId { get; set; }
        public Bashkia Bashkia { get; set; }
    }
}
