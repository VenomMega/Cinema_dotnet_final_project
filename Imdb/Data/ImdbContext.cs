
using Imdb.Models;
using Microsoft.EntityFrameworkCore;
namespace Imdb.Data
{
    public class ImdbContext : DbContext
    {
        public ImdbContext(DbContextOptions<ImdbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Language> Language {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
             .HasMany(c => c.Actors)
             .WithMany(s => s.Movies)
             .UsingEntity(j => j.ToTable("ActorMovies"));
        }
    }
}
