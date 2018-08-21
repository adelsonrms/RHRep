using System.Data.Entity.ModelConfiguration;
using RH.Domain.Entities;

namespace RH.Infra.Data.Mapping
{
    public class FuncionarioMapping : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapping()
        {
            //Essa configuração informa que o campo UsuarioID será chave primaria da tabela
            /*
             * Obs. 
             * Isso, nesse caso, é desnecessário pois o EntityFramework analisa o nome dos campos e ja define como padrão a propriedade 
             * formada pelo NomeDaClass + 'ID' ou simplesmente 'Id'
             * */
            //Define no nome da schema.tabela a ser criada
            ToTable("Funcionario", "TPA");
            //base.HasRequired(t => t.Contrato);
            base.HasRequired(d => d.Documento);

            Ignore(p => p.NomeDoFuncionario);

            //base.HasRequired(p => p.Nome);

            //Especifica o tamanho do campo para 100 caracteres na coluna 'Nome'
            //base.Property(p => p.Nome).HasMaxLength(100);

            //Coluna de DataTime será convertida para o tipo Date
            //No primeiro momento gerou erro pois tentei gerar uma coluna Date sendo que o tipo da coluna era String
            //Property(p => p.DataNascimento).HasColumnType("Date");

            /*
             * Erros ocorridos em relação ao relacionamento das classes
             * ---------------------------------------------------------------
             * 1 - A propriedade 'Funcionario' não estava definida nas classes Relacionadas
             * Conclusão : As classes relacionadas devem ter suas respectivas propriedades para vincula-las
             */

            // base.HasRequired(p => p.Endereco).WithRequiredDependent(c => c.Funcionario);

            // base.HasRequired(p => p.Telefone).WithRequiredDependent(c => c.Funcionario); 

            // base.HasRequired(p => p.Contrato).WithRequiredDependent(c => c.Funcionario);
        }
    }
}