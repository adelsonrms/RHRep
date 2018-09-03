using RH.Domain.Entities;
using RH.Domain.Repositories;
using RH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using RH.Shared;

namespace ERP.RH.Application
{
   public class ContratoApplication : DataContext
    {
        private IGenericRepository<Contrato> _rep; //Repositório
        private readonly DateService _dtService = new DateService();
        Funcionario _funcionario = null;


        public ContratoApplication()
        {
            _rep = InicializaRepositorio<Contrato>();
        }


        public IEnumerable<Contrato> ObtemRelacaoDeContratos()
        {
            IEnumerable<Contrato> lista;
            try
            {
                lista = _rep.ObterTodos();
            }
            catch (Exception)
            {
                lista = null;
            }

            return lista;
        }

        public Contrato RecuperaContratoPorFuncionario(int FuncionarioId)
        {
            Contrato contrato = new Contrato();

            _funcionario = InicializaRepositorio<Funcionario>().ObterPorId(FuncionarioId);

            try
            {
                var repContrato = InicializaRepositorio<Contrato>();
                //Obtem os contrados do Banco de dados
                var contratos = repContrato.ObterTodos();

                contrato = contratos.FirstOrDefault(u => u.IdFuncionario == _funcionario.Id);

                contrato.Cargo = InicializaRepositorio<Cargo>().ObterPorId(contrato.IdCargo);
                contrato.Modalidade = InicializaRepositorio<Modalidade>().ObterPorId(contrato.IdModalidade);
                contrato.TempoDeCasa = CalculaTempoDeCasa(contrato);

                return contrato;
            }
            catch (Exception e)
            {
                //Caso haja falha, retorna um contrato vazio
                return contrato;
                throw;
            }
        }

        public Contrato RecuperaContratoPorFuncionario(Funcionario funcionario)
        {

            Contrato contrato = new Contrato();

            try
            {
                return RecuperaContratoPorFuncionario(funcionario.Id);
            }
            catch (Exception e)
            {
                //Caso haja falha, retorna um contrato vazio
                return contrato;
                throw;
            }
        }

        public IEnumerable<Contrato> RecuperaContratosPorFuncionario(Funcionario funcionario, Enuns.eSituacao situacao = Enuns.eSituacao.Todos)
        {
            List<Contrato> _contratos = new List<Contrato>();
            try
            {
                var repContrato = InicializaRepositorio<Contrato>();

                //Obtem os contrados do Banco de dados
                IEnumerable<Contrato> contratos = repContrato.ObterTodos().Where(u => u.IdFuncionario == funcionario.Id); 

                switch (situacao)
                {
                    case Enuns.eSituacao.Ativo:
                        contratos = contratos.Where(u=>u.ativo == true);
                        break;
                    case Enuns.eSituacao.Inativo:
                        contratos = contratos.Where(u => u.ativo == false);
                        break;
                }
                    

                

                //Atualiza informações sobre os contratos
                foreach (Contrato contrato in contratos)
                {
                    contrato.Cargo = InicializaRepositorio<Cargo>().ObterPorId(contrato.IdCargo);
                    contrato.Modalidade = InicializaRepositorio<Modalidade>().ObterPorId(contrato.IdModalidade);
                    contrato.TempoDeCasa = CalculaTempoDeCasa(contrato);
                    _contratos.Add(contrato);
                }
               
                return _contratos;

            }
            catch (Exception e)
            {
                //Caso haja falha, retorna um contrato vazio
                return _contratos;
                throw;
            }
        }

        public IEnumerable<Contrato> ContratosAtivos(int funcionarioId)
        {
            _funcionario = InicializaRepositorio<Funcionario>().ObterPorId(funcionarioId);

            return RecuperaContratosPorFuncionario(_funcionario, Enuns.eSituacao.Ativo);
        }

        public IEnumerable<Contrato> ContratosInativos(int funcionarioId)
        {
            _funcionario = InicializaRepositorio<Funcionario>().ObterPorId(funcionarioId);

            return RecuperaContratosPorFuncionario(_funcionario, Enuns.eSituacao.Inativo);
        }

        public string CalculaTempoDeCasa(Contrato contrato)
        {
            try
            {
                DataContrato dt = new DataContrato(contrato);
                return contrato.TempoDeCasa = _dtService.TempoDecorrido(dt.DataInicio, dt.DataFim, "ym");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
                throw;
            }
        }

    }
}
