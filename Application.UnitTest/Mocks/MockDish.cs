using Application.Persistence.Contracts;
using Moq;
using Domain.Entity;

namespace Application.UnitTest.Mocks
{
    public static class MockDish
    {
        public static Mock<IDishRepository> GetDishRepository()
        {
            var dishes = new List<Domain.Entity.Dish>
            {
                new Domain.Entity.Dish
                {
                    Id = 1,
                    Name = "Nashville Hot Chicken",
                    Description = "Spicy fried chicken inspired by the flavors of Nashville.",
                    Price = 10.30M,
                    RestaurantId = 1
                },
                new Domain.Entity.Dish
                {
                    Id = 2,
                    Name = "Chicken Nuggets",
                    Description = "Crispy and juicy chicken nuggets, perfect for snacking.",
                    Price = 5.30M,
                    RestaurantId = 1
                },
                new Domain.Entity.Dish
                {
                    Id = 3,
                    Name = "Nashville Hot Chicken",
                    Description = "Spicy fried chicken inspired by the flavors of Nashville.",
                    Price = 10.30M,
                    RestaurantId = 2
                },
                new Domain.Entity.Dish
                {
                    Id = 4,
                    Name = "Chicken Nuggets",
                    Description = "Crispy and juicy chicken nuggets, perfect for snacking.",
                    Price = 5.30M,
                    RestaurantId = 2
                }
            };

            var mockRepo = new Mock<IDishRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(dishes);

            mockRepo.Setup(r => r.GetDishesBelongToRestaurant(It.IsAny<int>())).ReturnsAsync((int restaurantId)
                => dishes.Where(d => d.RestaurantId == restaurantId).ToList());

            mockRepo.Setup(r => r.GetDishDetails(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((int restaurantId, int id) 
                => dishes.FirstOrDefault(d => d.RestaurantId == restaurantId && d.Id == id));

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Entity.Dish>())).ReturnsAsync((Domain.Entity.Dish dish) =>
            {
                dishes.Add(dish);
                return dish;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<Domain.Entity.Dish>())).Returns(Task.CompletedTask);

            mockRepo.Setup(r => r.Delete(It.IsAny<Domain.Entity.Dish>())).Callback<Domain.Entity.Dish>(dish => dishes.Remove(dish));

            return mockRepo;
        }
    }
}
