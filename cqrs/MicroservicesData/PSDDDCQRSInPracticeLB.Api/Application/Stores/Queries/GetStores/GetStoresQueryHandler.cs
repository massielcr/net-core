using MediatR;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores
{
    public sealed class GetStoresQueryHandler : IRequestHandler<GetStoresQuery, List<StoreDto>>
    {
        private readonly IStoreRepository _repository;

        public GetStoresQueryHandler(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StoreDto>> Handle(GetStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await _repository.GetAll();
            return stores.Select(StoreMapper.ToDto).ToList();
        }
    }
}
