using ApiDesafio.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.Compras
{
    public interface ICompraRepository : IRepository<Compra>
    {
        Task<List<Compra>> ObterTodosComProdutos();
        Task<Compra> ObterComProdutosPorId(int compraId);
    }
}
