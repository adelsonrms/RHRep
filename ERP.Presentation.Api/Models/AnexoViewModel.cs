using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Presentation.Api.Models
{
    public class AnexoViewModel
    {
        [AllowHtml] /*Essa decoração permite que o conteudo possua Tags HTML. Caso nao seja incluido, o ASP NET bloqueia por motivo de segurança*/
        public string Notas { get; set; }
        public bool SalvarNoBd { get; set; }
        public bool SalvarNoFileSystem { get; set; }
        public IEnumerable<HttpPostedFileBase> Arquivos { get; set; }

    }

    public class ArquivoAnexo
    {

        public ArquivoAnexo(HttpPostedFileBase arquivo)
        {
            DefinirInfoArquivo(arquivo);
        }

        private void DefinirInfoArquivo(HttpPostedFileBase arquivo)
        {
            string filePath = arquivo.FileName;
            try
            {
                
                var arrPath = filePath.Split("\\".ToCharArray());

                Name = arrPath[arrPath.Length - 1];
                FullName = filePath;
                FolderPath = "()";//Path.GetDirectoryName(filePath);
                Extension = ObtemExtensaoDoArquivo(filePath);//Path.GetExtension(filePath);
                //CreationTime =File.GetCreationTime(filePath);
            }
            catch (Exception exception)
            {
                return;
                throw new Exception("Ocorreu um erro ao tentar obter informações sobre o arquivo " + filePath + 
                                    "\n Erro Interno : " + exception.Message + "\n Pilha de Chamadas : " + exception.StackTrace) ;
            }

       


        }

        private string ObtemExtensaoDoArquivo(string filePath)
        {
            var parhRev = InverteString(filePath);
            return InverteString(parhRev.Substring(0, parhRev.IndexOf(".", StringComparison.Ordinal)));
        }


        private string InverteString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }


        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string FolderPath { get; private set; }
        public int Size { get;  set; }
        public string Extension { get; private set; }
        public byte[] BinaryContent { get;  set; }
        public DateTime CreationTime { get; private set; }

    }
}