using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores
{
    public record GetStoresQuery() : IRequest<List<StoreDto>>;
}
