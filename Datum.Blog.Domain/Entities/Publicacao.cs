using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Domain.Entities
{
    public class Publicacao
    {
        public int PublicacaoId { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public Publicacao(string titulo, string conteudo, int usuarioId)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            Data = DateTime.Now;
            UsuarioId = usuarioId;
        }
    }
}
