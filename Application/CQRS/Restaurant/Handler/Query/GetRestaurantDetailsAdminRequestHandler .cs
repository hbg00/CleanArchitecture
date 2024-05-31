using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Restaurant.Handler.Query
{
    public class GetRestaurantDetailsAdminRequestHandler : IRequestHandler<GetRestaurantDetailsAdminRequest, UpdateRestaurantDto>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetRestaurantDetailsAdminRequestHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<UpdateRestaurantDto> Handle(GetRestaurantDetailsAdminRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(request.Id);
            return _mapper.Map<UpdateRestaurantDto>(restaurant);
        }
    }
}