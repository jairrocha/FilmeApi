using AutoMapper;
using FilmeAPI.Data;
using FilmeAPI.Data.Dtos;
using FilmeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {

        private FilmeContext _context;
        private IMapper _mapper;

        /*
         * IActionResult utilizado nas respostas http exemplo: (200, 404, 201, ...)
         */

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {


            /*
             * Quando criamos um recurso novo no sistema através do verbo POST, a convenção 
             * do que deve ser retornado caso a requisição tenha sido efetuada com sucesso
             * é: 201 (Created) e a localização de onde o recurso pode ser acessado no nosso sistema.
             * 
             * O retorno atende essa convenção.
             */



            // a extenssão Mapper faz o trabalho abaixo
            //Filme filme = new Filme
            //{

            //    Titulo = filmeDto.Titulo,
            //    Genero = filmeDto.Genero,
            //    Diretor = filmeDto.Diretor,
            //    Duracao = filmeDto.Duracao
            //};

            Filme filme = _mapper.Map<Filme>(filmeDto);

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

            if (filme != null)
            {

                // a extenssão Mapper faz o trabalho abaixo
                //ReadFilmeDto filmeDto = new ReadFilmeDto
                //{
                //    Titulo = filme.Titulo,
                //    Diretor = filme.Diretor,
                //    Genero = filme.Genero,
                //    Duracao = filme.Duracao,
                //    HoraDaConsulta = DateTime.Now /*Vantagem do padrão DTO, podemos adicionar informação*/

                //};

                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                filmeDto.HoraDaConsulta = DateTime.Now; /*Vantagem do padrão DTO, podemos adicionar informação*/

                return Ok(filmeDto); /*Retorno ok 200*/
            }
            return NotFound(); /*Retorno not found (404)*/

        }

        [HttpPut("{id}")] //Verbo http de Atualização
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            // a extenssão Mapper faz o trabalho abaixo
            //filme.Titulo = filmeDto.Titulo;
            //filme.Diretor = filmeDto.Diretor;
            //filme.Genero = filmeDto.Genero;
            //filme.Duracao = filmeDto.Duracao;

            _mapper.Map(filmeDto, filme);

            _context.SaveChanges();

            //No HttpPut a boa prática é não retornar nada
            return NoContent();

        }

        [HttpDelete("{id}")] //Verbo Htpp de remoção
        public IActionResult RemoveFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            //No HttpDelete a boa prática é não retornar nada
            return NoContent();

        }

      
    }
}
