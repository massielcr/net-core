using MediatR;

namespace PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<EventDetailVM>
    {
        public Guid Id { get; set; }
    }
}
