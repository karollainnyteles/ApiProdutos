using ApiDesafio.Business.Models.Produtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Commands.Produtos
{
    public class ProdutoAdicionarCommand : IRequest<Produto>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
