using Microsoft.EntityFrameworkCore;
using ApiTools.Model;
using Pomelo.EntityFrameworkCore.MySql;

namespace ApiTools.Context
{
    public class AutentiteDbContext : DbContext
    {
        public DbSet<Contact> Contact { get; set; }
        private readonly IConfiguration _configuration;
        public AutentiteDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options)
        : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MySqlConnection");
            optionsBuilder.UseMySQL(connectionString);

        }
    }
}