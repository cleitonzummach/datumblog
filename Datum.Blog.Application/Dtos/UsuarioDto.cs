using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Dtos
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário não pode ter mais que 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail do usuário é obrigatório.")]
        [StringLength(256, ErrorMessage = "O e-mail do usuário não pode ter mais que 256 caracteres.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória.")]
        [StringLength(50, ErrorMessage = "A senha do usuário não pode ter mais que 50 caracteres.")]
        public string Senha { get; set; }
    }
}
