using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Api.Models
{

    [Table("TB_EMPRESA_AEROESPACIAL")]
    public class Empresa
    {
        [Key]
        [Column("ID_EMPRESA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] // Garante NOT NULL no banco
        [StringLength(100)]
        [Column("NOME_EMPRESA")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("CNPJ_ID_EMPRESA")]
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("PAIS_ORIGEM")]
        public string PaisOrigem { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("STATUS_OPERACAO")]
        public string Status { get; set; } = "ATIVO";

        public virtual ICollection<Satelite>? Satelites { get; set; } = new List<Satelite>();
    }
}