using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Product> ProductRepository { get; }

        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext context;

        public UnitOfWork(ShoppingContext context)
        {
            this.context = context;
        }

        private IRepository<Customer> customerRepository;
        public IRepository<Customer> CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(context);
                }

                return customerRepository;
            }
        }

        private IRepository<Order> orderRepository;
        public IRepository<Order> OrderRepository
        {
            get
            {
                if(orderRepository == null)
                {
                    orderRepository = new OrderRepository(context);
                }

                return orderRepository;
            }
        }

        private IRepository<Product> productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }

                return productRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
