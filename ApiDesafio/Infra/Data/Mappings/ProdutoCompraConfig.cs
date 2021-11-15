using ApiDesafio.Business.Models.ProdutoCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Infra.Data.Mappings
{
    public class ProdutoCompraConfig : IEntityTypeConfiguration<ProdutoCompra>
    {
        public void Configure(EntityTypeBuilder<ProdutoCompra> builder)
        {
            builder.HasOne(p => p.Produto).WithMany(p => p.ProdutoCompras).HasForeignKey(p => p.ProdutoId);

            builder.HasOne(p => p.Compra).WithMany(p => p.ProdutoCompras).HasForeignKey(p => p.CompraId);

            builder.ToTable("ProdutoCompras");
        }
    }
}
