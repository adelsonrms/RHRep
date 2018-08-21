using System.Web.Mvc;
using RH.Infra.Data.DBContexts;

namespace ERP.Presentation.Api.Controllers
{
    public class ControllerBase:Controller

    {
        //Inicializa o controller ja conectado ao DB
        private  readonly  TecnunDbContext _db = new TecnunDbContext();

        //Expoe o contexto para a controller
        protected virtual TecnunDbContext db
        {
            get { return _db; }
        }
        //Finaliza a conexao
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}