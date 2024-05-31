using Application.CQRS.Dish.Request.Command;
using Application.DTOs.Dish.Validators;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.Dish.Handler.Command
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(request.RestaurantId);

            if (restaurant == null)
                throw new NotFoundException(nameof(Domain.Entity.Restaurant), request.RestaurantId);

            var validator = new CreateDishDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDishDto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var dish = _mapper.Map<Domain.Entity.Dish>(request.CreateDishDto);
            dish.RestaurantId = request.RestaurantId;

            dish = await _dishRepository.Add(dish);
            return dish.Id;
        }
    }
}