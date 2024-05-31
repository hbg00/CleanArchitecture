using Application.DTOs.Dish;
using MediatR;

namespace Application.CQRS.Dish.Request.Query
{
    public class GetDishesListRequest : IRequest<List<DishDto>>
    {
        public int RestaurantId { get; set; }
    }
}