using MediatR;

namespace Application.CQRS.Dish.Request.Command
{
    public class DeleteDishCommand : IRequest
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
    }
}