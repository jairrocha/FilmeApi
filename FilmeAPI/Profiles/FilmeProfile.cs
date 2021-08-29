using AutoMapper;
using FilmeAPI.Data.Dtos;
using FilmeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeAPI.Profiles
{
    public class FilmeProfile:Profile
    {
        /*
         * Utizado pela exetensão: AutoMapper
         * Extensão faz o mapeamento entre classes
         */

        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
            CreateMap<UpdateFilmeDto, Filme>();

        }
    }
}
