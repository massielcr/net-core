using Microsoft.EntityFrameworkCore;
using PSGettingStartedSWWeb.DataContext;
using PSGettingStartedSWWeb.Entities;

namespace PSGettingStartedSWWeb.Controllers
{
    public static class ActorMinimalApi
    {
        public static void MapGetActors(this WebApplication app)
        {
            app.MapGet("/api/films/{id:int}/actors", async (int id, FilmDbContext filmDbContext) =>
            {
                Film? film = await filmDbContext.Films.Include(f => f.Actors).Where(f => f.Id == id).FirstOrDefaultAsync();

                if (film is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(film.Actors);
            });        
        }
    }
}
