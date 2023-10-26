using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class PeopleLinkContextSeed
    {
        public static async Task SeedAsync(PeopleLinkContext db)
        {
            await db.Database.MigrateAsync();

            if (await db.Occupations.AnyAsync() || await db.Departments.AnyAsync())
                return;

            var o1 = new Occupation() { Name = "Yazılım Mühendisi" };
            var o2 = new Occupation() { Name = "Test Mühendisi" };
            var o3 = new Occupation() { Name = "Satış Temsilcisi" };
            var o4 = new Occupation() { Name = "Sistem Mühendisi" };
            var o5 = new Occupation() { Name = "Muhasebeci" };
            var o6 = new Occupation() { Name = "İnsan Kaynakları Yöneticisi" };
            var o7 = new Occupation() { Name = "Proje Yöneticisi" };
            var o8 = new Occupation() { Name = "Şirket Yöneticisi" };

            var d1 = new Department() { Name = "Yazılım Geliştirme" };
            var d2 = new Department() { Name = "Kalite Güvence ve Test" };
            var d3 = new Department() { Name = "Satış ve Pazarlama" };
            var d4 = new Department() { Name = "Bilgi Teknolojileri" };
            var d5 = new Department() { Name = "Muhasebe ve Finans" };
            var d6 = new Department() { Name = "İnsan Kaynakları" };
            var d7 = new Department() { Name = "Proje Yönetimi" };
            var d8 = new Department() { Name = "Yönetim ve İdari İşler" };

            var occupations = new List<Occupation> { o1, o2, o3, o4, o5, o6, o7, o8 };
            var departments = new List<Department> { d1, d2, d3, d4, d5, d6, d7, d8 };

            db.AddRange(occupations);
            db.AddRange(departments);
            await db.SaveChangesAsync();
        }
    }
}
