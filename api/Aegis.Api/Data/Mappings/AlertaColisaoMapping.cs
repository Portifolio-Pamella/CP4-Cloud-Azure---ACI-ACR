using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class AlertaColisaoMapping : IEntityTypeConfiguration<AlertaColisao>
    {
        public void Configure(EntityTypeBuilder<AlertaColisao> builder)
        {
            builder.ToTable("TB_ALERTA_COLISAO");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("ID_ALERTA");

            builder.Property(a => a.SateliteId)
                .HasColumnName("ID_SATELITE")
                .IsRequired();

            builder.Property(a => a.DetritoId)
                .HasColumnName("ID_DETRITO")
                .IsRequired();

            builder.Property(a => a.StatusGravidade)
                .HasColumnName("STATUS_GRAVIDADE")
                .HasMaxLength(20)
                .IsRequired();

            // Relacionamentos
            builder.HasOne(a => a.Satelite)
                .WithMany(s => s.Alertas)
                .HasForeignKey(a => a.SateliteId);

            builder.HasOne(a => a.Detrito)
                .WithMany() // Assumindo que DetritoEspacial não tem uma lista de Alertas
                .HasForeignKey(a => a.DetritoId);
        }
    }
}