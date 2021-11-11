namespace operadorLogisticoAPI.Models 
{

    public class Envio
   {
       public long Id { get; set; }
       public Direccion origen { get; set; }
       public Direccion destino { get; set; }
       public Direccion contacto { get; set; }
       public string estado { get; set; }
       public string fecha_recepcion { get; set; }
       public Repartidor repartidor { get; set; }
   }

}