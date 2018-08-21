using System;
using System.Linq;
using ERP.RH.Application;
using System.Web.Mvc;
using ERP.Presentation.Api.Models;
using Microsoft.AspNet.Identity;


namespace RH.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FuncionarioApplication _app = new FuncionarioApplication();

        public ActionResult Index()
        {

            DashboardViewModel dashboard = new DashboardViewModel();

            ViewBag.Funcionario = _app.ObtemFuncionario(User.Identity.GetUserName());

            var listaDeFuncionarios = _app.ObtemListaDeFuncionarios().ToList();

            dashboard.QtdDeFuncionarios = listaDeFuncionarios.Count();

            dashboard.QtdDeFuncionariosAtivos = listaDeFuncionarios.Count(f => f.Ativo == true);

            dashboard.QtdNovasContratacoesNoAno = 0;//listaDeFuncionarios.Count(f => Convert.ToDateTime(f.Contrato.DataAdmissao).Year == DateTime.Now.Year);

            //Devolve o resultado para View
            return View(dashboard);
        }


    }
}