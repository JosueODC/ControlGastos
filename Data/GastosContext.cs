
using ControlGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Data
{
    public class GastosContext : DbContext
    {
        public GastosContext(DbContextOptions<GastosContext> options)
            : base(options)
        {
        }

        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
