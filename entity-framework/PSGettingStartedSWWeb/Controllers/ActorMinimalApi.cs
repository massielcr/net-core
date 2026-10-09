using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PSGettingStartedSW.Web.DataContext;
using PSGettingStartedSW.Web.Entities;

namespace PSGettingStartedSW.Web.Controllers
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
            })
            .WithName("GetActor");

            app.MapPost("/api/films/{filmid:int}/actors", async (int filmid, Actor model, FilmDbContext filmDbContext) =>
            {
                ArgumentNullException.ThrowIfNull(model);

                Film? film = await filmDbContext.Films.Where(f => f.Id == filmid).FirstOrDefaultAsync();

                if (film is null)
                {
                    return Results.BadRequest();
                }

                film.Actors.Add(model);

                if (await filmDbContext.SaveChangesAsync() == 0)
                {
                    return Results.InternalServerError();
                }

                return Results.CreatedAtRoute("GetActor", new { filmid, id = model.Id }, model);                
            });

            app.MapPut("/api/actors/{id:int}", async (int id, Actor model, FilmDbContext filmDbContext) =>
            {
                ArgumentNullException.ThrowIfNull(model);

                Actor? existingActor = await filmDbContext.Actors.Where(a => a.Id == id).FirstOrDefaultAsync();

                if (existingActor is null)
                {
                    return Results.BadRequest();
                }

                existingActor.FirstName = model.FirstName;
                existingActor.LastName = model.LastName;
                existingActor.Age = model.Age;
                existingActor.Gender = model.Gender;
                existingActor.ImbLink = model.ImbLink;

                return await filmDbContext.SaveChangesAsync() > 0 
                             ? Results.Ok()
                             : Results.InternalServerError();
            });

            app.MapDelete("/api/actors/{id:int}", async (int id, FilmDbContext filmDbContext) =>
            {
                Actor? actor = await filmDbContext.Actors.Where(a => a.Id == id).FirstOrDefaultAsync();

                if (actor is null)
                {
                    return Results.NotFound();
                }

                filmDbContext.Actors.Remove(actor);

                return await filmDbContext.SaveChangesAsync() > 0 ? Results.Ok() : Results.InternalServerError();
            });
        }
    }
}
