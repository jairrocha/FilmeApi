using FilmeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {

        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        /*
         * IActionResult utilizado nas respostas http exemplo: (200, 404, 201, ...)
         */


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);

           /*
            * Quando criamos um recurso novo no sistema através do verbo POST, a convenção 
            * do que deve ser retornado caso a requisição tenha sido efetuada com sucesso
            * é: 201 (Created) e a localização de onde o recurso pode ser acessado no nosso sistema.
            * 
            * O retorno atende essa convenção.
            */
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { ID = filme.Id }, filme);

        }
        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(filmes); /*Retorno ok 200*/
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            
           Filme filme = filmes.FirstOrDefault(f => f.Id == id);

            if (filme!=null)
            {
                return Ok(filme); /*Retorno ok 200*/
            }
            return NotFound(); /*Retorno not found (404)*/

        }
    }
}
