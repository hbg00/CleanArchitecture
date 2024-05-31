using Application.DTOs.Restaurant;
using MediatR;

namespace Application.CQRS.Restaurant.Request.Command
{
    public class UpdateRestaurantCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateRestaurantDto UpdateRestaurantDto { get; set; }
    }
}