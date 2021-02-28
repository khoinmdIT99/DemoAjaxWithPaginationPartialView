using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Njoftime
    {
        public int NjoftimeId { get; set; }
        public string BanoreId { get; set; }
        [ForeignKey("BanoreId")]
        public Banore Banore { get; set; }
        public string Mesazhi { get; set; }

        public DateTime DataDergimit { get; set; }
        public bool Shikushmeria { get; set; }
        public bool  Statusi { get; set; }
    }
}
