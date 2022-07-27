using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get();

        Task<Movie> Get(int id);

        Task<Movie> Create(Movie movie);

        Task Update(Movie movie);

        Task Delete(int id);
    }
}
