using ApiDesafio.Business.Models.ProdutoCompras;
using ApiDesafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Infra.Repositories
{
    public class ProdutoCompraRepository : Repository<ProdutoCompra>, IProdutoCompraRepository
    {
        public ProdutoCompraRepository(MeuDbContext meuDbContext) : base(meuDbContext)
        {
        }

        public async Task<bool> ExisteCompraPorProdutoId(int produtoId)
        {
            return await Db.ProdutoCompra.AnyAsync(p => p.ProdutoId == produtoId);
        }
    }
}
