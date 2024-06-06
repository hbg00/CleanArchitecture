using Application.CQRS.Dish.Handler.Command;
using Application.CQRS.Dish.Request.Command;
using Application.DTOs.Dish;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Helpers;
using Application.UnitTest.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;

namespace Application.UnitTest.Dish.UnitTests.CommandTests
{
    public class UpdateDishCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDishRepository> _mock;
        private readonly UpdateDishCommandHandler _handler;
        private readonly UpdateDishExceptionHelper _exceptionHelper;
        public UpdateDishCommandHandlerTest()
        {
            _mock = MockDish.GetDishRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateDishCommandHandler(
               _mock.Object,
               _mapper);

            _exceptionHelper = new UpdateDishExceptionHelper();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateRestaurant()
        {
            // Arrange
            var updateDishDto = new UpdateDishDto
            {
                Name = "Dish1",
                Description = "Wit1hDesc",
                Price = 2.0M,
            };

            var command = new UpdateDishCommand
            {
                Id = 1,
                RestaurantId = 1,
                UpdateDishDto = updateDishDto,
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBe(Unit.Value);

            var dishes = await _mock.Object.GetDishesBelongToRestaurant(1);

            var updatedDish = dishes.FirstOrDefault(d => d.Id == 1);
            updatedDish.ShouldNotBeNull();
            updatedDish.Name.ShouldBe("Dish1");
            updatedDish.Description.ShouldBe("Wit1hDesc");
            updatedDish.Price.ShouldBe(2.0M);
            updatedDish.RestaurantId.ShouldBe(1);

            var totalDishes = await _mock.Object.GetAll();
            totalDishes.Count.ShouldBe(4);
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
            var updateDishDto = new UpdateDishDto
            {
                Name = "Dish1",
                Description = "Wit1hDesc",
                Price = 2.0M,
            };

            var command = new UpdateDishCommand
            {
                Id = 1,
                RestaurantId = 1,
                UpdateDishDto = updateDishDto,
            };

            await _exceptionHelper.ValidationEx(_handler, command);

            var restaurants = await _mock.Object.GetAll();

            //Assert
            restaurants.Count.ShouldBe(4);
        }
    }
}
