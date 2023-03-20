﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SegundoParcial.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20230320234912_Iniciando")]
    partial class Iniciando
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Productos", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Costo")
                        .HasColumnType("REAL");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<int>("Existencia")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            ProductoId = 1,
                            Costo = 15.0,
                            Descripcion = "Pistacho",
                            Existencia = 50,
                            Precio = 50.0
                        },
                        new
                        {
                            ProductoId = 2,
                            Costo = 15.0,
                            Descripcion = "Mani",
                            Existencia = 50,
                            Precio = 50.0
                        },
                        new
                        {
                            ProductoId = 3,
                            Costo = 15.0,
                            Descripcion = "Ciruela",
                            Existencia = 50,
                            Precio = 50.0
                        },
                        new
                        {
                            ProductoId = 4,
                            Costo = 15.0,
                            Descripcion = "Pasas",
                            Existencia = 50,
                            Precio = 50.0
                        },
                        new
                        {
                            ProductoId = 5,
                            Costo = 15.0,
                            Descripcion = "Arandanos",
                            Existencia = 50,
                            Precio = 50.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
