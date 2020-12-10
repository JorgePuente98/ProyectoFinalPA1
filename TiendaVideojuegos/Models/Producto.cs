using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TiendaVideojuegos.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        [Required(ErrorMessage = "Debe llevar nombre de producto")]
        [DataType(DataType.Text)]
        [Display(Name = "Producto")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe llevar precio")]
        [Display(Name = "Precio")]
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Debe contener descripción")]
        [DataType(DataType.Text)]
        [Display(Name = "Descripcion")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Column(TypeName = "varchar")]
        public string Descripcion { get; set; }
        [Display(Name = "Carátula")]
        [Column(TypeName = "varchar")]
        public string RutaImagen { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
        [Display(Name = "Categoria")]
        public virtual Categoria Categoria { get; set; }

        public object Operation()
        {
            return new Producto();
        }

    }
}