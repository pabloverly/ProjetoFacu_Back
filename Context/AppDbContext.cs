using Microsoft.EntityFrameworkCore;
using ApiTools.Model;

namespace ApiTools.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Stopword> Stopwords { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SessionValid> SessionValid { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TwoFactor> TwoFactor { get; set; }
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