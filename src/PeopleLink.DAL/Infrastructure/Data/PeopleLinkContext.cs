using ApplicationCore.Entities;
using ApplicationCore.Entities.AdvanceEntities;
using ApplicationCore.Entities.CityAndDistrictEntities;
using ApplicationCore.Entities.ExpenseEntities;
using ApplicationCore.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PeopleLinkContext : DbContext
    {
        public PeopleLinkContext(DbContextOptions<PeopleLinkContext> options) : base(options)
        {
        }
        public DbSet<Occupation> Occupations => Set<Occupation>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<District> Districts => Set<District>();

        public DbSet<Leave> Leaves => Set<Leave>();
        public DbSet<AnnualLeave> AnnualLeaves => Set<AnnualLeave>();
        public DbSet<BereavementLeave> BereavementLeaves => Set<BereavementLeave>();
        public DbSet<CompassionateLeave> CompassionateLeaves => Set<CompassionateLeave>();
        public DbSet<MarriageLeave> MarriageLeaves => Set<MarriageLeave>();
        public DbSet<MaternityLeave> MaternityLeaves => Set<MaternityLeave>();
        public DbSet<PaternityLeave> PaternityLeaves => Set<PaternityLeave>();
        public DbSet<SickLeave> SickLeaves => Set<SickLeave>();

        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<AccommodationExpense> AccommodationExpenses => Set<AccommodationExpense>();
        public DbSet<HealtInsuranceAndOtherBenefitsExpense> HealtInsuranceAndOtherBenefitsExpenses => Set<HealtInsuranceAndOtherBenefitsExpense>();
        public DbSet<MealExpense> MealExpenses => Set<MealExpense>();
        public DbSet<PhoneAndInternetExpense> PhoneAndInternetExpenses => Set<PhoneAndInternetExpense>();
        public DbSet<ProfessionalDevelopmentExpense> ProfessionalDevelopmentExpenses => Set<ProfessionalDevelopmentExpense>();
        public DbSet<TransportationExpense> TransportationExpenses => Set<TransportationExpense>();

        public DbSet<Advance> Advances => Set<Advance>();
        public DbSet<IndividualAdvance> IndividualAdvances => Set<IndividualAdvance>();
        public DbSet<CorporateAdvance> CorporateAdvances => Set<CorporateAdvance>();

        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
