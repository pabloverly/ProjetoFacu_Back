using Microsoft.EntityFrameworkCore;
using ApiTools.Model;
using Pomelo.EntityFrameworkCore.MySql;

namespace ApiTools.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Stopword> Stopwords { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SessionValid> SessionValid { get; set; }
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options) : base(options)
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