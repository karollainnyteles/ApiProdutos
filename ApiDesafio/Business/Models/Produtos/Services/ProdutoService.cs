using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Core.Services;
using ApiDesafio.Business.Models.ProdutoCompras;
using ApiDesafio.Business.Models.Produtos.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.Produtos.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IProdutoCompraRepository produtoCompraRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoCompraRepository = produtoCompraRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (ExecutarValidacao(new ProdutoValidation(), produto))
                await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (ExecutarValidacao(new ProdutoValidation(), produto))
            {
                var produtoAtualizacao = await _produtoRepository.ObterPorId(produto.Id);

                produtoAtualizacao.Name = produto.Name;
                produtoAtualizacao.Price = produto.Price;
                produtoAtualizacao.Active = produto.Active;

                await _produtoRepository.Atualizar(produtoAtualizacao);
            }
                
        }

        public async Task Remover(int id)
        {
            if (await _produtoCompraRepository.ExisteCompraPorProdutoId(id))
            {
                Notificar("Não é possível excluir um produto que já está incluso em uma compra");
                return;
            }
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
