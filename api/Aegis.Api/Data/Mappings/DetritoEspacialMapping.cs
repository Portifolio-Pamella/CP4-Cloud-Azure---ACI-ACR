using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class DetritoEspacialMapping : IEntityTypeConfiguration<DetritoEspacial>
    {
        public void Configure(EntityTypeBuilder<DetritoEspacial> builder)
        {
            builder.ToTable("TB_DETRITO_ESPACIAL");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("ID_DETRITO");

            // Assumindo as propriedades padrões de detritos
            builder.Property(d => d.Nome)
                .HasColumnName("NOME_DETRITO")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.MassaKg)
    .HasColumnName("MASSA_KG")
    .IsRequired()
    .HasPrecision(18, 4); // <--- Adicione isso! (18 total de dígitos, 4 decimais)

            // Se houver outras propriedades como altitude ou data de descoberta, 
            // basta seguir o padrão abaixo:
            // builder.Property(d => d.Propriedade).HasColumnName("NOME_COLUNA_BANCO");
        }
    }
}