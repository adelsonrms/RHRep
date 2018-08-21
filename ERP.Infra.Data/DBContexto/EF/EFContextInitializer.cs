using RH.Shared.Extensions;
using RH.Domain.Entities;
using RH.Domain.Regional;
using RH.Shared;
using System;
using System.Data.Entity;

namespace RH.Infra.Data.DBContexts
{
    /// <summary>
    ///     Define um inicializador para a classe de contexto.
    ///     Um inicializador especifica ações que serão executadas quando o contexto é criado para o banco de dados.
    /// </summary>
    public class EFContextInitializer : CreateDatabaseIfNotExists<TecnunDbContext>
    {
        /// <summary>
        ///     Esse metodo será executado sempre que o banco for recriado.
        /// </summary>
        /// <param name="context">Representa a instancia do DbContext</param>
        protected override void Seed(TecnunDbContext context)
        {
            GerarDadosIniciais(context);
        }

        public void GerarDadosIniciais(TecnunDbContext context)
        {
            try
            {
                Funcionario usuario;
                //Cria um usuário

                //------------------------------------------------------------------------
                //Informações Basicas
                //------------------------------------------------------------------------
                usuario = new Funcionario
                {
                    Nome = "Adelson Silva",
                   // DataNascimento = "14081982",
                    SexoId = Enuns.eSexo.Masculino.Value(),
                    EmailProfissional = "adelson@tecnun.com.br"
                };

                //-------------------------------------------------------------------------
                //Adiciona informações adicionais
                //-------------------------------------------------------------------------

                //Endereço
                var endereco = new Endereco
                {
                    Logradouro = "Rua José Adão",
                    Numero = "58",
                    Bairro = "Vila dos Palmares",
                    CEP = "05273-110",
                    Cidade = new Cidade
                    {
                        Nome = "São Paulo",
                        Estado = new Estado
                        {
                            Nome = "São Paulo",
                            UF = "SP"
                        }
                    }
                };

                //Telefone
                var telefone = new Telefone
                {
                    Celular = "11 96798-0117",
                    Comercial = "2178-4205",
                    Residencial = "39128020"
                };

                //Contrato contrato = new Contrato
                //{
                //    TipoDeContrato = Enuns.eTipoContrato.Trabalho
                //    //DataAdimissao = new DateTime(2013, 12,01)
                //};

                context.Endereços.Add(endereco);
                context.SaveChanges();

                context.Telefones.Add(telefone);
                context.SaveChanges();

                //context.Contratos.Add(contrato);
                //context.SaveChanges();

                //usuario.Endereco = endereco;
                //usuario.Telefone = telefone;
                //usuario.Contrato = contrato;

                //-------------------------------------------------------------------------
                //Adiciona no contexto
                //-------------------------------------------------------------------------
                context.Funcionarios.Add(usuario);


                usuario = new Funcionario
                {
                    Nome = "Fernando Ortega",
                    //DataNascimento = new DateTime(1978, 11, 21),
                    SexoId = Enuns.eSexo.Masculino.Value(),
                    EmailProfissional = "ortega@tecnun.com.br"
                };
                context.Funcionarios.Add(usuario);

                usuario = new Funcionario
                {
                    Nome = "Milene Bispo",
                    //DataNascimento = new DateTime(1985, 01, 13),
                    SexoId = Enuns.eSexo.Masculino.Value(),
                    EmailProfissional = "milene@tecnun.com.br"
                };
                context.Funcionarios.Add(usuario);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}