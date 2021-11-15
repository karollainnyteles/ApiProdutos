using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Commands.Produtos
{
    public class ProdutoRemoverCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
