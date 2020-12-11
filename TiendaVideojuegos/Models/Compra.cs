using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TiendaVideojuegos.Models
{
    public class Compra
    {
        [Key]
        [Column(Order = 0)]
        public int CompraID { get; set; }
        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        [Key]
        [Column(Order = 1)]
        public int ProductoID { get; set; }
        [Display(Name = "Producto")]
        public virtual Producto Producto { get; set; }
        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        [Key]
        [Column(Order = 2)]
        public int ClienteID { get; set; }
        [Display(Name = "Cliente")]
        public virtual Cliente Cliente { get; set; }
        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FechaCompra { get; set; }
        public int Cantidad { get; set; }
    }
}