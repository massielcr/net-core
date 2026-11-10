using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces
{
    public interface IQuery<TResponse> : IRequest<TResponse> { }
}
