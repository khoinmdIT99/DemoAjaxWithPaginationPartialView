using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.Models;

namespace BashkiaRaporton.ViewModel
{
    public class FaturaUjitViewModel
    {
        public List<Fatura> Faturas { get; set; }
        public int TaksaUjit { get; set; }
    }
}
