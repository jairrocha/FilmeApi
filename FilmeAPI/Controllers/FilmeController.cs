using FilmeAPI.Data;
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

        private FilmeContext _context;


        /*
         * IActionResult utilizado nas respostas http exemplo: (200, 404, 201, ...)
         */

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {


            /*
             * Quando criamos um recurso novo no sistema através do verbo POST, a convenção 
             * do que deve ser retornado caso a requisição tenha sido efetuada com sucesso
             * é: 201 (Created) e a localização de onde o recurso pode ser acessado no nosso sistema.
             * 
             * O retorno atende essa convenção.
             */
            _context.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { ID = filme.Id }, filme);

        }
        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes); /*Retorno ok 200*/
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            
           Filme filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme!=null)
            {
                return Ok(filme); /*Retorno ok 200*/
            }
            return NotFound(); /*Retorno not found (404)*/

        }
    }
}
