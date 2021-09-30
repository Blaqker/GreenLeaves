using System;
using System.ComponentModel.DataAnnotations;

namespace GreenLeaves.Models
{
    public class ContactoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateTime Fecha { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Dato no valido.")]
        public int UbicacionId { get; set; }
    }
}
