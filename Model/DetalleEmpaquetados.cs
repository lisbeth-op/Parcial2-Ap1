using System.ComponentModel.DataAnnotations;

public class DetalleEmpacados
{
    [Key]
    public int DetalleEmpacadosId { get; set; }
    public int EmpaquetadosId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}