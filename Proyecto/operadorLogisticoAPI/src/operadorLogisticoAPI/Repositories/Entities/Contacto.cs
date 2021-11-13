using System;
using System.Collections.Generic;

namespace operadorLogisticoAPI.Repositories.Entities
{
    public partial class Contacto
    {
        public Contacto()
        {
            Envio = new HashSet<Envio>();
        }

        public int Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Envio> Envio { get; set; }
    }
}
