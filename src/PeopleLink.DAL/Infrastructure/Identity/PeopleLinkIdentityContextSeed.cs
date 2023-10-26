using ApplicationCore.Constants;
using ApplicationCore.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class PeopleLinkIdentityContextSeed
    {
        public static async Task SeedAsync(PeopleLinkIdentityContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await db.Database.MigrateAsync();

            if (await userManager.Users.AnyAsync() || await roleManager.Roles.AnyAsync())
                return;

            var admin = new ApplicationUser()
            {
                UserName = AuthorizationConstant.DEFAULT_ADMIN_USER,
                Email = AuthorizationConstant.DEFAULT_ADMIN_USER,
                EmailConfirmed = true,
                PhoneNumber = "5347384176",
                FirstName = "Şule",
                LastName = "Çakır",
                BirthDate = new DateTime(1989, 06, 14),
                BirthPlaceName = "Ankara",
                PictureUri = "0bd2fa7e-6679-4415-ba8c-68362b7f8c13.jpg",
                IdentityNumber = "48951167304",
                HireDate = new DateTime(2017, 08, 26),
                OccupationId = 1,
                DepartmentId = 1,
                OccupationName = "Yazılım Mühendisi",
                DepartmentName = "Bilgi Teknolojileri Departmanı",
                CompanyName = "Bilge Adam Teknoloji",
                CityId = 6,
                DistrictId = 1231,
                CityName = "Ankara",
                DistrictName = "Çankaya",
                FullAddress = "M Mah. S Sok. No: 7",
                Salary = 51000,
                Gender = Gender.Female
            };

            var demoUser = new ApplicationUser()
            {
                UserName = AuthorizationConstant.DEFAULT_DEMO_USER,
                Email = AuthorizationConstant.DEFAULT_DEMO_USER,
                EmailConfirmed = true,
                PhoneNumber = "5557894512",
                FirstName = "Ezgi",
                LastName = "Sönmez",
                MiddleLastName = "Karaca",
                BirthDate = new DateTime(1990, 05, 10),
                BirthPlaceName = "Eskişehir",
                PictureUri = "14e52590-1d10-45fb-8480-7737a8eaf542.jpg",
                IdentityNumber = "12345678910",
                HireDate = new DateTime(2020, 05, 10),
                OccupationId = 1,
                DepartmentId = 1,
                OccupationName = "Yazılım Mühendisi",
                DepartmentName = "Bilgi Teknolojileri Departmanı",
                CompanyName = "Bilge Adam Teknoloji",
                CityId = 6,
                DistrictId = 1231,
                CityName = "Ankara",
                DistrictName = "Çankaya",
                FullAddress = "A Sok. 26 / 8",
                Salary = 40000,
                Gender = Gender.Female
            };


            var demoUser2 = new ApplicationUser()
            {
                UserName = "ahmet.yilmaz@bilgeadamboost.com",
                Email = "ahmet.yilmaz@bilgeadamboost.com",
                EmailConfirmed = true,
                PhoneNumber = "5557894512",
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                BirthDate = new DateTime(1988, 04, 09),
                BirthPlaceName = "İzmir",
                PictureUri = "14e52590-1d10-45fb-8480-7737a8eaf542.jpg",
                IdentityNumber = "54789621475",
                HireDate = new DateTime(2018, 07, 10),
                OccupationId = 2,
                DepartmentId = 2,
                OccupationName = "İş Analisti",
                DepartmentName = "Ar-Ge Departmanı",
                CompanyName = "Bilge Adam Teknoloji",
                CityId = 6,
                DistrictId = 1922,
                CityName = "Ankara",
                DistrictName = "Etimesgut",
                FullAddress = "B Sok. 23 / 1",
                Salary = 43000,
                Gender = Gender.Male
            };

            var manager = new ApplicationUser()
            {
                UserName = AuthorizationConstant.DEFAULT_MANAGER_USER,
                Email = AuthorizationConstant.DEFAULT_MANAGER_USER,
                EmailConfirmed = true,
                PhoneNumber = "5054796243",
                FirstName = "Mehmet",
                MiddleName = "Kaan",
                LastName = "Kaya",
                BirthDate = new DateTime(1985, 02, 21),
                BirthPlaceName = "İstanbul",
                PictureUri = "a0eea1e9-d93f-4c0f-82b3-a4333983c450.jpg",
                IdentityNumber = "10124578962",
                HireDate = new DateTime(2015, 04, 10),
                OccupationId = 3,
                DepartmentId = 5,
                OccupationName = "Proje Yöneticisi",
                DepartmentName = "Proje Yönetim Departmanı",
                CompanyName = "Bilge Adam Teknoloji",
                CityId = 6,
                DistrictId = 1231,
                CityName = "Ankara",
                DistrictName = "Çankaya",
                FullAddress = "C Sok. A-3 / 7",
                Salary = 45000,
                Gender = Gender.Male
            };

            demoUser.AdvanceAllowance = demoUser.Salary * 3;
            demoUser.ExpenseAllowance = demoUser.Salary * 3;
            demoUser.AccruedLeave = demoUser.IsActive ? (DateTimeOffset.Now > demoUser.HireDate.AddYears(1) ? (DateTimeOffset.Now < demoUser.HireDate.AddYears(6) ? 14 : 20) : 0) : 0;

            demoUser2.AdvanceAllowance = demoUser2.Salary * 3;
            demoUser2.ExpenseAllowance = demoUser2.Salary * 3;
            demoUser2.AccruedLeave = demoUser2.IsActive ? (DateTimeOffset.Now > demoUser2.HireDate.AddYears(1) ? (DateTimeOffset.Now < demoUser2.HireDate.AddYears(6) ? 14 : 20) : 0) : 0;

            await userManager.CreateAsync(admin, AuthorizationConstant.DEFAULT_PASSWORD);
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstant.Roles.ADMIN));
            await userManager.AddToRoleAsync(admin, AuthorizationConstant.Roles.ADMIN);

            await userManager.CreateAsync(manager, AuthorizationConstant.DEFAULT_PASSWORD);
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstant.Roles.MANAGER));
            await userManager.AddToRoleAsync(manager, AuthorizationConstant.Roles.MANAGER);

            await userManager.CreateAsync(demoUser, AuthorizationConstant.DEFAULT_PASSWORD);
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstant.Roles.EMPLOYEE));
            await userManager.AddToRoleAsync(demoUser, AuthorizationConstant.Roles.EMPLOYEE);

            await userManager.CreateAsync(demoUser2, AuthorizationConstant.DEFAULT_PASSWORD);
            await userManager.AddToRoleAsync(demoUser2, AuthorizationConstant.Roles.EMPLOYEE);
        }
    }
}
