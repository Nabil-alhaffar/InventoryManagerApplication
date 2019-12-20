using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Services
{
    public class MockDataStore : DbContext, IDataStore<Product> 
    {
        public DbSet<Product> products { get; set; }

        public MockDataStore(): base()
        {
       //     SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();        
        }
        string sqlitePath = App.sqliteURL;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite($"Filename={sqlitePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Product>()
            .Property(p => p.Category).HasConversion(v => v.ToString(), v => (ProductType)Enum.Parse(typeof(ProductType), v)
                ) ;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            await products.AddAsync(product).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var oldProduct = products.Where((Product arg) => arg.Id == product.Id).FirstOrDefault();
            products.Remove(oldProduct);
            products.Add(product);
            await SaveChangesAsync().ConfigureAwait(false);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var oldProduct = products.Where((Product arg) => arg.Id == id).FirstOrDefault();
            products.Remove(oldProduct);
            await SaveChangesAsync().ConfigureAwait(false);

            return await Task.FromResult(true);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await Task.FromResult(products.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(products);
        }

    }
}