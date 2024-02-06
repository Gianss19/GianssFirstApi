using GianssWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GianssWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        
        }
       
        public DbSet<Gian> Gians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//Aqui hardcodeamos los primeros usuarios a la db, sobreescribimos el metodo de DbContext
        {
            modelBuilder.Entity<Gian>().HasData( //Estableciendo que la db tendrá la siguiente data, siguiendo el modelo 'Gian'
                new Gian
                {
                    Id = 1,
                    Name = "Test1",
                    Detalle = "Detalle Test 1",
                    ImagenUrl = string.Empty,
                    Ocupantes = 5,
                    MetrosCuadrados = 60,
                    Tarifa = 600,
                    Amenidad = string.Empty,
                    CreationDate = DateTime.Now,
                    UptdateDate = DateTime.Now


                },
                new Gian
                {
                    Id = 2,
                    Name = "Test2",
                    Detalle = "Detalle Test 2",
                    ImagenUrl = string.Empty,
                    Ocupantes = 10,
                    MetrosCuadrados = 160,
                    Tarifa = 800,
                    Amenidad = string.Empty,
                    CreationDate = DateTime.Now,
                    UptdateDate = DateTime.Now
                }



                );
        }
    }
}
