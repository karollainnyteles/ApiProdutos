using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDesafio.Business.Core.Models;
using ApiDesafio.Business.Models.ProdutoCompras;

namespace ApiDesafio.Business.Models.Compras
{
    public class Compra : Entity
    {
        public decimal Total { get; set; }

        //EF Core relations
        public List<ProdutoCompra> ProdutoCompras { get; set; }
    }
}
