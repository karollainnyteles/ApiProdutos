using ApiDesafio.Business.Models.Compras;
using ApiDesafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Infra.Repositories
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(MeuDbContext meuDbContext) : base(meuDbContext)
        {
        }

        public async Task<Compra> ObterComProdutosPorId(int compraId)
        {
            return await Db.Compras
                .Include(c => c.ProdutoCompras)
                .ThenInclude(p => p.Produto)
                .AsNoTracking()
                .FirstOrDefaultAsync( p => p.Id == compraId);
        }

        public async Task<List<Compra>> ObterTodosComProdutos()
        {
            return await Db.Compras.Include(c => c.ProdutoCompras).ThenInclude(p=> p.Produto).AsNoTracking().ToListAsync();
        }
    }
}
