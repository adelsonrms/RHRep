using RH.Domain.Entities;
using RH.Domain.Repositories;
using RH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using RH.ViewModel;

namespace ERP.RH.Application
{
    public class FuncionarioApplication: EntityApplication<Funcionario>
    {

        Funcionario _funcionario = null;

        public IEnumerable<Funcionario> ObtemListaDeFuncionarios()
        {
            return Rep.ObterTodos();
        }

        public void SalvaFuncionario(Funcionario model)
        {
            try
            {
                Rep.Alterar(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            ;
        }

        public Funcionario ObtemFuncionario(Funcionario funcionario)
        {
            try
            {
                _funcionario = funcionario;
                PopulaInformacoesFuncionario();
            }
            catch (Exception e)
            {
                _funcionario = funcionario;
                Console.WriteLine(e);
            }
            return _funcionario;
        }
        /// <summary>
        /// Recupera um funcionario 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Funcionario ObtemFuncionario(int id)
        {
            try
            {
                //Busca o funcionario na base
                _funcionario = Rep.ObterPorId(id);

                PopulaInformacoesFuncionario();

            }
            catch (Exception e)
            {
                _funcionario = new Funcionario();
                Console.WriteLine(e);
            }
            return _funcionario;
        }

        public string CalculaIdade(Funcionario funcionario)
        {
            string idade="";

            if (funcionario.DataNascimento != null)
            {
                idade = new DateService().TempoDecorrido(funcionario.DataNascimento, DateTime.Today, "y");
            }
            return idade;
        }

        public Funcionario ObtemFuncionario(string email)
        {
            try
            {
                //Busca o funcionario na base
                _funcionario = Rep.ObterTodos().Where(f => f.EmailProfissional == email).SingleOrDefault();

                PopulaInformacoesFuncionario();
            }
            catch (Exception e)
            {
                _funcionario = new Funcionario();
            }
            return _funcionario;
        }

        private void PopulaInformacoesFuncionario()
        {
            if (_funcionario!=null)
            {

                //Busca informações dos contratos de trabalho
                var contrato = new ContratoApplication().RecuperaContratoPorFuncionario(_funcionario);

                //Recupera o primeiro contrato ativo mais recente
                _funcionario.Contrato = contrato;

                //Obtem informações sobre os documentos
                _funcionario.Documento = new EntityApplication<Documento>().ObterTodos().SingleOrDefault(f => f.IdFuncionario == _funcionario.Id);

                //Calcula a idade do funcionario baseado na sua data de nascimento
                _funcionario.Idade = CalculaIdade(_funcionario);
            }
        }
    }
}
