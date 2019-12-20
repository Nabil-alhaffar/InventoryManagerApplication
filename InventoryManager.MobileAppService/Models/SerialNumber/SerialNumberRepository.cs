using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace InventoryManager.MobileAppService.Models
{
    public class SerialNumberRepository: ISerialNumberRepository
    {
        private SqlDbContext dbContext;

        public SerialNumberRepository(SqlDbContext _db)
        {
            dbContext = _db;
        }

        public void Add(SerialNumber serialNumber)
        {
            dbContext.Add(serialNumber);
            dbContext.SaveChanges();
        }

        public SerialNumber Get(string imeiNumberValue) => dbContext.SerialNumbers.Find(imeiNumberValue);
       

        public IEnumerable<SerialNumber> GetAll() => dbContext.SerialNumbers;

        public int GetStoreFromSerialNumber(string imeiNumberValue) => dbContext.SerialNumbers.Find(imeiNumberValue).StoreId;
        

        public SerialNumber Remove(string imeiNumberValue)
        {
          SerialNumber serialNumber=  dbContext.SerialNumbers.Find(imeiNumberValue);
            dbContext.SerialNumbers.Remove(serialNumber);
            dbContext.SaveChanges();
            return serialNumber;
        }

        public void Update(SerialNumber serialNumber)
        {
            dbContext.SerialNumbers.Update(serialNumber);
            dbContext.SaveChanges();
        }
    }
}        