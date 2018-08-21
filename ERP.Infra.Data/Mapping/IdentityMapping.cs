using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace RH.Infra.Data.Mapping
{
    /// <summary>
    /// Retorna variáveis utilizadas para configurar o Identity
    /// </summary>
    public static class IdentityOptions
    {
        /// <summary>
        /// Schema padrão para armazenar as tabelas.
        /// </summary>
        public static string DefaultSchema { get { return "dbo"; } }
    }

    public class IdentityMappingUser : IdentityMappingConfig<IdentityUser>
    {
        public IdentityMappingUser() : base("AspNetUsers")
        {
        }
    }


    public class IdentityMappingRole : IdentityMappingConfig<IdentityRole> { public IdentityMappingRole() : base("AspNetRoles") { } }
    public class IdentityMappingClaims : IdentityMappingConfig<IdentityUserClaim> { public IdentityMappingClaims() : base("AspNetUserClaims") { } }
    public class IdentityMappingUserRole : IdentityMappingConfig<IdentityUserRole>
    {
        public IdentityMappingUserRole() : base("AspNetUserRoles")
        {
            base.HasKey(p => p.UserId)
                .HasKey(p => p.RoleId);
        }
    }
    public class IdentityMappingUserLogin : IdentityMappingConfig<IdentityUserLogin>
    {
        public IdentityMappingUserLogin() : base("AspNetUserLogins")
        {
            base.HasKey(p => p.UserId)
                .HasKey(p => p.LoginProvider)
                .HasKey(p => p.ProviderKey);
        }
    }
    public class IdentityMappingConfig<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public IdentityMappingConfig(string nomeTabela)
        {
            base.ToTable(nomeTabela, IdentityOptions.DefaultSchema);
        }
    }

}
