using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Api.Models
{

    [Table("TB_CHASER")]
    public class Chaser
    {
        [Key]
        [Column("ID_CHASER")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NOME_CHASER")]
        [MaxLength(50)]
        public string Nome { get; set; } = string.Empty; // <- Adicione o "= string.Empty;

        [Column("NUM_BATERIA")]
        public decimal Bateria { get; set; }

        [Column("TXT_COORDENADA_X")]
        public decimal CoordenadaX { get; set; }

        [Column("TXT_COORDENADA_Y")]
        public decimal CoordenadaY { get; set; }

        [Column("TXT_COORDENADA_Z")]
        public decimal CoordenadaZ { get; set; }

        // Relação 1:N com Missões
        public ICollection<MissaoInterceptacao> Missoes { get; set; } = new List<MissaoInterceptacao>(); // <- Inicialize a lista
    }
}