using ApiDesafio.Business.Models.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ApiDesafio.Infra.Data.Mappings
{
    public class CompraConfig : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Total).IsRequired();

            builder.ToTable("Compras");
          
        }
    }
}
