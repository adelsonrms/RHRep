using System;
using RH.Domain.Repositories;
using RH.Infra.Data.DBContexts;
using RH.Infra.Data.Repositories;

namespace ERP.RH.Application
{
  public  class DataContext:IDisposable
    {
        //Inicializa o controller ja conectado ao DB
        private readonly TecnunDbContext _db = new TecnunDbContext();

        //Expoe o contexto para a controller
        protected virtual TecnunDbContext Db
        {
            get { return this._db; }
        }
        /// <summary>
        /// Inicializa um repositório generico
        /// </summary>
        /// <typeparam name="TEntidade"></typeparam>
        /// <returns></returns>
        protected IGenericRepository<TEntidade> InicializaRepositorio<TEntidade>() where TEntidade : class
        {
            try
            {
                return new Repositorio<TEntidade>(this._db);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
