using Application.DTOs.Restaurant;
using MediatR;

namespace Application.CQRS.Restaurant.Request.Query
{
    public class GetRestaurantDetailsRequest : IRequest<RestaurantDto>
    {
        public int Id { get; set; }
    }
}