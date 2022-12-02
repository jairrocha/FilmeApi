using FilmeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {


        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegistrarUsuario registrarUsuario)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = registrarUsuario.Email,
                Email = registrarUsuario.Email,
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(user, registrarUsuario.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok();

        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUsuario loginUsuario)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUsuario.Email, loginUsuario.Password, false, true);

            if (!result.Succeeded) return BadRequest("Usuário ou senha inválido");

            return Ok();

        }

 
        
    }
}
