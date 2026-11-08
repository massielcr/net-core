using AutoMapper;
using PSCleanArchitectureGC.Application.Features.Categories.Commands.CreateCategory;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesList;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using PSCleanArchitectureGC.Application.Features.Events.Commands.CreateEvent;
using PSCleanArchitectureGC.Application.Features.Events.Commands.UpdateEvent;
using PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventDetail;
using PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventsList;
using PSCleanArchitectureGC.Application.Features.Orders.Queries.GetOrdersForMonth;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVM>().ReverseMap();
            CreateMap<Event, EventListVM>().ReverseMap();
            CreateMap<Event, CategoryEventDto>();

            CreateMap<Order, OrdersForMonthDto>();
        }
    }
}
