using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Domain
{
    public partial class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        public int? UbicacionId { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }
    }
}
