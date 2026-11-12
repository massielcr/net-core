using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores
{
    public record GetStoresQuery() : IQuery<List<StoreDto>>;
}
