using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Restaurant.Handler.Query
{
    public class GetRestaurantsListRequestHandler : IRequestHandler<GetRestaurantsListRequest, List<RestaurantDto>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetRestaurantsListRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<List<RestaurantDto>> Handle(GetRestaurantsListRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetAll();
            return _mapper.Map<List<RestaurantDto>>(restaurant);
        }
    }
}