using ApiDesafio.Business.Commands.Produtos;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Produtos;
using ApiDesafio.Business.Models.Produtos.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Handles.Produtos
{
    public class ProdutoAtualizarCommandHandler : BaseCommandHandler, IRequestHandler<ProdutoAtualizarCommand, string>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAtualizarCommandHandler(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<string> Handle(ProdutoAtualizarCommand request, CancellationToken cancellationToken)
        {
            var validacao = new ProdutoValidation();
            Produto produto = new Produto();
            produto.Id = request.Id;
            produto.Name = request.Name;
            produto.Price = request.Price;
            produto.Active = request.Active;

            var validator = validacao.Validate(produto);
            if (validator.IsValid)
            {
                await _produtoRepository.Atualizar(produto);
                return "Alterado com sucesso";
            }

            Notificar(validator);
            return "";
        }
    }
}
