using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empaquetados
{
    [Key]
    public int EmpacadoId { get; set; }
    [Required(ErrorMessage ="Campo obligatorio.")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage ="Campo obligatorio.")]
    public String? Concepto { get; set; }
    [Required(ErrorMessage ="Campo obligatorio.")]
    public int Cantidad { get; set; }
    [ForeignKey("EmpacadosId")]
    public List<DetalleEmpaquetados> detalleEmpaquetados { get; set; } = new List<DetalleEmpaquetados>();
}