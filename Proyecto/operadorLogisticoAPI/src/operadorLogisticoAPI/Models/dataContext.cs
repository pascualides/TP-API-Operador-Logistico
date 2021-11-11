using Microsoft.EntityFrameworkCore;


namespace operadorLogisticoAPI.Models
{
   public class DataContext : DbContext
   {
       public DataContext(DbContextOptions<DataContext> options)
           : base(options)
       {
       }

       public DbSet<Envio> envios { get; set; }

       public DbSet<Repartidor> repartidors { get; set; }
   }
}