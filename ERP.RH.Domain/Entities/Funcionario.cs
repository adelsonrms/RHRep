#region "Namepaces do Sistema"

using System.Collections.Generic;
using ERP.RH.Domain;
using ERP.Shared.ValueObjects;

//Expoe entidades genericas

#endregion

namespace RH.Domain.Entities
{
    /// <summary>
    ///     Representa um Funcionario da empresa.
    ///     Um Funcionario herda de uma pessoa genericamente.
    ///     Para um funcionário, no entanto, é necessário especificar algumas informações adicionais como por ex
    ///     Contatos, Dados Cadastrais
    /// </summary>
    public class Funcionario : EFEntity
    {
        #region propriedades públicas
        //public new virtual int Id { get; set; }
        //public new virtual string Nome { get; set; }

        public virtual string Matricula { get; set; }
        public virtual string CPF { get; set; }
        public virtual string PIS { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Celular { get; set; }
        public virtual string EmailProfissional { get; set; }
        public virtual string EmailPessoal { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string CEP { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Estado { get; set; }
        public virtual string DataNascimento { get; set; }
        public virtual int SexoId { get; set; }
        public string Idade { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public Nome NomeDoFuncionario { get => new Nome(this.Nome); }
        #endregion
    }

}