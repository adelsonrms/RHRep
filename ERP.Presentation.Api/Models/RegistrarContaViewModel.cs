using System.ComponentModel.DataAnnotations;

namespace ERP.Presentation.Api.Models
{
    public class RegistrarContaViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} senha deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conformar Senha")]
        [Compare("Senha", ErrorMessage = "A confirmação da senhañão confere.")]
        public string ConfirmarSenha { get; set; }
    }

}