using ApiDesafio.Business.Commands.Compras;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Compras;
using ApiDesafio.Business.Models.ProdutoCompras;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Handles.Compras
{
    public class CompraRemoverCommandHandler : BaseCommandHandler, IRequestHandler<CompraRemoverCommand, Compra>
    {

        private readonly ICompraRepository _compraRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;


        public CompraRemoverCommandHandler(INotificador notificador, ICompraRepository compraRepository, IProdutoCompraRepository produtoCompraRepository) : base(notificador)
        {
            _compraRepository = compraRepository;
            _produtoCompraRepository = produtoCompraRepository;
        }

        public async Task<Compra> Handle(CompraRemoverCommand request, CancellationToken cancellationToken)
        {
            var compra = await _compraRepository.ObterComProdutosPorId(request.Id);

            foreach (var produtoCompra in compra.ProdutoCompras)
            {
                await _produtoCompraRepository.Remover(produtoCompra.Id);
            }

            await _compraRepository.Remover(request.Id);
            
            return compra;
        }
    }
}
