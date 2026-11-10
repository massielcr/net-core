using MediatR;

namespace PSCleanArchitectureGC.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQuery : IRequest<List<OrdersForMonthDto>>
    {
        public DateTime Date { get; set; }
    }
}
