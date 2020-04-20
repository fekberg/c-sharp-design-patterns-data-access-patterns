using Microsoft.EntityFrameworkCore;
using MyShop.Business;
using MyShop.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Data
{
    public class CustomerRepository : IRepository<Customer>
    {
        private ShoppingContext Context { get; }

        public CustomerRepository(ShoppingContext context)
        {
            Context = context;
        }

        public Customer Add(Customer entity)
        {
            return Context.Customers
                .Add(entity)
                .Entity;
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            return Context.Customers.Where(predicate).ToList();
        }

        public Customer Get(Guid id)
        {
            return Context.Customers.Single(customer => customer.CustomerId == id);
        }

        public IEnumerable<Customer> All()
        {
            return Context.Customers.ToList();
        }

        public Customer Update(Customer entity)
        {
            var customer = Context.Customers
                .Single(c => c.CustomerId == entity.CustomerId);

            customer.Name = entity.Name;
            customer.PostalCode = entity.PostalCode;
            customer.ShippingAddress = entity.ShippingAddress;
            customer.Country = entity.Country;

            return Context.Customers
                .Update(customer)
                .Entity;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
