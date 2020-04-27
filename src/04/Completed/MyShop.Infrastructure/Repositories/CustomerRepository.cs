using MyShop.Domain.Lazy;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Ghosts;
using MyShop.Infrastructure.Lazy.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ShoppingContext context) : base(context)
        {
        }

        public override Customer Get(Guid id)
        {
            var customerId = context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => c.CustomerId)
                .Single();

            return new GhostCustomer(() => base.Get(id))
            {
                CustomerId = customerId
            };
        }

        public override IEnumerable<Customer> All()
        {
            // Lazy Loading: Value Holder
            //ProfilePictureValueHolder = new ValueHolder<byte[]>();
            //ProfilePictureValueHolder = new Lazy<byte[]>(() =>
            //{
            //    return ProfilePictureService.GetFor(customer.Name);
            //});

            return base.All().Select(MapToProxy);
        }

        public override Customer Update(Customer entity)
        {
            var customer = context.Customers
                .Single(c => c.CustomerId == entity.CustomerId);

            customer.Name = entity.Name;
            customer.City = entity.City;
            customer.PostalCode = entity.PostalCode;
            customer.ShippingAddress = entity.ShippingAddress;
            customer.Country = entity.Country;

            return base.Update(entity);
        }

        private CustomerProxy MapToProxy(Customer customer)
        {
            return new CustomerProxy
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                ShippingAddress = customer.ShippingAddress,
                City = customer.City,
                PostalCode = customer.PostalCode,
                Country = customer.Country
            };
        }
    }
}
