using Microsoft.EntityFrameworkCore;
using SportsBackend.Models;

namespace SportsBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SportsInfoModel> SportsInfo { get; set; }
    }
}
