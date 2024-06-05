using Application.CQRS.Restaurant.Handler.Query;
using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using Application.Exceptions;
using Application.Persistence.Contracts;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
namespace Application.UnitTest.Restaurant.UnitTests.Query
{
    public class GetRestaurantDetailsRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRestaurantRepository> _mockRepo;
        private readonly GetRestaurantDetailsRequestHandler _handler;
        private readonly string[] _unwantedProperties; 
        public GetRestaurantDetailsRequestHandlerTest()
        {
            _mockRepo = MockRestaurant.GetRestaurantRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetRestaurantDetailsRequestHandler(
                _mockRepo.Object,
                _mapper);
              _unwantedProperties = new[] { "ContactEmail", "ContactName"};
        }

        [Fact]
        public async Task Handle_SpecifcRestaurantValid_ShouldReturnRestaurnt()
        {
            // Arrange
            var request = new GetRestaurantDetailsRequest { Id = 2 };
          
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
         
            // Assert
            result.ShouldBeOfType<RestaurantDto>();
            result.Id.ShouldBe(request.Id);
            
            foreach (var propertyName in _unwantedProperties)
            {
                result.GetType().GetProperty(propertyName).ShouldBeNull();
            }
        }

        [Fact]
        public async Task Handle_SpecifcRestaurantInValid_ShouldNotFound()
        {
            // Arrange
            var request = new GetRestaurantDetailsRequest { Id = 3 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            
            // Assert
            result.ShouldBeNull();
        }
    }
}
