using Microsoft.EntityFrameworkCore;

namespace BE_CRUDNET.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) 
        {

        }   
        
        public DbSet<Mascota> Mascotas { get; set; }    

    }
}
