using ApiDesafio.Business.Commands.Produtos;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Produtos;
using ApiDesafio.Business.Models.Produtos.Validations;
using ApiDesafio.Infra.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Handles.Produtos
{
    public class ProdutoAdicionarCommandHandler : BaseCommandHandler, IRequestHandler<ProdutoAdicionarCommand, Produto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAdicionarCommandHandler(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> Handle(ProdutoAdicionarCommand request, CancellationToken cancellationToken)
        {
            var validacao = new ProdutoValidation();
            Produto produto = new Produto();

            produto.Name = request.Name;
            produto.Price = request.Price;

            var validator = validacao.Validate(produto);
            if (validator.IsValid)
            {
                await _produtoRepository.Adicionar(produto);
                return produto;
            }

            Notificar(validator);
            return null;

        }

    }
}
