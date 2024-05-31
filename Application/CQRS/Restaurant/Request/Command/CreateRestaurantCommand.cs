using Application.DTOs.Restaurant;
using MediatR;

namespace Application.CQRS.Restaurant.Request.Command
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        public CreateRestaurantDto CreateRestaurantDto { get; set; }
    }
}