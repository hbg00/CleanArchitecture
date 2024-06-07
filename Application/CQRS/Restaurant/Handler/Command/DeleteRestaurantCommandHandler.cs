using Application.Contracts.Persistence;
using Application.CQRS.Restaurant.Request.Command;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace Application.CQRS.Restaurant.Handler.Command
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IAddressRepository _adddressRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,IDishRepository dishRepository,IAddressRepository addressRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _adddressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Domain.Entity.Restaurant), request.Id);
            
            var address = await _adddressRepository.GetById(restaurant.AddressId);
            if(address == null)
                throw new NotFoundException(nameof(Address), restaurant.AddressId);
            foreach (var dish in await _dishRepository.GetDishesBelongToRestaurant(restaurant.Id)) 
            {
                await _dishRepository.Delete(dish);
            }
          
            await _adddressRepository.Delete(address);
            return Unit.Value;
        }
    }
}