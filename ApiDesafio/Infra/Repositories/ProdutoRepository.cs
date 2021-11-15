using ApiDesafio.Business.Models.Produtos;
using ApiDesafio.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Infra.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext meuDbContext) : base(meuDbContext)
        {
        }
    }
}
