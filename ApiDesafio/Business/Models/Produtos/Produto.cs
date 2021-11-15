using ApiDesafio.Business.Core.Models;
using ApiDesafio.Business.Models.ProdutoCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.Produtos
{
    public class Produto : Entity
    {
        public Produto()
        {
            Active = true;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        //EF Core relations
        public List<ProdutoCompra> ProdutoCompras { get; set; }

    }
}
