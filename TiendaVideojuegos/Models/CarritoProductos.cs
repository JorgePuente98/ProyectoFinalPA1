using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVideojuegos.Models
{
    public abstract class CarritoProductos
    {
        //Lista que servirá como nuestro carrito
        public static List<CarritoItem> listaProductos = new List<CarritoItem>();
    }
}