using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Restaurant.Handler.Query
{
    public class GetRestaurantDetailsRequestHandler : IRequestHandler<GetRestaurantDetailsRequest, RestaurantDto>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetRestaurantDetailsRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<RestaurantDto> Handle(GetRestaurantDetailsRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(request.Id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}