using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace operadorLogisticoAPI.Repositories.Entities
{
    public partial class Envio
    {
        public string ProvinciaDestino { get; set; }
        public int? CodPostalDestino { get; set; }
        public string CalleDestino { get; set; }
        public int? NroDestino { get; set; }
        public string ProvinciaOrigen { get; set; }
        public int? CodPostalOrigen { get; set; }
        public string CalleOrigen { get; set; }
        public int? NroOrigen { get; set; }
        public int? DniContacto { get; set; }
        public int? NroProducto { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? DniRepartidor { get; set; }
        public int Id { get; set; }
        public string LocalidadDestino { get; set; }
        public string LocalidadOrigen { get; set; }
        public int? PesoProd { get; set; }
        public string TamañoProd { get; set; }
        public string Delicado { get; set; }

        //prueba
        [NotMapped]
        public Contacto contacto { get; set; }

        // public virtual Contacto DniContactoNavigation { get; set; }
        public virtual Repartidores DniRepartidorNavigation { get; set; }
        public virtual Producto NroProductoNavigation { get; set; }
    }
}
