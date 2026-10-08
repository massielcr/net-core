using Microsoft.EntityFrameworkCore;
using PSGettingStartedSWWeb.DataContext;
using PSGettingStartedSWWeb.Entities;

namespace PSGettingStartedSWWeb.Controllers
{
    public static class ActorMinimalApi
    {
        public static void MapActorEndpoints(this WebApplication app)
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

            app.MapGet("/api/films/{filmid:int}/actors/{id:int}", async (int filmid, int id, FilmDbContext filmDbContext) =>
            {
                Actor? actor = await filmDbContext.Films
                                                  .Where(f => f.Id == filmid)
                                                  .SelectMany(f => f.Actors)
                                                  .Where(fa => fa.Id == id)
                                                  .Select(fa => fa)
                                                  .FirstOrDefaultAsync();

                if (actor is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(actor);
            });
        }
    }
}
