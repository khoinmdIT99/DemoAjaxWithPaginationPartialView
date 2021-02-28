using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public int Sasia { get; set; }
        public string BanoreId { get; set; }
        [ForeignKey("BanoreId")]
        public Banore Banore { get; set; }

        public DateTime Date { get; set; }
        public bool Paguar { get; set; }
    }
}
