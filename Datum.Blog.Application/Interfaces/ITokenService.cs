using Datum.Blog.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Blog.Application.Interfaces
{
    public interface ITokenService
    {
        public Task<string> RetornarToken(TokenDto tokenDto);
    }
}
