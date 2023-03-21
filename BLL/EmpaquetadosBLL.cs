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

    private bool Modificar (Empaquetados produccido){
        var empacadoAnterior = _Contexto.Empacados
             .Where(o => o.EmpacadoId== produccido.EmpacadoId)
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

         foreach (var detalle in produccido.detalleEmpaquetados)
        {
            var producto = _Contexto.Productos.Find(detalle.ProductoId);
            if(producto!=null){
                producto.Existencia-= detalle.Cantidad;
                _Contexto.Entry(producto).State = EntityState.Modified;
            }
        }

        var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(e => e.EmpacadosId == produccido.EmpacadoId);
         _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
         _Contexto.Entry(produccido).State = EntityState.Modified;
         return  _Contexto.SaveChanges() >0;
    }

    public bool Guardar( Empaquetados empaquetado){
         if(!Existe(empaquetado.EmpacadoId)){
             return this.Insertar(empaquetado);
         }
         else{
             return this.Modificar(empaquetado);
         }
    } 
    public Empaquetados? Buscar(int EmpacadoId){
         return _Contexto.Empacados
             .Where(o => o.EmpacadoId== EmpacadoId)
             .Include(o =>  o.detalleEmpaquetados)
             .AsNoTracking()
             .SingleOrDefault();
    }

     public bool Eliminar (Empaquetados empaquetados){
        var DetalleEliminar = _Contexto.Set<DetalleEmpaquetados>().Where(e => e.EmpacadosId == empaquetados.EmpacadoId);
         _Contexto.Set<DetalleEmpaquetados>().RemoveRange(DetalleEliminar);
         _Contexto.Entry(empaquetados).State = EntityState.Deleted;
         return _Contexto.SaveChanges() >0;
    }

   

    public List<Empaquetados> GetList(Expression<Func<Empaquetados,bool>>ctr){
         return _Contexto.Empacados.AsNoTracking().Where(ctr).ToList();
    }
}