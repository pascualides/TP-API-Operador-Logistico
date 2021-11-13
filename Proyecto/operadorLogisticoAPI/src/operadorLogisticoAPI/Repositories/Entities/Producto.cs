using System;
using System.Collections.Generic;

namespace operadorLogisticoAPI.Repositories.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            Envio = new HashSet<Envio>();
        }

        public int? Peso { get; set; }
        public string Tamaño { get; set; }
        public string Delicado { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Envio> Envio { get; set; }
    }
}
