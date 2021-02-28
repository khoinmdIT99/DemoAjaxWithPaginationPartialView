using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BashkiaRaporton.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BashkiaRaporton.Models.Bashkia> Bashkia { get; set; }
        public DbSet<BashkiaRaporton.Models.NjesiaAdministrative>  njesiaAdministratives { get; set; }
        public DbSet<BashkiaRaporton.Models.Banore> Banore { get; set; }
        public DbSet<BashkiaRaporton.Models.Pronesia> Prona { get; set; }
        public DbSet<BashkiaRaporton.Models.Taksa> Taksas { get; set; }
        public DbSet<BashkiaRaporton.Models.TaksaVlera> TaksaVlera { get; set; }
        public DbSet<BashkiaRaporton.Models.Status> Statuses { get; set; }
        public DbSet<BashkiaRaporton.Models.Fatura> Fatura { get; set; }
        public DbSet<BashkiaRaporton.Models.Njoftime> Njoftime { get; set; }
        public DbSet<BashkiaRaporton.Models.ApplicationRole> ApplicationRole { get; set; }




    }
}
