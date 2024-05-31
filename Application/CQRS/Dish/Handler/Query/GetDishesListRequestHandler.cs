using Application.DTOs.Dish;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;
using Application.CQRS.Dish.Request.Query;

namespace Application.CQRS.Dish.Handler.Query
{
    public class GetDishesListRequestHandler : IRequestHandler<GetDishesListRequest, List<DishDto>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetDishesListRequestHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<List<DishDto>> Handle(GetDishesListRequest request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetDishesBelongToRestaurant(request.RestaurantId);
            return _mapper.Map<List<DishDto>>(dishes);
        }
    }
}