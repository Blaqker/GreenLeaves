using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Domain
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
