using Application.CQRS.Restaurant.Handler.Command;
using Application.CQRS.Restaurant.Handler.Query;
using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTest.Restaurant.UnitTests.Query
{
    public class GetRestaurantListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRestaurantRepository> _mockRepo;
        private readonly GetRestaurantsListRequestHandler _handler;
        public GetRestaurantListRequestHandlerTest()
        {
            _mockRepo = MockRestaurant.GetRestaurantRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetRestaurantsListRequestHandler(
                _mockRepo.Object,
                _mapper);
        }

        [Fact]
        public async Task GetAllRestaurants() 
        {
            // Arrange
            var request = new GetRestaurantsListRequest();
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<RestaurantDto>>();
            result.Count.ShouldBe(2);
        }

    }
}
