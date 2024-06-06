using Application.CQRS.Dish.Handler.Query;
using Application.CQRS.Dish.Request.Query;
using Application.DTOs.Dish;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Dish.UnitTests.RequestTests
{
    public class GetDishDetailsRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDishRepository> _mockRepo;
        private readonly GetDishDetailsRequestHandler _handler;

        public GetDishDetailsRequestHandlerTest()
        {
            _mockRepo = MockDish.GetDishRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetDishDetailsRequestHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Handle_SpecificDishValid_ShouldReturnDish()
        {
            // Arrange
            var request = new GetDishDetailsRequest { Id = 2, RestaurantId = 1 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<DishDto>();
            result.Id.ShouldBe(request.Id);
        }

        [Fact]
        public async Task Handle_SpecificDishInValid_ShouldBeNull()
        {
            // Arrange
            var request = new GetDishDetailsRequest { Id = 22, RestaurantId = 2 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_SpecificDishInValidRestaurantId_ShouldBeNull()
        {
            // Arrange
            var request = new GetDishDetailsRequest { Id = 3, RestaurantId = 3 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
