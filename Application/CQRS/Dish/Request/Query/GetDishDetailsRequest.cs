using Application.DTOs.Dish;
using MediatR;

namespace Application.CQRS.Dish.Request.Query
{
    public class GetDishDetailsRequest : IRequest<DishDto>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

    }
}