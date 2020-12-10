using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TiendaVideojuegos.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "Debe llevar nombre")]
        [DataType(DataType.Text)]
        [Display(Name = "Categoria")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [Column(TypeName = "varchar")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe contener descripción")]
        [DataType(DataType.Text)]
        [Display(Name = "Descripcion")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Column(TypeName = "varchar")]
        public string Descripcion { get; set; }
    }
}