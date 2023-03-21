using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class EmpaquetadosBLL
{
    private readonly Contexto _Contexto;

    public EmpaquetadosBLL(Contexto Contexto)
    {
        _Contexto= Contexto;
    }

    public bool Existe(int EmpacadoId ){
         return _Contexto.Empacados.Any(o=> o.EmpacadoId==EmpacadoId);
    }

    private bool Insertar (Empaquetados empacado){
        foreach (var detalle in empacado.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(detalle.ProductoId);
            if(producto!=null){
                producto.Existencia-= detalle.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;
            }
        }

         _Contexto.Empacados.Add(empacado);
         return _Contexto.SaveChanges() >0;
    }

    private bool Modificar (Empaquetados empacado){
        var empacadoAnterior = _Contexto.Empacados
             .Where(o => o.EmpacadoId== empacado.EmpacadoId)
             .Include(o =>  o.detalleEmpaquetados)
             .AsNoTracking()
             .SingleOrDefault();
        if(empacadoAnterior!=null){
            foreach (var detalle in empacadoAnterior.detalleEmpaquetados)
            {
                var producto = _Contexto.Productos.Find(detalle.ProductoId);
                if(producto!=null){
                    producto.Existencia += detalle.Cantidad;
                    _Contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }

         foreach (var detalle in empacado.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(detalle.ProductoId);
            if(producto!=null){
                producto.Existencia-= detalle.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;
            }
        }

        var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(o => o.EmpacadosId == empacado.EmpacadoId);
         _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
         _Contexto.Entry(empacado).State = EntityState.Modified;
         return  _Contexto.SaveChanges() >0;
    }

    public bool Guardar( Empaquetados empacado){
         if(!Existe(empacado.EmpacadoId)){
             return this.Insertar(empacado);
         }
         else{
             return this.Modificar(empacado);
         }
    }

    public Empaquetados? Buscar(int EmpacadoId){
         return _Contexto.Empacados
             .Where(o => o.EmpacadoId== EmpacadoId)
             .Include(o =>  o.detalleEmpaquetados)
             .AsNoTracking()
             .SingleOrDefault();
    }
   public bool Eliminar (EmpaquetadosBLL empacado){
        var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(e => e.EmpacadosId == e.EmpacadosId);
         _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
         _Contexto.Entry(empacado).State = EntityState.Deleted;
         return _Contexto.SaveChanges() >0;
    }
    public List<Empaquetados> GetList(Expression<Func<Empaquetados,bool>>ctr){
         return _Contexto.Empacados.AsNoTracking().Where(ctr).ToList();
    }
}