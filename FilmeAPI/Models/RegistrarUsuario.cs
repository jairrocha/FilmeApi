using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models
{
    public class RegistrarUsuario
    {
        [EmailAddress(ErrorMessage ="O Campo {0} está em formato inválido")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage ="O Campo {0} precisa ter no minímo 6 carateres.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="As senha não conferem.")]
        public string ComfirmPassword { get; set; }
    }
}
