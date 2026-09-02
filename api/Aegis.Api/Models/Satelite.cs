using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Api.Models
{

    [Table("TB_SATELITE")]
    public class Satelite
    {
        [Key]
        [Column("ID_SATELITE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NUMERO_SATELITE")]
        public string NumeroSatelite { get; set; } = string.Empty;

        [Column("NUMERO_ALTITUDE_KM")]
        public decimal AltitudeKm { get; set; }

        [ForeignKey("Empresa")]
        [Column("ID_EMPRESA")]
        public int EmpresaId { get; set; }

        // Navigation Property
        public virtual Empresa? Empresa { get; set; }

        // Relação 1:N com Alertas
        public virtual ICollection<AlertaColisao>? Alertas { get; set; }
    }
}