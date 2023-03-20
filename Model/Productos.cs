using System.ComponentModel.DataAnnotations;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    public String? Descripcion { get; set; }
    public Double Costo { get; set; }
    public Double Precio { get; set; }
    public int Existencia { get; set; }
}