using Microsoft.EntityFrameworkCore;
using WayneInc.Entitys;
using WayneInc.Entitys.Tokens;

namespace WayneInc.Data
{
    public class DbWContext : DbContext
    {
        public DbWContext(DbContextOptions<DbWContext> options) : base(options)
        {
        }

        public DbSet<Sesion> Sesion => Set<Sesion>();
        public DbSet<Admin> Admin => Set<Admin>();
        public DbSet<Carrito> Carritos => Set<Carrito>();
        public DbSet<CarritoItem> CarritoItems => Set<CarritoItem>();
        public DbSet<Tienda> Tiendas => Set<Tienda>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Articulo> Articulos => Set<Articulo>();
        public DbSet<ArticuloTienda> ArticulosTienda => Set<ArticuloTienda>();
        public DbSet<ArticuloCliente> ArticulosCliente => Set<ArticuloCliente>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    }
}

