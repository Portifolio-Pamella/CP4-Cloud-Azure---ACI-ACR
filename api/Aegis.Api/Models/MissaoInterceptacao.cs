using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Api.Models
{
    [Table("TB_MISSAO_INTERCEPTACAO")]
    public class MissaoInterceptacao
    {
        [Key]
        [Column("ID_MISSAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Alerta")]
        [Column("ID_ALERTA")]
        public int AlertaId { get; set; }
        public virtual AlertaColisao? Alerta { get; set; }

        [Required]
        [ForeignKey("Chaser")]
        [Column("ID_CHASER")]
        public int ChaserId { get; set; }
        public virtual Chaser? Chaser { get; set; }

        [Column("DATA_EXECUCAO")]
        public DateTime DataExecucao { get; set; }

        [Column("STATUS_MISSAO")]
        [StringLength(20)]
        public string Status { get; set; } = "PENDENTE";
    }
}