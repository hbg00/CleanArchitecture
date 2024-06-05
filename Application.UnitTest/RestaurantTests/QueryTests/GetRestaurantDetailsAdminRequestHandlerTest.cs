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
    public class GetRestaurantDetailsAdminRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRestaurantRepository> _mockRepo;
        private readonly GetRestaurantDetailsAdminRequestHandler _handler;
        private readonly string[] _unwantedProperties;
        public GetRestaurantDetailsAdminRequestHandlerTest()
        {
            _mockRepo = MockRestaurant.GetRestaurantRepository();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetRestaurantDetailsAdminRequestHandler(
                _mockRepo.Object,
                _mapper);
            _unwantedProperties = new[] { "ContactEmail", "ContactName"};
        }

        [Fact]
        public async Task Handle_SpecifcRestaurantValid_ShouldReturnRestaurnt()
        {
            // Arrange
            var request = new GetRestaurantDetailsAdminRequest { Id = 2 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UpdateRestaurantDto>();
            result.Id.ShouldBe(request.Id);

            foreach (var propertyName in _unwantedProperties)
            {
                result.GetType().GetProperty(propertyName).ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Handle_SpecifcRestaurantInValid_ShouldNotFound()
        {
            // Arrange
            var request = new GetRestaurantDetailsAdminRequest { Id = 3 };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
