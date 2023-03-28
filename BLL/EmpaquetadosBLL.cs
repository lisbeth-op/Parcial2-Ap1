using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class EmpaquetadosBLL
{
    private readonly Contexto _Contexto;

    public EmpaquetadosBLL(Contexto Contexto)
    {
        _Contexto = Contexto;
    }

    public bool Existe(int EmpacadoId)
    {
        return _Contexto.Empacados.Any(o => o.EmpaqueId == EmpacadoId);
    }

    public bool Insertar(Empaquetados empacado)
    {
        var contener = false;
        try
        {
            Productos? producto;
            foreach (var detalle in empacado.detalleEmpaquetados)
            {
                producto = _Contexto.Productos.SingleOrDefault(p => p.ProductoId == detalle.ProductoId);
                if (producto != null)
                {
                    producto.Existencia -= detalle.Cantidad; 
                    _Contexto.Entry(producto).State = EntityState.Modified;
                    _Contexto.Entry(detalle).State = EntityState.Added;
                }
            }
            
            var producido = _Contexto.Productos.SingleOrDefault(p => p.ProductoId == empacado.ProductoId);

            if (producido != null)
            {
                producido.Existencia += empacado.Cantidad;
                _Contexto.Entry(producido).State = EntityState.Modified;
            }
            _Contexto.Entry(empacado).State = EntityState.Added;

            contener = _Contexto.SaveChanges() > 0;
            _Contexto.Entry(empacado).State = EntityState.Detached;
        }
        catch (Exception)
        {
            return false;
        }
        return contener;
    }

    private bool Modificar(Empaquetados empaquetado)
    {
        var anterior = Buscar(empaquetado.EmpaqueId);

        if (anterior != null)
        {
            foreach (var detalle in anterior.detalleEmpaquetados)
            {
                ActualizarExistenciaProducto(detalle.ProductoId, detalle.Cantidad);
            }
            var ProductoAnterior = _Contexto.Productos.Find(anterior.ProductoId);

            if (ProductoAnterior != null)
            {
                ProductoAnterior.Existencia -= anterior.Cantidad; 
                _Contexto.Entry(ProductoAnterior).State = EntityState.Modified;
            }
        }
        _Contexto.Database.ExecuteSqlRaw($"DELETE FROM DetalleEmpaquetados WHERE EmpaqueId = {empaquetado.EmpaqueId}");

        foreach (var detalle in empaquetado.detalleEmpaquetados)
        {
            ActualizarExistenciaProducto(detalle.ProductoId, -detalle.Cantidad);
            _Contexto.Entry(detalle).State=EntityState.Added;
        }
        var PNuevo = _Contexto.Productos.Find(empaquetado.ProductoId);
        if (PNuevo != null)
        {
            PNuevo.Existencia += empaquetado.Cantidad;
            _Contexto.Entry(PNuevo).State=EntityState.Modified;
        }
        _Contexto.Entry(empaquetado).State = EntityState.Modified;
        var contener=_Contexto.SaveChanges() > 0;
        _Contexto.Entry(empaquetado).State = EntityState.Detached;
        return contener;

    }

    private void ActualizarExistenciaProducto(int productoId, int cantidad)
    {
        var producto = _Contexto.Productos.Find(productoId);
        if (producto != null)
        {
            producto.Existencia += cantidad;
            _Contexto.Entry(producto).State = EntityState.Modified;
        }
    }


    public bool EliminarEmpaquetado(Empaquetados empaquetado)
    {
        foreach (var detalle in empaquetado.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(detalle.ProductoId);
            if (producto != null)
            {
                producto.Existencia += detalle.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;
            }
        }
        _Contexto.Database.ExecuteSqlRaw($"DELETE FROM DetalleEmpaquetados WHERE EmpaqueId = {empaquetado.EmpaqueId}");

        var ProductoAnterior = _Contexto.Productos.Find(empaquetado.ProductoId);

        if (ProductoAnterior != null)
        {
            ProductoAnterior.Existencia -= empaquetado.Cantidad;
            _Contexto.Entry(ProductoAnterior).State = EntityState.Modified;
        }
        _Contexto.Entry(empaquetado).State = EntityState.Deleted;
        bool saveSucceded = _Contexto.SaveChanges() > 0;

        _Contexto.Entry(empaquetado).State = EntityState.Detached;
        return saveSucceded;
        
    }

    public bool Guardar(Empaquetados empaquetado)
    {
        if (!Existe(empaquetado.EmpaqueId))
        {
            return this.Insertar(empaquetado);
        }
        else
        {
            return this.Modificar(empaquetado);
        }
    }
    public Empaquetados? Buscar(int EmpacadoId)
    {
        return _Contexto.Empacados
            .Where(o => o.EmpaqueId == EmpacadoId)
            .Include(o => o.detalleEmpaquetados)
            .AsNoTracking()
            .SingleOrDefault();
    }
    public List<Empaquetados> GetList(Expression<Func<Empaquetados, bool>> ctr)
    {
        return _Contexto.Empacados.AsNoTracking().Where(ctr).ToList();
    }

}