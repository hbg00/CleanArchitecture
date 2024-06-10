using Application.CQRS.Dish.Request.Command;
using Application.DTOs.Dish.Validators;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;
using Application.Exceptions;

namespace Application.CQRS.Dish.Handler.Command
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Unit>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public UpdateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetDishDetails(request.RestaurantId, request.Id);

            if (dish == null)
                throw new NotFoundException(nameof(Domain.Entity.Dish), request.RestaurantId);

            var validator = new UpdateDishDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateDishDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
          

            _mapper.Map(request.UpdateDishDto, dish);

            await _dishRepository.Update(dish);

            return Unit.Value;
        }
    }
}