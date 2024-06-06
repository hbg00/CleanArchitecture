using Application.Persistence.Contracts;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UnitTest.Mocks
{
    public static class MockRestaurant
    {
        public static Mock<IRestaurantRepository> GetRestaurantRepository()
        {
            var restaurants = new List<Domain.Entity.Restaurant>
            {
                new Domain.Entity.Restaurant
                {
                    Id = 1,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    ContactName = "John Doe",
                    HasDelivery = true,
                    AddressId = 1
                },
                new Domain.Entity.Restaurant
                {
                    Id = 2,
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    ContactName = "Jane Smith",
                    HasDelivery = true,
                    AddressId = 2
                }
            };

            var mockRepo = new Mock<IRestaurantRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(restaurants);

            mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((int id) => restaurants.FirstOrDefault(r => r.Id == id));

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Entity.Restaurant>())).ReturnsAsync((Domain.Entity.Restaurant restaurant) =>
            {
                restaurants.Add(restaurant);
                return restaurant;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<Domain.Entity.Restaurant>())).Returns(Task.CompletedTask);


            mockRepo.Setup(r => r.Delete(It.IsAny<Domain.Entity.Restaurant>())).Callback<Domain.Entity.Restaurant>(restaurant =>
            {
                restaurants.Remove(restaurant);
            });

            return mockRepo;
        }
    }
}
