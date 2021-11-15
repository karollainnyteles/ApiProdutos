using ApiDesafio.Business.Commands.Compras;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Compras;
using ApiDesafio.Business.Models.ProdutoCompras;
using ApiDesafio.Business.Models.Produtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Handles.Compras
{
    public class CompraAdicionarCommandHandler : BaseCommandHandler, IRequestHandler<CompraAdicionarCommand, Compra>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;


        public CompraAdicionarCommandHandler(IProdutoRepository produtoRepository, INotificador notificador, ICompraRepository compraRepository, IProdutoCompraRepository produtoCompraRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _compraRepository = compraRepository;
            _produtoCompraRepository = produtoCompraRepository;
        }
        public async Task<Compra> Handle(CompraAdicionarCommand request, CancellationToken cancellationToken)
        {
            foreach (var produtoId in request.ListaProdutosId)
            {
                var existe = await _produtoRepository.Existe(produtoId);
                if (!existe)
                {
                    Notificar($"O produto {produtoId} não existe");
                }
            }

            if (_notificador.TemNotificacao())
            {
                return null;
            }



            decimal total = 0;
            foreach (var produtoId in request.ListaProdutosId)
            {
                var produto = await _produtoRepository.ObterPorId(produtoId);
                total += produto.Price;
            }


            var compra = new Compra();
            compra.Total = total;
            await _compraRepository.Adicionar(compra);

            foreach (var produtoId in request.ListaProdutosId)
            {
                var produtoCompra = new ProdutoCompra();
                produtoCompra.CompraId = compra.Id;
                produtoCompra.ProdutoId = produtoId;
                await _produtoCompraRepository.Adicionar(produtoCompra);
            }

            return compra;

        }
    }
}
