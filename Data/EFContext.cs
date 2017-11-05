using Data.Domain;
using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Word>().ToTable("Word");     
        }
    }
}