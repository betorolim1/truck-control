using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TruckControl.Model.EFModel
{
    public partial class AppDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public AppDbContext(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("Connection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public virtual DbSet<Truck> Truck { get; set; }
    }
}
