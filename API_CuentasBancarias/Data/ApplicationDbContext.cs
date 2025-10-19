using API_CuentasBancarias.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CuentasBancarias.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Representa la tabla CuentasBancarias en la BD
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
    }
}