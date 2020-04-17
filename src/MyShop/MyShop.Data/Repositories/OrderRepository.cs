using Microsoft.EntityFrameworkCore;
using MyShop.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Data
{
    public class OrderRepository : IRepository<Order>
    {
        private ShoppingContext Context { get; }

        public OrderRepository(ShoppingContext context)
        {
            Context = context;
        }

        public Order Add(Order entity)
        {
            return Context.Orders
                .Add(entity)
                .Entity;
        }

        public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            return Context.Orders
                .Include(order => order.LineItems)
                .ThenInclude(lineItem => lineItem.Product)
                .Where(predicate).ToList();
        }

        public Order Get(Guid id)
        {
            return Context.Orders.Single(order => order.OrderId == id);
        }

        public IEnumerable<Order> All()
        {
            return Context.Orders.ToList();
        }

        public Order Update(Order entity)
        {
            var order = Context.Orders
                .Include(o => o.LineItems)
                .ThenInclude(lineItem => lineItem.Product)
                .Single(o => o.OrderId == entity.OrderId);

            order.OrderDate = entity.OrderDate;
            order.LineItems = entity.LineItems;

            return Context.Orders.Update(order)
                .Entity;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
