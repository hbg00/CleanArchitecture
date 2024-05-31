using Application.DTOs.Restaurant;
using MediatR;

namespace Application.CQRS.Restaurant.Request.Query
{
    public class GetRestaurantsListRequest : IRequest<List<RestaurantDto>>
    {

    }
}
