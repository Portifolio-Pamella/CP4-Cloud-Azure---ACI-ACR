using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class MissaoInterceptacaoMapping : IEntityTypeConfiguration<MissaoInterceptacao>
    {
        public void Configure(EntityTypeBuilder<MissaoInterceptacao> builder)
        {
            builder.ToTable("TB_MISSAO_INTERCEPTACAO");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("ID_MISSAO");

            builder.Property(m => m.AlertaId).HasColumnName("ID_ALERTA").IsRequired();
            builder.Property(m => m.ChaserId).HasColumnName("ID_CHASER").IsRequired();

            builder.Property(m => m.DataExecucao).HasColumnName("DATA_EXECUCAO");

            builder.Property(m => m.Status)
                .HasColumnName("STATUS_MISSAO")
                .HasMaxLength(20)
                .IsRequired();

            // Configuração dos relacionamentos
            builder.HasOne(m => m.Chaser)
            .WithMany(c => c.Missoes)
            .HasForeignKey(m => m.ChaserId); // O EF vai parar de criar o ChaserId1

            builder.HasOne(m => m.Chaser)
    .WithMany(c => c.Missoes)
    .HasForeignKey(m => m.ChaserId) // <--- Garanta que você está usando a FK explícita
    .IsRequired();
        }
    }
}