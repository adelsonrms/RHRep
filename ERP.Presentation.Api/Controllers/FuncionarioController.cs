using System;
using ERP.Presentation.Api.Models;
using ERP.RH.Application;
using RH.Domain.Entities;
using System.Web.Mvc;
using RH.Services;

namespace ERP.Presentation.Api.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        //
        // GET: /Funcionario/
        private readonly FuncionarioApplication  _app = new FuncionarioApplication();
        

        public ActionResult ListaDeFuncionarios()
        {
            //Recupera a lista de funcionarios
            var lista = _app.ObtemListaDeFuncionarios();

            //Devolve o resultado para View
            return View(lista);
        }


        public ActionResult FichaCadastral(int id, string modo)
        {
            //ViewBag.ComboPerfil = 
            //PopulaCombos();
            bool editar = (modo == "edit");

            using (EntityApplication<Cargo> app = new EntityApplication<Cargo>())
            {
                ViewData["cmbCargos"] = new DropDownService().HtmlCombo(app.ObterTodos(), "Id", "Nome");
            }

            using (EntityApplication<Modalidade> app = new EntityApplication<Modalidade>())
            {
                ViewData["cmbModalidade"] = new DropDownService().HtmlCombo(app.ObterTodos(), "Id", "NomeModalidade");
            }

            using (EntityApplication<EstadoCivil> app = new EntityApplication<EstadoCivil>())
            {
                ViewData["cmbEstadoCivil"] = new DropDownService().HtmlCombo(app.ObterTodos(), "Id", "Nome");
            }


            ViewBag.TituloDaPagina = "Ficha Cadastral";
            ViewBag.Editar = editar;
            ViewBag.Modo = modo;
            Funcionario funcionario;

            if (modo=="edit" || modo=="read")
            {
                funcionario = _app.ObtemFuncionario(id);
            }
            else
            {
                funcionario = new Funcionario();
            }
            
            return View(funcionario);
        }

        private void PopulaCombos()
        {
            try
            {
                
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao popular as Combos", e);
            }
        }


        [HttpPost]
        public ActionResult FichaCadastral(Funcionario model)
        {
            _app.SalvaFuncionario(model);
            return View();
        }

        [HttpPost]
        public ActionResult SalvarFuncionario(Funcionario model)
        {
            _app.SalvaFuncionario(model);
            MensagemParaUsuario.MensagemSucesso("Dados atualizados com sucesso.", TempData);
            var parametros = new {id= model.Id, modo = "read"};
            return RedirectToAction("FichaCadastral", parametros);

        }
    }
}
