using System;

namespace RH.Domain.Entities
{
    public class Anexo
    {
        public int Id { get; set; }
        public string Caminho { get; set; }
        public string NomeDoArquivo { get; set; }
        public int Tamanho { get; set; }
        public string Tipo { get; set; }
        public DateTime DataUpload { get; set; }
        public string UsuarioUpload { get; set; }
        public string Notas { get; set; }
        public byte[] Conteudo { get; set; }

        public string DataArquivo { get; set; }
        public string CaminhoOrigem { get; set; }
        public string NomeInterno { get; set; }
    }
}