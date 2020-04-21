using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using System;

namespace MyShop.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            // ARRANGE 
            var mockLogger = new Mock<ILogger<OrderController>>();

            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();
            var customerRepository = new Mock<IRepository<Customer>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.CustomerRepository).Returns(() => customerRepository.Object);
            unitOfWork.Setup(uow => uow.OrderRepository).Returns(() => orderRepository.Object);
            unitOfWork.Setup(uow => uow.ProductRepository).Returns(() => productRepository.Object);

            var orderController = new OrderController(mockLogger.Object, 
                orderRepository.Object, 
                productRepository.Object, 
                customerRepository.Object, 
                unitOfWork.Object);

            var createOrderModel = new CreateOrderModel 
            {
                Customer = new CustomerModel
                {
                    Name = "Filip Ekberg",
                    ShippingAddress = "Test address",
                    City = "Gothenburg",
                    PostalCode = "43317",
                    Country = "Sweden"
                },
                LineItems = new []
                {
                    new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 10 },
                    new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 2 },
                }
            };

            // ACT

            orderController.Create(createOrderModel);

            // ASSERT

            orderRepository.Verify(r => r.Add(It.IsAny<Order>()), Times.AtLeastOnce());
        }
    }
}
