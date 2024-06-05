using Application.Contracts.Persistence;
using Application.CQRS.Restaurant.Handler.Command;
using Application.CQRS.Restaurant.Request.Command;
using Application.DTOs.Address;
using Application.DTOs.Restaurant;
using Application.DTOs.Restaurant.Validators;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Moq;
using Shouldly;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTest.Restaurant.UnitTests.Command
{
    public class UpdateRestaurantCommandHandlerTest
    {
        private readonly IMapper _mockMapper;
        private readonly UpdateRestaurantCommandHandler _handler;
        private readonly Mock<IAddressRepository> _mockAddressRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;

        public UpdateRestaurantCommandHandlerTest()
        {
            _mockRestaurantRepository = MockRestaurant.GetRestaurantRepository();
            _mockAddressRepository = MockAddress.GetAddressRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();

            _handler = new UpdateRestaurantCommandHandler(
                _mockRestaurantRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper);

        }

        [Theory]
        [InlineData("KFC", "WithDesc", "carljenkins@gmail.com", "CAT", "CONTACT", false, "CI2TY", "STR2EET", "36-632")]
        [InlineData("K1FC", "", "carlj1enkins@gmail.com", "CA1T", "CONT1ACT", false, "CIT3Y", "ST3REET", "32-632")]
        public async Task Handle_ValidCommand_ShouldUpdateRestaurant(
            string name,
            string description,
            string contactEmail,
            string category,
            string contactName,
            bool hasDelivery,
            string street,
            string city,
            string postalCode)
        {
            // Arrange
            var updateRestaurantDto = new UpdateRestaurantDto
            {
                Name = name,
                Category = category,
                ContactEmail = contactEmail,
                ContactName = contactName,
                Description = description,
                HasDelivery = hasDelivery,
                Address = new AddressDto
                {
                    Street = street,
                    City = city,
                    PostalCode = postalCode
                }
            };

            var command = new UpdateRestaurantCommand { Id = 1, UpdateRestaurantDto = updateRestaurantDto };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBe(Unit.Value);

            var restaurant = await _mockRestaurantRepository.Object.GetById(1);
            var address = await _mockAddressRepository.Object.GetById(1);
            restaurant.ShouldNotBeNull();
            restaurant.Name.ShouldBe(name);
            restaurant.Description.ShouldBe(description);
            restaurant.ContactEmail.ShouldBe(contactEmail);
            restaurant.Category.ShouldBe(category);
            restaurant.ContactName.ShouldBe(contactName);
            restaurant.HasDelivery.ShouldBe(hasDelivery);
            address.Street.ShouldBe(street);
            address.City.ShouldBe(city);
            address.PostalCode.ShouldBe(postalCode);

            var restaurants = await _mockRestaurantRepository.Object.GetAll();
            restaurants.Count.ShouldBe(2);
        }


        [Theory]
        [InlineData("", "", "", "", "", true, "", "", "")]
        [InlineData("KFC", "", "carljenkins@gmailcom", "CAT", "CONTACT", false, "CITY", "STREET", "36-632")]
        [InlineData("KFC", "RANDOMDESC", "carljenkins@gmail.com", "CAT", "CONTACT", false, "CITY", "STREET", "35632")]
        [InlineData("asddddddddddddddddddddddddddddddddddddddddddddddd", "", "carljenkins@gmail.com", "CAT", "CONTACT", true, "CITY", "STREET", "34-632")]
        [InlineData("KFC", "RANDOMDESC", "carljenkins@gmail.com", "asdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd", "CONTACT", false, "CITY", "STREET", "33-632")]
        [InlineData("KFC", "RANDOMDESC", "carljenkins@gmail.com", "CAT", "asddddddddddddddddddddddddddddddddddddd", true, "CITY", "STREET", "31-632")]
        public async Task Handle_InvalidCommand_ShouldThrowValidationException(
         string name,
         string description,
         string contactEmail,
         string category,
         string contactName,
         bool hasDelivery,
         string street,
         string city,
         string postalCode)
        {
            
            // Arrange
            var updateRestaurantDto = new UpdateRestaurantDto
            {
                Name = name,
                Category = category,
                ContactEmail = contactEmail,
                ContactName = contactName,
                Description = description,
                HasDelivery = hasDelivery,
                Address = new AddressDto
                {
                    Street = street,
                    City = city,
                    PostalCode = postalCode
                }
            };

            // Act
            var validator = new UpdateRestaurantDtoValidator();
            var validationResult = await validator.ValidateAsync(updateRestaurantDto);

            if (!validationResult.IsValid)
            {
                var command = new UpdateRestaurantCommand
                {
                    UpdateRestaurantDto = updateRestaurantDto
                };

                var exception = await Should.ThrowAsync<ValidationException>(()
                     => _handler.Handle(command, CancellationToken.None));
                var restaurant = await _mockRestaurantRepository.Object.GetAll();


                // Assert
                restaurant.Count.ShouldBe(2);
                exception.Message.ShouldContain("Validation failed");
            }
            else
            {
                false.ShouldBeTrue("Excepted failure but validation passed");
            }
        }
    }
}
