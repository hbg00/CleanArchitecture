using Application.CQRS.Dish.Handler.Command;
using Application.CQRS.Dish.Request.Command;
using Application.CQRS.Restaurant.Handler.Command;
using Application.CQRS.Restaurant.Request.Command;
using Application.Exceptions;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Helpers;
using Application.UnitTest.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using System.Reflection.Metadata;

namespace Application.UnitTest.Dish.UnitTests.CommandTests
{
    public class DeleteDishCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDishRepository> _mock;
        private readonly DeleteDishCommandHandler _handler;

        public DeleteDishCommandHandlerTest()
        {
            _mock = MockDish.GetDishRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new DeleteDishCommandHandler(
               _mock.Object,
               _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteDish()
        {
            // Arrange
            var command = new DeleteDishCommand { Id = 4, RestaurantId = 2 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var dishes = await _mock.Object.GetAll();

            // Assert
            result.ShouldBe(Unit.Value);
            dishes.Count.ShouldBe(3);
            dishes.ShouldNotContain(r => r.Id == 4);
        }

        [Theory]
        [InlineData(33, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 5)]
        public async Task Handle_InValidDeleteCommand_ShouldThrowNotFoundException(
            int id,
            int restaurantId)
        {
            // Arrange
            var command = new DeleteDishCommand { Id = id, RestaurantId = restaurantId };

            // Act
            var exception = await Should.ThrowAsync<NotFoundException>(()
                => _handler.Handle(command, CancellationToken.None));

            var restaurants = await _mock.Object.GetAll();

            //Assert
            restaurants.Count.ShouldBe(4);
            exception.Message.ShouldContain("Entity \"Dish\"");
        }
    }
}
