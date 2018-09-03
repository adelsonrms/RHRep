using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using ERP.RH.Application;
using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace ERP.RH.Application
{
    public class EntityApplication<T>: DataContext, IEntityApplication<T> where T:class 
    {
        protected IGenericRepository<T> Rep { get; }

        public EntityApplication()
        {
            Rep = InicializaRepositorio<T>();
        }
        public IEnumerable<T> ObterTodos()
        {
            IEnumerable<T> lista;
            try
            {
                lista = Rep.ObterTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter os dados da lista do Repositorio", ex);
            }
            return lista;
        }
        public void Salvar(T entidade)
        {
            try
            {
                Rep.Inserir(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar a Entidade no Banco de dados", ex);
            }


        }
        protected T ObterPorId(int id)
        {
            try
            {
                return Rep.ObterPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar a Entidade no Banco de dados", ex);
            }
        }
    }
}
