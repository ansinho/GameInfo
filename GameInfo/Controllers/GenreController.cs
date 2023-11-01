using GameInfo.Controllers.Dtos;
using GameInfo.Models;
using GameInfo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _repository;

        public GenreController(IGenreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var genres = await _repository.ListGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListById(Guid id)
        {
            var genre = await _repository.ListGenreById(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreDto genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            var genreModel = new GenreModel
            {
                Name = genre.Name,
            };

            var response = await _repository.CreateGenre(genreModel);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GenreDto genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            var genreModel = new GenreModel
            {
                Name = genre.Name,
            };

            var response = await _repository.UpdateGenre(id, genreModel);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteGenre(id);
            return Ok("Gênero exluído com sucesso.");
        }
    }

}