using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProductosBLL
{
    private readonly Contexto _Contexto;

    public ProductosBLL(Contexto Contexto)
    {
        _Contexto = Contexto;
    }

   

    public bool Existe(int ProductoId)
    {
        return _Contexto.Productos.Any(p => p.ProductoId == ProductoId);
    }
    private bool Modificar(Productos producto)
    {
        _Contexto.Entry(producto).State = EntityState.Modified;
        return _Contexto.SaveChanges() > 0;
    }
    private bool Insertar(Productos producto)
    {
        _Contexto.Productos.Add(producto);
        return _Contexto.SaveChanges() > 0;
    }
 public List<Productos> GetList(Expression<Func<Productos, bool>> ctr)
    {
        return _Contexto.Productos.AsNoTracking().Where(ctr).ToList();
    }
    public bool Guardar(Productos producto)
    {
        if (!Existe(producto.ProductoId))
        {
            return this.Insertar(producto);
        }
        else
        {
            return this.Modificar(producto);
        }
    }
    public Productos? Buscar(String Nombre)
    {
        return _Contexto.Productos.Where(o => o.Descripcion == Nombre).AsTracking().SingleOrDefault();
    }

    public bool Eliminar(Productos producto)
    {
        _Contexto.Entry(producto).State = EntityState.Deleted;
        return _Contexto.SaveChanges() > 0;
    }

    public Productos? Buscar(int id)
    {
        return _Contexto.Productos.Where(o => o.ProductoId == id).AsTracking().SingleOrDefault();
    }

}