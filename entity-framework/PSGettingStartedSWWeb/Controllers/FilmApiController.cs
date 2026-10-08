using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSGettingStartedSWWeb.DataContext;
using PSGettingStartedSWWeb.Entities;

namespace PSGettingStartedSWWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController(FilmDbContext filmDbContext) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetFilms()
        {
            IEnumerable<Film> films = await filmDbContext.Films.Include(f => f.Actors).OrderBy(f => f.Year).ToListAsync();

            if (!films.Any())
            {
                return NotFound();
            }

            return Ok(films);
        }

        [HttpGet("{id:int}", Name ="GetFilm")]
        public async Task<IActionResult> GetFilm(int id)
        {
            Film? film = await filmDbContext.Films.Include(f => f.Actors).FirstOrDefaultAsync(f => f.Id == id);

            if(film is null)
            {
                return NotFound();
            }

            return Ok(film);
        }


    }
}
