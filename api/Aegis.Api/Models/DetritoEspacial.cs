using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aegis.Api.Models; // Importante se houver relacionamentos

namespace Aegis.Api.Models
{
    [Table("TB_DETRITO_ESPACIAL")]
    public class DetritoEspacial
    {
        [Key]
        [Column("ID_DETRITO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOME_DETRITO")]
        public string Nome { get; set; } = string.Empty;

        [Column("MASSA_KG")]
        public decimal MassaKg { get; set; }

        [Column("TAMANHO_METROS")]
        public decimal TamanhoMetros { get; set; }

        [Column("TXT_COORDENADA_X")]
        public decimal CoordenadaX { get; set; }

        [Column("TXT_COORDENADA_Y")]
        public decimal CoordenadaY { get; set; }

        [Column("TXT_COORDENADA_Z")]
        public decimal CoordenadaZ { get; set; }

        // Relação 1:N com Alertas
        public virtual ICollection<AlertaColisao>? Alertas { get; set; }
    }
}