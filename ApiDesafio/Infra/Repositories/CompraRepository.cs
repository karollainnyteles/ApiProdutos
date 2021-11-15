using ApiDesafio.Business.Models.Compras;
using ApiDesafio.Infra.Data.Context;
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
    }
}
