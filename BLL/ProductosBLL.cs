using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProductosBLL
{
    private readonly Contexto _Contexto;

    public ProductosBLL(Contexto Contexto)
    {
        _Contexto= Contexto;
    }

    public List<Productos> GetList(Expression<Func<Productos,bool>>criterio){
         return _Contexto.Productos.AsNoTracking().Where(criterio).ToList();
    }
}