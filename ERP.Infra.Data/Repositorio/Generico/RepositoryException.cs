using System;

namespace RH.Infra.Data.Repositories
{
    /// <summary>
    ///     Encapsula erros especificos para Repositorios
    ///     Essa classe obriga a informar a Exception que gerou o erro.
    /// </summary>
    [Serializable]
    internal class RepositoryException : Exception
    {
        private Exception ex;

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
            ex = innerException;
        }
    }
}