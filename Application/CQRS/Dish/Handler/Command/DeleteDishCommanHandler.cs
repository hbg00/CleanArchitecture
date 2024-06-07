using Application.CQRS.Dish.Request.Command;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Dish.Handler.Command
{
    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DeleteDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetDishDetails(request.RestaurantId, request.Id);

            if (dish == null)
                throw new NotFoundException(nameof(Domain.Entity.Dish), request.RestaurantId);


            await _dishRepository.Delete(dish);
            return Unit.Value;
        }
    }
}