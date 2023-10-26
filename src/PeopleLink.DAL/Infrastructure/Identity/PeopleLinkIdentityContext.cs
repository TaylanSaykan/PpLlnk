using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class PeopleLinkIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public PeopleLinkIdentityContext(DbContextOptions<PeopleLinkIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}