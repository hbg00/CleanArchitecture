using Application.Contracts.Persistence;
using Domain.Entity;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockAddress
    {
        public static Mock<IAddressRepository> GetAddressRepository()
        {
            var addresses = new List<Address>
            {
                  new Address
                {
                    Id = 1,
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
                new Address
                {
                    Id = 2,
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                }
            };

            var mockRepo = new Mock<IAddressRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(addresses);

            mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((int id) =>
                addresses.FirstOrDefault(a => a.Id == id));

            mockRepo.Setup(r => r.Add(It.IsAny<Address>())).ReturnsAsync((Address address) =>
            {
                addresses.Add(address);
                return address;
            });
            return mockRepo;
        }
    }
}
