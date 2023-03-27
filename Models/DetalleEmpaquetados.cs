using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class DetalleEmpaquetados
{
    [Key]
    public int DetalleEmpacadosId { get; set; }
    public int EmpaqueId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}