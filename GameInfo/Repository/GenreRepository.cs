using Dapper;
using GameInfo.Models;
using GameInfo.Repository.Interfaces;
using Npgsql;

namespace GameInfo.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public GenreRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("GameInfoDbConnection");
        }

        public async Task<IEnumerable<GenreModel>> ListGenres()
        {
            string sql = @"SELECT id_genre as id, name FROM tb_genre;";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryAsync<GenreModel>(sql);

        }

        public async Task<GenreModel> ListGenreById(Guid id)
        {
            string sql = @"SELECT id_genre as id, name FROM tb_genre WHERE id_genre = @Id";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<GenreModel>(sql, new { Id = id });

        }

        public async Task<GenreModel> CreateGenre(GenreModel genre)
        {
            string sql = @"INSERT INTO tb_genre (id_genre, name) VALUES (@Id, @Name)";

            using var con = new NpgsqlConnection(connectionString);

            genre.Id = Guid.NewGuid();

            await con.ExecuteAsync(sql, genre);

            return genre;
        }

        public async Task<GenreModel> UpdateGenre(Guid id, GenreModel genre)
        {
            string sql = @"UPDATE tb_genre SET name = :Name WHERE id_genre = :Id";

            using var con = new NpgsqlConnection(connectionString);
            await con.ExecuteAsync(sql, new { genre.Name, Id = id });

            return await ListGenreById(id);
        }

        public async Task DeleteGenre(Guid id)
        {
            string sql = @"DELETE FROM tb_genre WHERE id_genre = :Id";

            using var con = new NpgsqlConnection(connectionString);
            await con.ExecuteAsync(sql, new { id });
        }
    }
}



