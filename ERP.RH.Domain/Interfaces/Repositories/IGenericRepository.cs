using System.Collections.Generic;

namespace RH.Domain.Repositories
{
    /// <summary>
    ///     Interface Generica (T) onde T será a classe que a utilizará
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> ObterTodos();
        T ObterPorId(int id);
        bool Inserir(T entity);
        bool Excluir(T entity);
        bool Alterar(T entity);
        void Submit();
    }
}