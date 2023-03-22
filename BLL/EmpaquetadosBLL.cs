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

    private bool Insertar(Empaquetados empaquetado)
    {
        foreach (var dtl in empaquetado.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(dtl.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= dtl.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;
            }
        }

        _Contexto.Empacados.Add(empaquetado);
        return _Contexto.SaveChanges() > 0;
    }

    private bool Modificar(Empaquetados empaquetado)
    {
        var empacadoAnterior = _Contexto.Empacados
             .Where(e => e.EmpaqueId == empaquetado.EmpaqueId)
             .Include(e => e.detalleEmpaquetados)
             .AsNoTracking()
             .SingleOrDefault();
        if (empacadoAnterior != null)
        {
            foreach (var detalle in empacadoAnterior.detalleEmpaquetados)
            {
                var producto = _Contexto.Productos.Find(detalle.ProductoId);
                if (producto != null)
                {
                    producto.Existencia += detalle.Cantidad;
                    _Contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }

        _Contexto.Entry(empacadoAnterior).State = EntityState.Detached;



        foreach (var detalle in empaquetado.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(detalle.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= detalle.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;

            }
        }

            var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(e => e.EmpacadosId == empaquetado.EmpaqueId);
            _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
            _Contexto.Entry(empaquetado).State = EntityState.Modified;
            return _Contexto.SaveChanges() > 0;

    }

    public bool Eliminar(Empaquetados empaquetados)
    {
        var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(e => e.EmpacadosId == empaquetados.EmpaqueId);
        _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
        _Contexto.Entry(empaquetados).State = EntityState.Deleted;
        return _Contexto.SaveChanges() > 0;
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