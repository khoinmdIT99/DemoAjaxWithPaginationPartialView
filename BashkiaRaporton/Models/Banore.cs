using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Models
{
    public class Banore : IdentityUser
    {
       
        public int NjesiaAdministrativeId { get; set; }
        public NjesiaAdministrative NjesiaAdministrative { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public string  Emri { get; set; }
        public string Mbiemri { get; set; }
        public bool Detyrime { get; set; }
        public DateTime Data { get; set; }



    }
}
