using System.ComponentModel.DataAnnotations;
public class DetalleEmpaquetados
{
    [Key]
    public int DetalleEmpacadosId { get; set; }
    public int EmpacadosId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}