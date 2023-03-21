using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empaquetados
{
    [Key]
    public int EmpacadoId { get; set; }
    [Required(ErrorMessage ="la Fecha es requerida.")]
    public DateOnly Fecha { get; set; }
    [Required(ErrorMessage ="la Concepto es requerido.")]
    public String? Concepto { get; set; }
    [Required(ErrorMessage ="la Cantidad es Requerida.")]
    public int Cantidad { get; set; }
    [ForeignKey("EmpacadosId")]
    public List<DetalleEmpaquetados> detalleEmpaquetados { get; set; } = new List<DetalleEmpaquetados>();
}