using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class ChaserMapping : IEntityTypeConfiguration<Chaser>
    {
        public void Configure(EntityTypeBuilder<Chaser> builder)
        {
            builder.ToTable("TB_CHASER");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("ID_CHASER");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME_CHASER")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Bateria)
                .HasColumnName("NUM_BATERIA");

            builder.Property(c => c.CoordenadaX)
    .HasColumnName("COORDENADA_X")
    .HasPrecision(18, 4); // Adicione isso

            builder.Property(c => c.CoordenadaX)
    .HasColumnName("COORDENADA_Y")
    .HasPrecision(18, 4); // Adicione isso

            builder.Property(c => c.CoordenadaX)
    .HasColumnName("COORDENADA_Z")
    .HasPrecision(18, 4); // Adicione isso
            

            // Relacionamento 1:N com Missões (Um Chaser pode realizar várias missões)
            builder.HasMany(c => c.Missoes)
                .WithOne(m => m.Chaser)
                .HasForeignKey(m => m.ChaserId);
        }
    }
}