using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Dtos
{
    public class CriarPublicacaoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(500, ErrorMessage = "O título não pode ter mais que 500 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; set; }
    }
}
