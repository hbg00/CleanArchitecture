using Application.CQRS.Restaurant.Request.Command;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Restaurant.Handler.Command
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Domain.Entity.Restaurant), request.Id);

            await _restaurantRepository.Delete(restaurant);
            return Unit.Value;
        }
    }
}