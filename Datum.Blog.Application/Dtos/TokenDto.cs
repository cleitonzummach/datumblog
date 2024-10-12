using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Dtos
{
    public class TokenDto
    {
        [Required(ErrorMessage = "O e-mail do usuário é obrigatório.")]
        [StringLength(256, ErrorMessage = "O e-mail do usuário não pode ter mais que 256 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória.")]
        [StringLength(50, ErrorMessage = "A senha do usuário não pode ter mais que 50 caracteres.")]
        public string Senha { get; set; }
    }
}
