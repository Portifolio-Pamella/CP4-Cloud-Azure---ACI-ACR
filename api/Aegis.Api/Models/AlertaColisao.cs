using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aegis.Api.Models { 

[Table("TB_ALERTA_COLISAO")]
public class AlertaColisao
{
    [Key]
    [Column("ID_ALERTA")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    [ForeignKey("Satelite")]
    [Column("ID_SATELITE")]
    public int SateliteId { get; set; }
    public virtual Satelite? Satelite { get; set; }

    [Required] 
    [ForeignKey("Detrito")]
    [Column("ID_DETRITO")]
    public int DetritoId { get; set; }
    public virtual DetritoEspacial? Detrito { get; set; }

    [Required] 
    [StringLength(20)] 
    [Column("STATUS_GRAVIDADE")]
    public string StatusGravidade { get; set; } = "BAIXA"; 
}
}