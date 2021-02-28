using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Pronesia
    {
        public int Id { get; set; }
        public double Ndertesa { get; set; }
        public double TokaBujqesore { get; set; }
        public double Titulli { get; set; }
        public string BanoreId { get; set; }
        [ForeignKey("BanoreId")]
        public Banore Banore { get; set; }
    }
}
