using Application.Contracts.Persistence;
using Application.CQRS.Restaurant.Handler.Command;
using Application.CQRS.Restaurant.Request.Command;
using Application.Exceptions;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Moq;
using Shouldly;

namespace Application.UnitTest.Restaurant.UnitTests.Command
{
    public class DeleteRestaurantCommandHandlerTest
    {
        private readonly IMapper _mockMapper;
        private readonly DeleteRestaurantCommandHandler _handler;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IDishRepository> _mockDishRepository;

        public DeleteRestaurantCommandHandlerTest()
        {
            _mockDishRepository = new Mock<IDishRepository>();  
            _mockRestaurantRepository = MockRestaurant.GetRestaurantRepository();
            _addressRepositoryMock = new Mock<IAddressRepository>();
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();

            _handler = new DeleteRestaurantCommandHandler(
                _mockRestaurantRepository.Object,
                _mockDishRepository.Object,
                _addressRepositoryMock.Object,
                _mockMapper);
        }

        [Fact]
        public async Task DeleteRestaurant_ValidCommand_ShouldDeleteRestraurant()
        {
            // Arrange
            var command = new DeleteRestaurantCommand
            {
                Id = 2
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var restaurants = await _mockRestaurantRepository.Object.GetAll();

            // Assert
            result.ShouldBe(Unit.Value);
            restaurants.Count.ShouldBe(1);
            restaurants.ShouldNotContain(r => r.Id == 2);
        }


        [Fact]
        public async Task Handle_InValidDeleteCommand_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteRestaurantCommand { Id = 3 };

            // Act
            var exception = await Should.ThrowAsync<NotFoundException>(()
                => _handler.Handle(command, CancellationToken.None));
            var restaurants = await _mockRestaurantRepository.Object.GetAll();

            //Assert
            restaurants.Count.ShouldBe(2);
            exception.Message.ShouldContain("Entity \"Restaurant\"");
        }
    }
}