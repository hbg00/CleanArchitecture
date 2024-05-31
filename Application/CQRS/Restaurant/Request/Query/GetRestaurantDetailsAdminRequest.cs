using Application.DTOs.Restaurant;
using MediatR;
namespace Application.CQRS.Restaurant.Request.Query
{
    public class GetRestaurantDetailsAdminRequest : IRequest<UpdateRestaurantDto>
    {
        public int Id { get; set; }
    }
}