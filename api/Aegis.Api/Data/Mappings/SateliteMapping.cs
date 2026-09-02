using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class SateliteMapping : IEntityTypeConfiguration<Satelite>
    {
        public void Configure(EntityTypeBuilder<Satelite> builder)
        {
            builder.ToTable("TB_SATELITE");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName("ID_SATELITE");

            builder.Property(s => s.NumeroSatelite)
                .HasColumnName("NUMERO_SATELITE")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.AltitudeKm)
                .HasColumnName("NUMERO_ALTITUDE_KM");

            builder.Property(s => s.EmpresaId)
                .HasColumnName("ID_EMPRESA");

            // Configuração do Relacionamento (N:1 com Empresa)
            builder.HasOne(s => s.Empresa)
                .WithMany(e => e.Satelites)
                .HasForeignKey(s => s.EmpresaId);

            // Relacionamento com Alertas (1:N)
            // Geralmente definido aqui ou no AlertaColisaoMapping
            builder.HasMany(s => s.Alertas)
                .WithOne(a => a.Satelite)
                .HasForeignKey(a => a.SateliteId);
        }
    }
}