using System.ComponentModel.DataAnnotations;

public class Productos
{
    [Key]
    
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "campo obligatorio.")]
    public String? Descripcion { get; set; }
    [Required(ErrorMessage = "campo obligatorio.")]
    public Double Costo { get; set; }
    [Required(ErrorMessage = "campo obligatorio.")]
    public Double Precio { get; set; }
    [Required(ErrorMessage = "campo obligatorio.")]
    public int Existencia { get; set; }
}