using GameInfo.Models;

namespace GameInfo.Repository.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreModel>> ListGenres();
        Task<GenreModel> ListGenreById(Guid id);
        Task<GenreModel> CreateGenre(GenreModel genre);
        Task<GenreModel> UpdateGenre(Guid id, GenreModel genre);
        Task DeleteGenre(Guid id);
    }
}
