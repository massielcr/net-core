using MediatR;

namespace PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery : IRequest<List<EventListVM>>
    {
    }
}
