using Application.CQRS.Dish.Handler.Query;
using Application.CQRS.Dish.Request.Query;
using Application.DTOs.Dish;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTest.Dish.UnitTests.RequestTests
{
    public class GetDishListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDishRepository> _mockDishRepo;
        private readonly GetDishesListRequestHandler _handler;
        public GetDishListRequestHandlerTest()
        {
            _mockDishRepo = MockDish.GetDishRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();


            _handler = new GetDishesListRequestHandler(
                _mockDishRepo.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ListOfDishes_ShouldReturnAllDishesFromRestaurant()
        {
            // Arrange
            var request = new GetDishesListRequest { RestaurantId = 2};

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<DishDto>>();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_ListOfDishes_ShouldBeEmpty()
        {
            // Arrange
            var request = new GetDishesListRequest { RestaurantId = 22 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeEmpty();
        }
    }
}
