using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empaquetados
{
    [Key]
    public int EmpaqueId { get; set; }
    [Required(ErrorMessage ="campo obligatorio.")]
    public DateTime Fecha { get; set; }= DateTime.Now;  
    [Required(ErrorMessage ="campo obligatorio.")]
    public String? Concepto { get; set; }
    [Required(ErrorMessage ="campo obligatorio.")]
    public int ProductoId { get; set; }
    [Required(ErrorMessage ="campo obligatorio.")]
    public int Cantidad { get; set; }
    [ForeignKey("EmpaqueId")]
    public List<DetalleEmpaquetados> detalleEmpaquetados { get; set; } = new List<DetalleEmpaquetados>();
}