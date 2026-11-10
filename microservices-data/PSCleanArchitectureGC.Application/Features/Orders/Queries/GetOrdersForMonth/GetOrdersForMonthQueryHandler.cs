using AutoMapper;
using MediatR;
using PSCleanArchitectureGC.Application.Contracts.Persistence;

namespace PSCleanArchitectureGC.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQueryHandler : IRequestHandler<GetOrdersForMonthQuery, List<OrdersForMonthDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersForMonthQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersForMonthDto>> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersForMonth(request.Date);
            return _mapper.Map<List<OrdersForMonthDto>>(orders);
        }
    }
}
