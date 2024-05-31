using Application.Contracts.Persistence;
using Application.CQRS.Restaurant.Request.Command;
using Application.DTOs.Restaurant.Validators;
using Application.Exceptions;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.Restaurant.Handler.Command
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, Unit>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRestaurantDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateRestaurantDto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var restaurant = await _restaurantRepository.GetById(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Domain.Entity.Restaurant), request.Id);

            var address = await _addressRepository.GetById(restaurant.AddressId);
            if (address == null)
                throw new NotFoundException(nameof(Address), restaurant.AddressId);

            if (request.UpdateRestaurantDto.Address != null)
            {
                address.Street = request.UpdateRestaurantDto.Address.Street ?? address.Street;
                address.City = request.UpdateRestaurantDto.Address.City ?? address.City;
                address.PostalCode = request.UpdateRestaurantDto.Address.PostalCode ?? address.PostalCode;
                await _addressRepository.Update(address);
            }

            restaurant.Name = request.UpdateRestaurantDto.Name ?? restaurant.Name;
            restaurant.Description = request.UpdateRestaurantDto.Description ?? restaurant.Description;
            restaurant.Category = request.UpdateRestaurantDto.Category ?? restaurant.Category;
            restaurant.HasDelivery = request.UpdateRestaurantDto.HasDelivery;
            restaurant.ContactEmail = request.UpdateRestaurantDto.ContactEmail ?? restaurant.ContactEmail;
            restaurant.ContactName = request.UpdateRestaurantDto.ContactName ?? restaurant.ContactName;

            await _restaurantRepository.Update(restaurant);

            return Unit.Value;
        }
    }
}
