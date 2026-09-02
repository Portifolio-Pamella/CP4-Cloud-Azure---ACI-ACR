using Microsoft.EntityFrameworkCore;
using Aegis.Api.Models;
using Aegis.Api.Data.Mappings; // Importante: importar o namespace dos mappings

namespace Aegis.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabelas do Banco de Dados
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Satelite> Satelites { get; set; }
        public DbSet<DetritoEspacial> Detritos { get; set; }
        public DbSet<Chaser> Chasers { get; set; }
        public DbSet<AlertaColisao> AlertasColisao { get; set; }
        public DbSet<MissaoInterceptacao> Missoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- APLICAÇÃO DOS MAPPINGS ---
            // Ao invés de configurar aqui dentro, chamamos cada classe de Mapping
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            modelBuilder.ApplyConfiguration(new SateliteMapping());
            modelBuilder.ApplyConfiguration(new DetritoEspacialMapping());
            modelBuilder.ApplyConfiguration(new ChaserMapping());
            modelBuilder.ApplyConfiguration(new AlertaColisaoMapping());
            modelBuilder.ApplyConfiguration(new MissaoInterceptacaoMapping());
        }
    }
}