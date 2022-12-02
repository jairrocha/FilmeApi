using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models
{
    public class LoginUsuario
    {
        [EmailAddress(ErrorMessage = "O Campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="O Campo {0} é obrigatório")]
        public string Password { get; set; }
    }
}
