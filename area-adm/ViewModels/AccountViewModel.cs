using System.ComponentModel.DataAnnotations;

namespace area_adm.ViewModels
{
    public class AccountViewModel
    {

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display (Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [DataType  (DataType.EmailAddress)]
        [Display (Name = "Email")]
        public string Email { get; set; }

        public bool LembrarMe { get; set; } 
        public string ReturnUrl { get; set; }
    }
}
