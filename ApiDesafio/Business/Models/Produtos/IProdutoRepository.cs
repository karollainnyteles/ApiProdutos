using ApiDesafio.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<bool> Existe(int produtoId);
    }
}
