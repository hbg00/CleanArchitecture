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
using Moq;
using Shouldly;
using System.ComponentModel.DataAnnotations;

namespace Application.UnitTest.Restaurant.UnitTests.Command
{
    public class CreateRestaurantCommandHandlerTest
    {
        private readonly IMapper _mockMapper;
        private readonly CreateRestaurantCommandHandler _handler;
        private readonly Mock<IAddressRepository> _mockAddressRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;

        public CreateRestaurantCommandHandlerTest()
        {
            _mockRestaurantRepository = MockRestaurant.GetRestaurantRepository();
            _mockAddressRepository = MockAddress.GetAddressRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();

            _handler = new CreateRestaurantCommandHandler(
                _mockRestaurantRepository.Object,
                _mockAddressRepository.Object,
                _mockMapper);

        }

        [Theory]
        [InlineData("KFC", "WithDesc", "carljenkins@gmail.com", "CAT", "CONTACT", false, "CITY", "STREET", "36-632")]
        [InlineData("KFC", "", "carljenkins@gmail.com", "CAT", "CONTACT", false, "CITY", "STREET", "32-632")]
        public async Task Handle_ValidCommand_ShouldCreateRestaurant(
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
            var createRestaurantDto = new CreateRestaurantDto
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

            var command = new CreateRestaurantCommand
            {
                CreateRestaurantDto = createRestaurantDto
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<int>();

            var restaurant = await _mockRestaurantRepository.Object.GetAll();

            var addedRestaurant = restaurant.FirstOrDefault(r => r.Id == result);
            addedRestaurant.ShouldNotBeNull();
            addedRestaurant.Name.ShouldBe(name);
            addedRestaurant.Description.ShouldBe(description);
            addedRestaurant.ContactEmail.ShouldBe(contactEmail);
            addedRestaurant.Category.ShouldBe(category);
            addedRestaurant.ContactName.ShouldBe(contactName);
            addedRestaurant.HasDelivery.ShouldBe(hasDelivery);
            addedRestaurant.Address.Street.ShouldBe(street);
            addedRestaurant.Address.City.ShouldBe(city);
            addedRestaurant.Address.PostalCode.ShouldBe(postalCode);

            var restaurantCount = await _mockRestaurantRepository.Object.GetAll();
            restaurantCount.Count.ShouldBe(3);
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
            var createRestaurantDto = new CreateRestaurantDto
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
            var validator = new CreateRestaurantDtoValidator();
            var validationResult = await validator.ValidateAsync(createRestaurantDto);

            if (!validationResult.IsValid)
            {
                var command = new CreateRestaurantCommand
                {
                    CreateRestaurantDto = createRestaurantDto
                };
                
                var exception = await Should.ThrowAsync<ValidationException>(()
                    => _handler.Handle(command, CancellationToken.None));

                var restaurants = await _mockRestaurantRepository.Object.GetAll();
                
                //Assert
                restaurants.Count.ShouldBe(2);
                exception.Message.ShouldContain("Validation failed");
            }   
            else 
            {
                false.ShouldBeTrue("Excepted failure but validation passed");
            }
        
        }
    }
}