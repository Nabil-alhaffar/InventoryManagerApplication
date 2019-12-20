using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace InventoryManager.MobileAppService.Models
{
    public class ProductRepository : IProductRepository
    {
        private static ConcurrentDictionary<string, Product> products =
            new ConcurrentDictionary<string, Product>();
        private SqlDbContext dbContext;

        public ProductRepository(SqlDbContext _db)
        {
            dbContext = _db;
        }
        //[Authorize]
        public IEnumerable<Product> GetAll() => dbContext.Products;

        public Product Get(int id) => dbContext.Products.Find(id);

        public void Add(Product product)
        {
            //product.Id = Guid.NewGuid().;

            dbContext.Products.Add (product);
            dbContext.SaveChanges();
        }

        public Product Remove(int id)
        {
            Product product = dbContext.Find<Product>(id);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            Product oldProduct = dbContext.Find<Product>(product.Id);
            dbContext.Products.Update(product);
            dbContext.SaveChanges();

        }
    }
}
