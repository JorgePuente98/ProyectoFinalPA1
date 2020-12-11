using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TiendaVideojuegos.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }
        [Required(ErrorMessage = "Debe llevar nombre de cliente")]
        [DataType(DataType.Text)]
        [Display(Name = "Cliente")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe llevar el apellido")]
        [DataType(DataType.Text)]
        [Display(Name = "Apellidos")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        [Column(TypeName = "varchar")]
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Debe llevar el correo")]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        [MaxLength(40, ErrorMessage = "Máximo 40 caracteres")]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

    }
}