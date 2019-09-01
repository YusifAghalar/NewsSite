using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewsSiteVersion2.Entities
{
    public class CustomIdentityDbContext:IdentityDbContext<ApplicationUser,CustomRole,string>
    {
        public CustomIdentityDbContext(DbContextOptions options):base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(b => {
                b.HasOne(x => x.UserRole).WithMany().HasForeignKey(x=>x.CustomRoleId);
                base.OnModelCreating(builder);
            });
        }


    }
}
