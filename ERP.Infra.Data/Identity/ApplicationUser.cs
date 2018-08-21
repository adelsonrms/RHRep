using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using RH.Infra.Data.Mapping;

namespace RH.Infra.Data.DBContexts
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("TPAContextConnStr", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            /*---------------------------------------------------------------------------------------------------------------------------
             * IDENTITY - Adequação de mapeamento das entidades do Identity
             ---------------------------------------------------------------------------------------------------------------------------
            */
            modelBuilder.Configurations.Add(new IdentityMappingClaims());
            modelBuilder.Configurations.Add(new IdentityMappingUser());
            modelBuilder.Configurations.Add(new IdentityMappingRole());
            modelBuilder.Configurations.Add(new IdentityMappingUserRole());
            modelBuilder.Configurations.Add(new IdentityMappingUserLogin());
        }
    }
}
