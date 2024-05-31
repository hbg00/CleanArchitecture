using Application.CQRS.Dish.Request.Query;
using Application.DTOs.Dish;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;


namespace Application.CQRS.Dish.Handler.Query
{
    public class GetDishDetailsRequestHandler : IRequestHandler<GetDishDetailsRequest, DishDto>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetDishDetailsRequestHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishDetailsRequest request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetDishDetails(request.RestaurantId, request.Id);
            return _mapper.Map<DishDto>(dish);
        }
    }
}