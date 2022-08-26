using Microsoft.EntityFrameworkCore;
using JWTWebAPI.Models;

namespace JWTWebAPI.Data
{
    public class ConexionDBContext : DbContext
    {
        protected readonly IConfiguration Configuracion;
        public ConexionDBContext(IConfiguration configuracion)
        {
            Configuracion = configuracion;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuracion.GetConnectionString("MiConexion"));
        }
        public DbSet<Producto> Producto
        {
            get;
            set;
        }
        public DbSet<Login> Login
        {
            get;
            set;
        }
    }
}
