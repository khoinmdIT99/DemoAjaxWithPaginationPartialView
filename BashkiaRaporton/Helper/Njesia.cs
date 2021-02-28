using BashkiaRaporton.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashkiaRaporton.Helper
{
    public static class Njesia
    {
        public static int NrBanoresipasnjesise(ApplicationDbContext context, int njesia)
        {
            return   context.Banore.Where(nj => nj.NjesiaAdministrativeId == njesia).Count();
            
        }
        public static string Checked(ApplicationDbContext context, int fatura)
        {
           var number =  context.Fatura.Where(nj => nj.Paguar == true && nj.Id == fatura).Count();
            if(number > 0)
            {
                return "checked";
            }
            else
            {
                return "";
            }

        }
        public static string CheckedBanore(ApplicationDbContext context, string Banoreid)
        {
            var number = context.Banore.Where(nj => nj.Detyrime == true && nj.Id == Banoreid).Count();
            if (number > 0)
            {
                return "checked";
            }
            else
            {
                return "";
            }

        }
        public static string CheckedNjoftime(ApplicationDbContext context, int njoftimeid)
        {
            var number = context.Njoftime.Where(nj => nj.Shikushmeria == true && nj.NjoftimeId == njoftimeid).Count();
            if (number > 0)
            {
                return "checked";
            }
            else
            {
                return "";
            }

        }
        public static string CheckedRole(ApplicationDbContext context, string Banoreid)
        {
            
            return  context.UserRoles.Where(nj => nj.UserId == Banoreid).Select(r => r.RoleId).FirstOrDefault();
           

        }
    }
}
