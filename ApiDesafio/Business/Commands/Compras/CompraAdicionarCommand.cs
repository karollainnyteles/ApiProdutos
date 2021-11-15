using ApiDesafio.Business.Models.Compras;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Commands.Compras
{
    public class CompraAdicionarCommand : IRequest<Compra>
    {
        public List<int> ListaProdutosId { get; set; }
    }
}
