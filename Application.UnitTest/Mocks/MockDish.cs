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

                },
                new Domain.Entity.Dish
                {

                }
            };

            var mockRepo = new Mock<IDishRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(dishes);

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Entity.Dish>())).ReturnsAsync((Domain.Entity.Dish dish) =>
            {
                dishes.Add(dish);
                return dish;
            });
            return mockRepo;
        }
    }
}
