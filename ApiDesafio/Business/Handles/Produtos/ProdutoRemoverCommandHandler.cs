using ApiDesafio.Business.Commands.Produtos;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.ProdutoCompras;
using ApiDesafio.Business.Models.Produtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Handles.Produtos
{
    public class ProdutoRemoverCommandHandler : BaseCommandHandler, IRequestHandler<ProdutoRemoverCommand, string>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;

        public ProdutoRemoverCommandHandler(IProdutoRepository produtoRepository, INotificador notificador, IProdutoCompraRepository produtoCompraRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoCompraRepository = produtoCompraRepository;
        }
        public async Task<string> Handle(ProdutoRemoverCommand request, CancellationToken cancellationToken)
        {
            if(await _produtoCompraRepository.ExisteCompraPorProdutoId(request.Id))
            {
                Notificar("Não é possível excluir um produto que já está incluso em uma compra");
                return "";
            }
            await _produtoRepository.Remover(request.Id);
            return "excluído com sucesso";
        }
    }
}
