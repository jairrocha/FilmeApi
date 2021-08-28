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

        [HttpPost]
        public void AdicionaFilme([FromBody]Filme filme)
        {

            filmes.Add(filme);

        }
    }
}
