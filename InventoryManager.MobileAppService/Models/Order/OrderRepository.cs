using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.MobileAppService.Models
{
    public class OrderRepository :IOrderRepository
    {
        private SqlDbContext dbContext;

        public OrderRepository(SqlDbContext _db)
        {
            dbContext = _db;
        }
        public IEnumerable<Order> GetAll() => dbContext.Orders.Include(x =>x.OrderItems);


        public void Add(Order order)
        {
         //   order.OrderId = Guid.NewGuid().ToString();

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
        public Order Get(int id) => dbContext.Orders.Find(id);

        public Order Remove(int id)
        {
            Order order = dbContext.Find<Order>(id);
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
            return order;
        }
        public void Update(Order order)
        {
            Order oldProduct = dbContext.Find<Order>(order.OrderId);
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();

        }
    }
}
