using Application.Contracts.Persistence;
using Application.CQRS.Restaurant.Request.Command;
using Application.DTOs.Restaurant.Validators;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.Restaurant.Handler.Command
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRestaurantDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateRestaurantDto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var address = _mapper.Map<Address>(request.CreateRestaurantDto.Address);
            address = await _addressRepository.Add(address);

            var restaurant = _mapper.Map<Domain.Entity.Restaurant>(request.CreateRestaurantDto);
            restaurant.AddressId = address.Id;
            restaurant.Address = address;
            restaurant = await _restaurantRepository.Add(restaurant);

            return restaurant.Id;
        }
    }
}