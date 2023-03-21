using Microsoft.EntityFrameworkCore;

public class Contexto :DbContext
{
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Empaquetados> Empacados {get; set;}

    public Contexto(DbContextOptions<Contexto> options) :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        modelBuilder.Entity<Productos>().HasData(
                new Productos
                {
                    ProductoId = 1,
                    Descripcion = "Pistacho",
                    Costo = 15,
                    Precio = 50,
                    Existencia = 50
                },
                new Productos
                {
                    ProductoId = 2,
                    Descripcion = "Mani",
                    Costo = 15,
                    Precio = 50,
                    Existencia = 50
                },
                new Productos
                {
                    ProductoId = 3,
                    Descripcion = "Ciruela",
                    Costo = 15,
                    Precio = 50,
                    Existencia = 50
                },
                 new Productos
                 {
                     ProductoId = 4,
                     Descripcion = "Pasas",
                     Costo = 15,
                     Precio = 50,
                     Existencia = 50
                 },
                 
                 new Productos
                 {
                     ProductoId = 5,
                     Descripcion = "Arandanos",
                     Costo = 15,
                     Precio = 50,
                     Existencia = 50
                 }
        );
    }
}