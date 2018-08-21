using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ERP.RH.Application;
using System.Web.Mvc;
using ERP.Presentation.Api.Models;
using RH.Domain.Entities;
using RH.Services;

namespace ERP.Presentation.Api.Controllers
{
    [Authorize]
    public class AnexoController : Controller
    {
        //
        // GET: /Anexo/
        private readonly AnexoApplication _app = new AnexoApplication();

        
        public ActionResult Index()
        {
            var listaDeAnexos = _app.ObterTodos();
            return View(listaDeAnexos);
        }

        [HttpGet]
        public ActionResult Upload(int id)
        {
            AnexoViewModel model = new AnexoViewModel();

            ViewBag.Modo = "edit";
            if (id!=0)
            {
                Anexo anexo = _app.ObterAnexoPorId(id);
                model.Notas = anexo.Notas;
                ViewBag.Modo = "read";
            }

            
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(AnexoViewModel listaDeArquivos)
        {
            string nomeInterno = "Anexo_" + DateTime.Now.ToString("yyyyMMdd_hhmmss_"); ;

            foreach (var arquivoSelecionado in listaDeArquivos.Arquivos)
            {
                try
                {
                    ArquivoAnexo arquivo = new ArquivoAnexo(arquivoSelecionado);

                    //if (listaDeArquivos.SalvarNoFileSystem == true)
                    //{
                    //try
                    //{
                    //    arquivo = SalvarArquivoNoFileSystem(arquivoSelecionado, nomeInterno);

                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e);
                    //    throw;
                    //}
                    //}

                    byte[] content;

                    using (var binaryReader = new BinaryReader(arquivoSelecionado.InputStream))
                    {
                        content = binaryReader.ReadBytes(arquivoSelecionado.ContentLength);
                        arquivo.BinaryContent = content;
                        arquivo.Size = content.Length;
                    }


                    nomeInterno = nomeInterno + arquivo.Name;
                    Anexo anexo = new Anexo()
                    {
                        CaminhoOrigem = arquivo.FullName,
                        NomeDoArquivo = arquivo.Name,
                        NomeInterno = nomeInterno,
                        Tamanho = arquivo.Size,
                        Tipo = arquivo.Extension,
                        UsuarioUpload = HttpContext.User.Identity.Name,
                        Conteudo = arquivo.BinaryContent,
                        Caminho = Path.Combine("~/Anexos", arquivo.Name),
                        DataArquivo = arquivo.CreationTime.ToString("dd/MM/yyyy"),
                        Notas = listaDeArquivos.Notas
                    };

                    _app.SalvarAnexo(anexo);
               // }
            }
                catch (Exception e)
                {
                    MensagemParaUsuario.MensagemErro("Ocorreu um erro durante o Upload do arquivo ! \n" + e.Message, TempData);
                    throw;
                }
            }

            MensagemParaUsuario.MensagemSucesso("Upload dos arquivos concluídos com sucesso !",TempData);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            Anexo anexo = _app.ObterAnexoPorId(id);
            var dados =  DownloadArquivo(anexo.Conteudo, anexo.NomeDoArquivo);

            return dados;
        }

        private ActionResult DownloadArquivo(byte[] fileBytes, string nomeArquivo)
        {
            try
            {
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nomeArquivo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index");
            }
            
        }

        private ArquivoAnexo SalvarArquivoNoFileSystem(HttpPostedFileBase arquivoSelecionado, string nomeInterno)
        {
            ArquivoAnexo arquivo = new ArquivoAnexo(arquivoSelecionado);

            string pastaDestino;
            string arquivoDestino;
            //string nomeInterno;


            pastaDestino = Server.MapPath("~/Anexos");
            
            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            arquivoDestino = Path.Combine(pastaDestino, nomeInterno);

            arquivoSelecionado.SaveAs(arquivoDestino);

            return arquivo;
        }

    }
}
