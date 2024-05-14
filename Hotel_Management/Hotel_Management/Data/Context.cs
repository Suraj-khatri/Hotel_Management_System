using Hotel_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
