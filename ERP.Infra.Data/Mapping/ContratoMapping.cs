using RH.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RH.Infra.Data.Mapping
{

    public class ContratoMapping : EntityTypeConfiguration<Contrato>
    {
        public ContratoMapping()
        {
            base.Ignore(p => p.TempoDeCasa);
            //base.HasMany(t=>t.Cargo).WithRequired(p => p.)
        }
    }
}
