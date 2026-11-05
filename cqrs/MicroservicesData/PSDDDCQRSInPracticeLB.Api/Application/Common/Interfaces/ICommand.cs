using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces
{
    public interface ICommand<TResponse> : IRequest<TResponse> { }

    public interface ICommand : ICommand<Unit>, IRequest { }
}
