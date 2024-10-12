using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Dtos
{
    public class ConsultarPublicacaoRetornoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string DataPublicacao { get; set; }
        public string UsuarioPublicacao { get; set; }
        public string EmailUsuarioPublicacao { get; set; }

        public ConsultarPublicacaoRetornoDto(int publicacaoId, string titulo, string conteudo, DateTime dataPublicacao, string usuarioPublicacao, string emailUsuarioPublicacao)
        {
            Id = publicacaoId;
            Titulo = titulo;
            Conteudo = conteudo;
            DataPublicacao = dataPublicacao.ToString("dd/MM/yyyy HH:mm");
            UsuarioPublicacao = usuarioPublicacao;
            EmailUsuarioPublicacao = emailUsuarioPublicacao;
        }
    }
}
