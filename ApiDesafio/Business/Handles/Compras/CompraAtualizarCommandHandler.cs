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
    public class CompraAtualizarCommandHandler : BaseCommandHandler, IRequestHandler<CompraAtualizarCommand, string>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;


        public CompraAtualizarCommandHandler(IProdutoRepository produtoRepository, INotificador notificador, ICompraRepository compraRepository, IProdutoCompraRepository produtoCompraRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _compraRepository = compraRepository;
            _produtoCompraRepository = produtoCompraRepository;
        }

        public async Task<string> Handle(CompraAtualizarCommand request, CancellationToken cancellationToken)
        {
            var CompraAtual = await _compraRepository.ObterComProdutosPorId(request.CompraId);
            foreach (var idRemover in request.ProdutosIdRemover)
            {
                var produtoCompra = CompraAtual.ProdutoCompras.FirstOrDefault(a => a.ProdutoId == idRemover);
                if (produtoCompra != null)
                    await _produtoCompraRepository.Remover(idRemover);
            }

            foreach (var idAdicionar in request.ProdutosIdAtualizar)
            {
               var produto = await _produtoRepository.ObterPorId(idAdicionar);
                if (produto != null)
                {
                    var produtoCompra = new ProdutoCompra();
                    produtoCompra.CompraId = request.CompraId;
                    produtoCompra.ProdutoId = idAdicionar;
                    await _produtoCompraRepository.Adicionar(produtoCompra);
                }
            }


            var compraAtualizada = await _compraRepository.ObterComProdutosPorId(request.CompraId);
            decimal total = 0;
            foreach (var produtoCompra in compraAtualizada.ProdutoCompras)
            {

                total += produtoCompra.Produto.Price;
            }

            compraAtualizada.Total = total;
            await _compraRepository.Atualizar(compraAtualizada);
            return "Alterações possíveis foram concluídas";
        }
    }
}
