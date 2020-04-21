using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Ghosts;
using MyShop.Infrastructure.Lazy.Proxies;
using MyShop.Infrastructure.Services;
using MyShop.Infrastructure.ValueHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ShoppingContext context) : base(context)
        {
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

        public override IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            var result = base.Find(predicate);

            return result.Select(c => MapToProxy(c));
        }

        public override Customer Get(Guid id)
        {
            // Lazy: Ghost
            return new GhostCustomer(() => base.Get(id))
            {
                CustomerId = id
            };
        }

        public override IEnumerable<Customer> All()
        {
            return base.All().Select(c => MapToProxy(c));
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
                Country = customer.Country,
                ProfilePictureValueHolder = new ProfilePictureValueHolder(),
                ProfilePictureLazy = new Lazy<byte[]>(() =>
                {
                    return ProfilePictureService.GetFor(customer.Name);
                })
            };
        }
    }
}
