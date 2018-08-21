using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERP.RH.Domain.Entidades;
using ERP.Shared;
using ERP.Shared.Extensions;
using ERP.Infra.Data.EFContexts;
using Infra.Data.Repositories;
using System.Data.Entity.Infrastructure;
using System.Data.Common;

namespace RH.Domain.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CriacaoDeFuncionario()
        {
            Funcionario usuario;
            //------------------------------------------------------------------------
            //Informações Basicas
            //------------------------------------------------------------------------
            usuario = new Funcionario
            {
                Nome = "Adelson Silva",
                DataNascimento = new System.DateTime(1982, 8, 14),
                SexoID = Enuns.eSexo.Masculino.Value(),
                EmailPessoal = "adelson@tecnun.com.br",
                EmailProfissional = "adelsons@gmail.com"
            };

            Assert.IsTrue(usuario != null);

        }


        [TestMethod]
        public void VerificaSeConextoEValido()
        {
            EFDbContext db = new EFDbContext("Data Source=(localdb)\\projects;Initial Catalog=ERP;Integrated Security=True");

            db.CarregarDadosIniciais();

            Assert.IsTrue(db.Database.Connection.State==System.Data.ConnectionState.Open);
        }


        [TestMethod]
        public void CriarBDComDados()
        {

            EFDbContext db = new EFDbContext("Data Source=(localdb)\\projects;Initial Catalog=ERP;Integrated Security=True");
            FuncionarioRepositorio UserRep = new FuncionarioRepositorio(db);
            var lista = UserRep.ObterTodos();

            Assert.IsNotNull(lista);


        }
    }
}
