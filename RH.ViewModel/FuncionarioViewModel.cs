using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ERP.Shared.ValueObjects;

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
        public  string PIS { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\([0-9]{2}\))\s([0-9]{4})-([0-9]{4,5})$", ErrorMessage = "Telefone Inválido")]
        public  string Telefone { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\([0-9]{2}\))\s([0-9]{4})-([0-9]{4,5})$", ErrorMessage = "Telefone Inválido")]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DataNascimento { get; set; }
        [Display(Name = "Sexo")]
        public  int SexoId { get; set; }
        public string Idade { get; set; }
        public  bool Ativo { get; set; }
        public  ContratoViewModel Contrato { get; set; }
        public  DocumentoViewModel Documento { get; set; }
        public  EstadoCivilViewModel EstadoCivil { get; set; }
        public Nome NomeDoFuncionario { get => new Nome(this.Nome); }
    }


}
