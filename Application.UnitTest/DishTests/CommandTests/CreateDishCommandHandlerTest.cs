using Application.CQRS.Dish.Handler.Command;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Application.DTOs.Dish;
using Application.CQRS.Dish.Request.Command;
using Application.UnitTest.Helpers;


namespace Application.UnitTest.Dish.UnitTests.CommandTests
{
    public class CreateDishCommandHandlerTest
    {
        private readonly IMapper _mockMapper;
        private readonly CreateDishCommandHandler _handler;
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly CreateDishExceptionHelper _exceptionHelper;

        public CreateDishCommandHandlerTest()
        {
            _mockRestaurantRepository = MockRestaurant.GetRestaurantRepository();
            _mockDishRepository = MockDish.GetDishRepository();
            _exceptionHelper = new CreateDishExceptionHelper();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();

            _handler = new CreateDishCommandHandler(
                _mockRestaurantRepository.Object,
                _mockDishRepository.Object,
                _mockMapper);
        }

        [Theory]
        [InlineData("Dish", "WithDesc", 1.0, 1)]
        [InlineData("Dish", "", 2.0, 1)]
        [InlineData("Dish", "WithDesc", 3.0, 2)]
        [InlineData("Dish", "", 0.1, 2)]
        public async Task Handle_ValidCommand_ShouldCreateRestaurant(
            string name,
            string description,
            decimal price,
            int restaurantId)
        {
            // Arrange
            var createDishDto = new CreateDishDto
            {
                Name = name,
                Description = description,
                Price = price,
                RestaurantId = 0
            };

            var command = new CreateDishCommand
            {
                RestaurantId = restaurantId,
                CreateDishDto = createDishDto,
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var dishes = await _mockDishRepository.Object.GetDishesBelongToRestaurant(restaurantId);
            var addDishes = dishes.FirstOrDefault(d => d.Id == result);

            var totalDishes = await _mockDishRepository.Object.GetAll();

            // Assert
            totalDishes.Count.ShouldBe(5);
            result.ShouldBeOfType<int>();

            addDishes.ShouldNotBeNull();
            addDishes.Name.ShouldBe(name);
            addDishes.Description.ShouldBe(description);
            addDishes.Price.ShouldBe(price);
            addDishes.RestaurantId.ShouldBe(restaurantId);
        }

        [Theory]
        [InlineData("", "", 19.0, 1)]
        [InlineData("ssssssssssssssssssssssssssssssssssssssssssssssssssssss", "", 1.0, 2)]
        [InlineData("Test", "", 0, 1)]
        [InlineData("Test", "", -2.0, 1)]
        [InlineData("Test", "", -0.1, 1)]
        [InlineData("Test", "", 1.0, 33)]
        public async Task Handle_InvalidCommand_ShouldThrowValidationException(
            string name,
            string description,
            decimal price,
            int restaurantId)
        {
            // Arrange
            var createDishDto = new CreateDishDto
            {
                Name = name,
                Description = description,
                Price = price,
                RestaurantId = 0
            };

            // Act 
            var command = new CreateDishCommand
            {
                RestaurantId = restaurantId,
                CreateDishDto = createDishDto
            };

            await _exceptionHelper.ValidationEx(_handler, command);

            var restaurants = await _mockDishRepository.Object.GetAll();

            //Assert
            restaurants.Count.ShouldBe(4);
        }
    } 
}
