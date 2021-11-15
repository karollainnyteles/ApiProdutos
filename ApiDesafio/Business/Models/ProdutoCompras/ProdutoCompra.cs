using ApiDesafio.Business.Core.Models;
using ApiDesafio.Business.Models.Compras;
using ApiDesafio.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiDesafio.Business.Models.ProdutoCompras
{
    public class ProdutoCompra : Entity
    {
        public int ProdutoId { get; set; }
        public int CompraId { get; set; }

        public Produto Produto { get; set; }
        public Compra Compra { get; set; }


    }
}
