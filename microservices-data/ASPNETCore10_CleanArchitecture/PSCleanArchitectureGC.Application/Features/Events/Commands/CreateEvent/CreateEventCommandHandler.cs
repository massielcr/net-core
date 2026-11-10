using AutoMapper;
using MediatR;
using PSCleanArchitectureGC.Application.Contracts.Infrastructure;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Application.Models.Mail;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            var _event = _mapper.Map<Event>(request);

            _event = await _eventRepository.AddAsync(_event);

            var email = new Email()
            {
                To = "testemail@gmail.com",
                Body = $"A new event was created: {request}",
                Subject = "A new event was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception) { }

            return _event.EventId;
        }
    }
}
