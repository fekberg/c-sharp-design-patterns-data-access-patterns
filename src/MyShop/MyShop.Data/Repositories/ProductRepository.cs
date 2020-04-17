using MyShop.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Data
{
    public class ProductRepository : IRepository<Product>
    {
        private ShoppingContext Context { get; }

        public ProductRepository(ShoppingContext context)
        {
            Context = context;
        }

        public Product Add(Product entity)
        {
            return Context.Products
                .Add(entity)
                .Entity;
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return Context.Products.Where(predicate).ToList();
        }

        public Product Get(Guid id)
        {
            return Context.Products.Single(product => product.ProductId == id);
        }

        public IEnumerable<Product> All()
        {
            return Context.Products.ToList();
        }

        public Product Update(Product entity)
        {
            var product = Context.Products
                .Single(p => p.ProductId == entity.ProductId);

            product.Price = entity.Price;
            product.Name = entity.Name;

            return Context.Products.Update(product)
                .Entity;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
