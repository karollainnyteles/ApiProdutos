using ApiDesafio.Business.Core.Data;
using ApiDesafio.Business.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.ProdutoCompras
{
    public interface IProdutoCompraRepository : IRepository<ProdutoCompra>
    {
        Task<bool> ExisteCompraPorProdutoId(int produtoId);
    }
}
