using Application.DTOs.Dish;
using MediatR;

namespace Application.CQRS.Dish.Request.Command
{
    public class CreateDishCommand : IRequest<int>
    {
        public int RestaurantId { get; set; }
        public CreateDishDto CreateDishDto { get; set; }
    }
}
