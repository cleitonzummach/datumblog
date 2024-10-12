using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Dtos
{
    public class EditarPublicacaoDto
    {
        [Required(ErrorMessage = "O id da publicação é obrigatório.")]
        public int PublicacaoId { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }
    }
}
