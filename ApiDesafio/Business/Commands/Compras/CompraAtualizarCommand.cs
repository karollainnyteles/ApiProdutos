using ApiDesafio.Business.Models.Compras;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Commands.Compras
{
    public class CompraAtualizarCommand : IRequest<string>
    {
        public int CompraId { get; set; }
        public List<int> ProdutosIdRemover { get; set; }
        public List<int> ProdutosIdAdicionar { get; set; }
    }
}
