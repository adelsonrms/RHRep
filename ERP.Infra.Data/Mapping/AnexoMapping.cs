using RH.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace RH.Infra.Data.Mapping
{
    public class AnexoMapping : EntityTypeConfiguration<Anexo>
    {
        public AnexoMapping()
        {
            base.Property(p => p.Tamanho).HasColumnType("bigint");
            base.Ignore(p => p.DataUpload);

        }
    }


}
