using PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Mapper
{
    public static class StoreMapper
    {
        public static StoreDto ToDto(Store store)
        {
            return new StoreDto()
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location
            };
        }
    }
}
