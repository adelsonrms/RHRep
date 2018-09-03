using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ERP.Shared.ValueObjects;
using RH.Domain.Entities;

namespace RH.ViewModel
{
   public class FuncionarioViewModel
    {
        public  int Id { get; set; }

        [Required(ErrorMessage="Informe o nome do funcionario")]
        [MaxLength(150, ErrorMessage="Maximo de {0} caracteres")]
        [MinLength(3, ErrorMessage = "Numero minimo de {0} caracteres")]
        [DisplayName("Nome do Funcionario")]
        public  string Nome { get; set; }

        [Required(ErrorMessage = "Número da matricula obrigatorio")]
        [MaxLength(6, ErrorMessage = "Maximo de {0} caracteres")]
        [DisplayName("Nº da Matricula")]
        public  string Matricula { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.###.###-##}")]
        public  string CPF { get; set; }

        [DisplayName("Numero do PIS/PASEP")]
        public  string PIS { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$", ErrorMessage = "O Numero do Telefone é Inválido")]
        public  string Telefone { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$", ErrorMessage = "O Numero do Celular é Inválido")]
        public  string Celular { get; set; }

        [DisplayName("Email/Login")]
        [Required(ErrorMessage = "Informe o email do funcionario")]
        [EmailAddress]
        [RegularExpression(@".+\@tecnun\.com\.br$", ErrorMessage = "Somente endereços tecnun.com.br são validos nesse campo")]
        public  string EmailProfissional { get; set; }

        [DisplayName("Email pessoal")]
        [EmailAddress]
        public  string EmailPessoal { get; set; }

        public  string Endereco { get; set; }
        public  string CEP { get; set; }
        public  string Bairro { get; set; }
        public  string Cidade { get; set; }
        [MaxLength(2, ErrorMessage = "Maximo de {0} caracteres para o estado")]
        public  string Estado { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }


        [Display(Name = "Sexo")]
        public  int SexoId { get; set; }
        public string Idade { get; set; }
        public  bool Ativo { get; set; }
        public Contrato Contrato { get; set; }
        public Documento Documento { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public Nome NomeDoFuncionario { get => new Nome(this.Nome); }
    }


}
