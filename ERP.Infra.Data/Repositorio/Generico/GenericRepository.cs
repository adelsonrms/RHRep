using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RH.Domain.Repositories;
using RH.Infra.Data.DBContexts;

namespace RH.Infra.Data.Repositories
{
    /// <summary>
    ///     Classe concreta que implementa os metodos assinados pela Interface Generica "IGenericRepository"
    ///     Nessa classe devemos implementar as ações que interage com o banco de dados atraves da instancia do contexto de
    ///     sessão do EF.
    /// </summary>
    /// <typeparam name="TEntity">Representa a instancia da entidade que irá implementar o repositório generico</typeparam>
    public class Repositorio<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TecnunDbContext _Db;

        /// <summary>
        ///     Inicializa um novo repositório ja com a instancia do DbContext
        /// </summary>
        /// <param name="db"></param>
        public Repositorio(TecnunDbContext db)
        {
            _Db = db;
        }

        /// <summary>
        ///     Adiciona uma nova entrada na lista de entidades
        /// </summary>
        /// <param name="obj"></param>
        public bool Inserir(TEntity obj)
        {
            var ok = false;
            try
            {
                _Db.Set<TEntity>().Add(obj);
                CommitChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    string.Format("{1} - Ocorreu um erro ao tentar adicionar uma entidade na Lista \n{0}", ex.Message,
                        ex.Source), ex);
            }

            return ok;
        }

        /// <summary>
        ///     Exclui uma entidade do modelo
        /// </summary>
        /// <param name="obj"></param>
        public bool Excluir(TEntity obj)
        {
            var ok = false;
            try
            {
                _Db.Set<TEntity>().Remove(obj);
                CommitChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    string.Format("{1} - Ocorreu um erro ao tentar Excluir uma entidade na Lista \n{0}", ex.Message,
                        ex.Source), ex);
            }

            return ok;
        }

        /// <summary>
        ///     Atualiza uma elemento na lista
        /// </summary>
        /// <param name="obj"></param>
        public bool Alterar(TEntity obj)
        {
            var ok = false;
            try
            {
                _Db.Entry(obj).State = EntityState.Modified;
                CommitChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    string.Format("{1} - Ocorreu um erro ao tentar Atualizar um elemento na lista de entidades \n{0}",
                        ex.Message, ex.Source), ex);
            }

            return ok;
        }

        /// <summary>
        ///     Retorna a lista de todas os membros de uma entidade
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> ObterTodos()
        {
            try
            {
                return _Db.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    string.Format("{1} - Ocorreu um erro ao tentar recuperar toda a lista de entidades \n{0}",
                        ex.Message, ex.Source), ex);
            }
        }
        
        /// <summary>
        ///     Retorna uma entidade TEntity correspondete a chave primaria informada
        /// summary>
        /// <param name="id">Codigo que deve ser a chave primaria</param>
        /// <returns></returns>
        public TEntity ObterPorId(int id)
        {
            try
            {
                return _Db.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    string.Format(
                        "{1} - Ocorreu um erro ao tentar recuperar um elemento na lista de entidades atraves da chave '{2}' \n{0}",
                        ex.Message, ex.Source, id.ToString()), ex);
            }
        }

        /// <summary>
        ///     Salva as alterações feitas no modelo de objetos para o banco de dados.
        /// </summary>
        /// <returns>Retorna True ou False indicando se obteve sucesso ou nao</returns>
        private bool CommitChanges()
        {
            var ok = false;
            try
            {
                _Db.SaveChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao enviar as atualizações para o banco de dados : " + ex.Message,
                    ex);
            }

            return ok;
        }

        public void Submit()
        {

            CommitChanges();
        }
    }
}