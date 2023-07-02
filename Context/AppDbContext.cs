using Microsoft.EntityFrameworkCore;
using ApiTools.Model;

namespace ApiTools.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Stopword> Stopwords { get; set; }
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
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