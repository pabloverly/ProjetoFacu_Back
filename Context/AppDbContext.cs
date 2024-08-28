using Microsoft.EntityFrameworkCore;
using ApiTools.Model;

namespace ApiTools.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<ProdutoVariations> ProdutoVariations { get; set; }
        // public DbSet<Stopword> Stopwords { get; set; }       
        public DbSet<SessionValid> SessionValid { get; set; }
        public DbSet<User> User { get; set; }       
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProdutoVariations>()
       .HasOne(p => p.Produto)
       .WithMany(b => b.Variations)
       .HasForeignKey(p => p.Product_id);

            base.OnModelCreating(modelBuilder);

            // Outras configurações...

            base.OnModelCreating(modelBuilder);
        }
    }
}