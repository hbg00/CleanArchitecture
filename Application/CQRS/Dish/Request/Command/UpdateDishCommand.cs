using Application.DTOs.Dish;
using MediatR;

namespace Application.CQRS.Dish.Request.Command
{ 
    public class UpdateDishCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public UpdateDishDto UpdateDishDto { get; set; }
    }
}