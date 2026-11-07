using AutoMapper;
using MediatR;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Application.Exceptions;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVM>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IEventRepository eventRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailVM> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var _event = await _eventRepository.GetByIdAsync(request.Id);
            if (_event is null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            var eventDetailDto = _mapper.Map<EventDetailVM>(_event);

            var category = await _categoryRepository.GetByIdAsync(_event.CategoryId);
            if (category is null)
            {
                throw new NotFoundException(nameof(Category), _event.CategoryId);
            }

            eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailDto;
        }
    }
}
