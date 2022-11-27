using Microsoft.EntityFrameworkCore;
using SystemPlusAPI.Models;
using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
