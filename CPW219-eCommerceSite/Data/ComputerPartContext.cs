using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    public class ComputerPartContext : DbContext
    {
        public ComputerPartContext(DbContextOptions<ComputerPartContext> options) : base(options)
        {

        }

        public DbSet<Part> Parts { get; set; }
    }
}
