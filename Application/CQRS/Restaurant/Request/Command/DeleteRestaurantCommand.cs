using MediatR;

namespace Application.CQRS.Restaurant.Request.Command
{
    public class DeleteRestaurantCommand : IRequest
    {
        public int Id { get; set; }
    }
}
