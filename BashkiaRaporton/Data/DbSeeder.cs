using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BashkiaRaporton.Data;
using BashkiaRaporton.Models;

namespace BashkiaRaporton.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDb(ApplicationDbContext context,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
         {

            context.Database.EnsureCreated();
            string password = "Bashkia123$";
            //krijimi i rolit
            IdentityRole identityRolefirst = new IdentityRole
            {
                Name = "Administrator"
            };
            await roleManager.CreateAsync(identityRolefirst);
            //krijimi i user te pare
            IdentityUser admin = new IdentityUser
            {
                Email = "admin@buxheti.com",
                UserName = "admin@buxheti.com",

               
                EmailConfirmed = true,
              
            };
            var result =   await userManager.CreateAsync(admin, password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, identityRolefirst.Name);
            }
            context.SaveChanges();
            







            //shtojme banore kur hapet sistemi
            Bashkia bashkia = new Bashkia() { Emri = "Konispol", Pershkrimi = "Bashkia Konispol ndodhet në cepin më jugor të qarkut, rreth 30 km nga Bashkia e Sarandës. Ajo kufizohet në jug me kufirin shtetëror të Greqisë, në lindje me bashkinë Finiq dhe në perëndim me detin Jon" };
            context.Add(bashkia);
            context.SaveChanges();


            Taksa taksa = new Taksa() { Pershkrimin= "Taksa vendore mbi biznesin e vogël" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa mbi pasurinë e paluajtshme" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "TaksaNdertesa" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "TaksaBujqesi" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "TaksaeTruallit" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "TaksaeUjit" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa mbi transaksionet e pasurisë" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa vendore në shërbimin hotelier" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa e ndikimit në infrastrukturë nga ndërtimet e reja" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa e tabelës" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa e përkohëshme aaa" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa e përkohëshme 1 bbb" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa mbi kalimin e të drejtës së pronësisë / pasuritë e paluajtshme" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa vjetore për qarkullimin e mjeteve të përdorura" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa e rentës minerare" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa mbi të ardhurat personale" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Taksa të tjera" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa me menaxhimit te mbetjeve" };
            context.Add(taksa);      
            taksa = new Taksa() { Pershkrimin = "Tarifa e pastrimit për familjet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa e pastrimit për institucionet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa e pastrimit për biznesin " };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për mbledhjen dhe largimin e mbetjeve " };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për ndriçimin publik" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për ndriçimin publik nga familjet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për ndriçimin publik nga institucionet " };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për ndriçimin publik nga biznesi" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për gjelbërimin" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për gjelbërimin nga Institucionet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Teatri" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Qendra kulturore e fëmijëve" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Pallati i sportit" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Qendra Komunitare" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Mensa (Konviktet)" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Kopshtet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Çerdhet" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për furnizimin me ujë dhe kanalizime" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa për shërbimin e ujitjes dhe kullimit" };
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa e përkohëshme" };          
            
            context.Add(taksa);
            taksa = new Taksa() { Pershkrimin = "Tarifa të tjera" };

            context.Add(taksa);


            await context.SaveChangesAsync();

            Status status = new Status() { Statusi = "Martuar" };
            context.Add(status);

            status = new Status() { Statusi = "Beqare" };
            context.Add(status);
            await context.SaveChangesAsync();



        }



    }
}

