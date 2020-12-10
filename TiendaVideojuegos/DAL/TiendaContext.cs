using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TiendaVideojuegos.Models;

namespace TiendaVideojuegos.DAL
{
    public class TiendaContext : DbContext
    {
        public TiendaContext() : base("TiendaContext")
        {

        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<TiendaVideojuegos.Models.CarritoItem> CarritoItems { get; set; }
    }
}