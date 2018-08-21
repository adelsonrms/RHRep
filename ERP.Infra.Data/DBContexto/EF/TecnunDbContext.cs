using System;
using RH.Domain.Entities;
using RH.Domain.Regional;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using RH.Infra.Data.Mapping;


// ReSharper disable once CheckNamespace
namespace RH.Infra.Data.DBContexts
{
    public class TecnunDbContext : DbContext, IDisposable
    {

        #region Construtores
        public static TecnunDbContext Create()
        {
            return new TecnunDbContext();
        }

        public TecnunDbContext(string connectionString) : base(connectionString)
        {
            inicializaConexto();
        }

        public TecnunDbContext() : base("name=TPAContextConnStr")
        {
            inicializaConexto();
        }
        #endregion

        #region DataSets - Entidades Mapeadas para as Tabelas

        //Datasets
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Endereco> Endereços { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<FaixaCargo> Perfies { get; set; }
        public DbSet<NivelCargo> FaixaCargos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Anexo> Anexos { get; set; }

        #endregion

        #region OnModelCreating - Inicialização/Criação do Modelo
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Desliga algumas opções
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            /*---------------------------------------------------------------------------------------------------------------------------
             * Classes que devem ser ignoradas do mapeamento para não gerar tabelas.
             ---------------------------------------------------------------------------------------------------------------------------*/
            modelBuilder.Ignore<System.Object>();

            /*---------------------------------------------------------------------------------------------------------------------------
             * Mapeamentos especificos para as entidades
             ---------------------------------------------------------------------------------------------------------------------------*/
            modelBuilder.Configurations.Add(new AnexoMapping());
            modelBuilder.Configurations.Add(new FuncionarioMapping());
            modelBuilder.Configurations.Add(new ContratoMapping());



            /*---------------------------------------------------------------------------------------------------------------------------
             * IDENTITY - Adequação de mapeamento das entidades do Identity
             ---------------------------------------------------------------------------------------------------------------------------*/

            //modelBuilder.Configurations.Add(new IdentityMappingClaims());
            //modelBuilder.Configurations.Add(new IdentityMappingUser());
            //modelBuilder.Configurations.Add(new IdentityMappingRole());
            //modelBuilder.Configurations.Add(new IdentityMappingUserRole());
            //modelBuilder.Configurations.Add(new IdentityMappingUserLogin());

            //Schema padrão será o RH
            //Demais tabelas relacionadas em Schemas diferentes deverão ser mapeadas 
            modelBuilder.HasDefaultSchema("RH");


            /*---------------------------------------------------------------------------------------------------------------------------
             * Configurações gerais para todos os campos (propriedades) e tabelas do banco de dados
             ---------------------------------------------------------------------------------------------------------------------------*/

            //Configura o tipo e tamanho padrão para criação dos campos. Assim o migration não irá mais criar tipo nvarchar(max)
            //modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            //modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            //Essa instrução verifica se alguma proproiedade da classe possui um campo no padrão "NomeClasseId". 
            //Caso identifique, configura-a como chave primeira (PK) na tabela
            //modelBuilder.Properties()
            //    .Where(p => p.Name == p.ReflectedType.Name + "Id")
            //    .Configure(p => p.IsKey());
        }
        #endregion

        #region Métodos Privados
        private void inicializaConexto()
        {
            //Atribui o inicializador ao contexto
            //Na classe 'EFContextInitializer' estão ações que são executadas ao inicializar o contexto
            //Database.SetInitializer<TecnunDbContext>(new EFContextInitializer());
            Database.SetInitializer<TecnunDbContext>(null);
        }
        //Inicializa/Criação do DB, caso nao exista
        public void CarregarDadosIniciais()
        {
            var inicitializer = new EFContextInitializer();
            inicitializer.GerarDadosIniciais(this);
        }
        #endregion
    }
}