using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TiendaVideojuegos.Models
{
    public class CarritoItem : IEquatable<CarritoItem>
    {   
        [Key]
        public int CarritoID { get; set; }
        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        [Display(Name = "Producto")]
        public virtual Producto Producto { get; set; }
        [Required(ErrorMessage = "Debe indicar la cantidad de productos")]
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }


      public void CalcularSubtotal()
        {
          Subtotal= Cantidad * Producto.Precio;
        }
        

        bool IEquatable<CarritoItem>.Equals(CarritoItem otro)
        {
            if (this.ProductoID == otro.ProductoID) return true;
            return false;
        }
    }
}