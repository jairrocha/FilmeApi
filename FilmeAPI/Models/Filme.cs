﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeAPI.Models
{
    public class Filme
    {

        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required (ErrorMessage ="O campo Titulo é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="O campo titulo é obrigatório")]
        public string Diretor { get; set; }
        
        [StringLength(30,ErrorMessage ="O campo genero não pode ter mais de 30 caracteres")]
        public string Genero { get; set; }
        [Required]
        [Range(1,600,ErrorMessage ="A duração deve ter no mínimo 1 e no máximo 600")]
        public int Duracao { get; set; }
    }
}
