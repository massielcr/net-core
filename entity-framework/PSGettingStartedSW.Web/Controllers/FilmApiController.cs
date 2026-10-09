using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSGettingStartedSW.Web.DataContext;
using PSGettingStartedSW.Web.Entities;

namespace PSGettingStartedSW.Web.Controllers
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

        [HttpPost("")]
        public async Task<IActionResult> CreateFilm(Film model)
        {
            ArgumentNullException.ThrowIfNull(model);

            foreach(Actor actor in model.Actors)
            {
                filmDbContext.Actors.Add(actor);
            }
            filmDbContext.Films.Add(model);

            if (await filmDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtRoute("GetFilm", new { Id = model.Id }, model );
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFilm(int id, Film model)
        {
            ArgumentNullException.ThrowIfNull(model);

            Film? existingFilm = await filmDbContext.Films.Include(f => f.Actors).Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingFilm is null)
            {
                return NotFound();
            }

            existingFilm.Title = model.Title;
            existingFilm.Year = model.Year;
            existingFilm.Length = model.Length;
            existingFilm.RatingScore = model.RatingScore;
            existingFilm.Mpaa = model.Mpaa;

            return await filmDbContext.SaveChangesAsync() > 0 ? Ok(existingFilm) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            Film? film = await filmDbContext.Films.Include(f => f.Actors).Where(f => f.Id == id).FirstOrDefaultAsync();

            if (film is null)
            {
                return NotFound();
            }

            filmDbContext.Films.Remove(film);
            filmDbContext.Actors.RemoveRange(film.Actors);

            return await filmDbContext.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }
    }
}
