//using RH.Domain.Entities;
//using System.ComponentModel.DataAnnotations;

//namespace ERP.Presentation.Api.Models
//{
//    public class FuncionarioViewModel
//    {
//        [Key]
//        public int Id { get; set; }
//        public virtual Contrato Contrato { get; set; }
//        public virtual string Matricula { get; set; }
//        public virtual string CPF { get; set; }
//        public virtual string PIS { get; set; }
//        public virtual string Telefone { get; set; }
//        [Phone]
//        [DataType(DataType.PhoneNumber)]
//        [RegularExpression(@"^(\([0-9]{2}\))\s([0-9]{4})-([0-9]{4,5})$", ErrorMessage = "Telefone Inválido")]
//        public virtual string Celular { get; set; }
//        [EmailAddress]
//        [Display(Name = "E-mail no ambiente do cliente")]
//        public virtual string EmailProfissional { get; set; }
//        public virtual string EmailPessoal { get; set; }
//        public virtual string Endereco { get; set; }
//        public virtual string CEP { get; set; }
//        public virtual string Bairro { get; set; }
//        public virtual string Cidade { get; set; }
//        public virtual string Estado { get; set; }
//        public virtual string DataNascimento { get; set; }
//        public virtual int SexoId { get; set; }
//        public string Idade { get; set; }
//        public virtual bool Ativo { get; set; }

//    }
//}