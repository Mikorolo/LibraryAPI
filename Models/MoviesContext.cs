using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
