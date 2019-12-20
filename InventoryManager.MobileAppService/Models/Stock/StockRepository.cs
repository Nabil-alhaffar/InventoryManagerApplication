using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.MobileAppService.Models
{
    public class StockRepository : IStockRepository
    {
        private SqlDbContext dbContext;

        public StockRepository(SqlDbContext _db)
        {
            dbContext = _db;

        }

        public void AddStocks(int storeId, IEnumerable<Stock> stocks)
        {
            foreach (Stock stock in stocks)
            {
                AddSingleStock(storeId, stock);
            }

        }
        public IEnumerable<Stock> GetAllStocks() => dbContext.Stocks.Include(s=>s.SerialNumbers).ToList();
        public IEnumerable<Stock> GetAllStoreStocks(int storeId) => dbContext.Stocks.Where(x => x.StoreId == storeId).Include(i =>i.SerialNumbers).ToList();
       // public IEnumerable<Stock> GetAllStoreStocks(int storeId) => dbContext.Stores.Find(storeId).Stocks.ToList();

        public IEnumerable<Stock> GetAllAccessoriesStocks(int storeId) => dbContext.Stores.Find(storeId).Stocks.Where(y => y.Product.ProductType == "Accessory").ToList();
        //public IEnumerable<Stock> GetAllAccessoriesStocks(int storeId) => dbContext.Stores.Find(storeId).Stocks.Where(y => y.Product.Category == ProductType.AccessoryItem).ToList();

        public IEnumerable<Stock> GetAllPhoneStocks(int storeId) =>  dbContext.Stocks.Where(s=>s.StoreId == storeId).Where(y => y.Product.ProductType == "Phone").Include(i => i.SerialNumbers).ToList();
       // public IEnumerable<Stock> GetAllPhoneStocks(int storeId) => dbContext.Stores.Find(storeId).Stocks.Where(y => y.Product.Category == ProductType.PhoneItem).ToList();

        public int GetProductCount(int storeId, int productId) 
        {
            Stock chosenStock = GetStock(storeId, productId);
            return chosenStock.Quantity;

        }

        public Stock GetStock(int storeId, int productId) => dbContext.Stocks.Find(storeId, productId);
        //Where((Stock arg) => arg.ProductId == productId).w Include(s => s.SerialNumbers).FirstOrDefault();

        //dbContext.Stores.Find(storeId).Stocks.Where(x => x.ProductId == productId).FirstOrDefault();
        //Storess.Stocks.Where((Stock arg) => arg.ProductId == productId).Include(s=>s.IMEInumbers).FirstOrDefault();

        public void SubtractStocks(int storeId, IEnumerable<Stock> stocks)
        {
            foreach (Stock stock in stocks)
            {
                SubtractSingleStock(storeId, stock);
            }

        }

        public string GetStockType(int productId) => dbContext.Products.Find(productId).ProductType;

        public void AddSingleStock(int storeId, Stock stock)
        {
            if (dbContext.Products.Find(stock.ProductId) != null)
            {
                //Store store = dbContext.Stores.Include(s => s.Stocks).
                //                                   ThenInclude(s => s.SerialNumbers).
                //                                   Include(o => o.Orders).
                //                                   ThenInclude(o => o.OrderItems).
                //                                   Include(s => s.Users).
                //                                   FirstOrDefault(i => i.StoreId == storeId);
                Stock chosenStock = GetStock(storeId, stock.ProductId);
                if (chosenStock != null)
                {
                    //dbContext.Stocks.Find(stock.ProductId).Quantity += stock.Quantity;
                    
                    //store.Stocks.First(s => s.ProductId == stock.ProductId).Quantity += stock.Quantity;
                    chosenStock.Quantity += stock.Quantity;
                    if (GetStockType(stock.ProductId).ToLower() == "phone")
                    {

                        foreach (SerialNumber imeiNumber in stock.SerialNumbers)
                        {
                            //dbContext.Stocks.Find(stock.ProductId).SerialNumbers.Add(imeiNumber);
                            //   store.Stocks.First(s => s.ProductId == stock.ProductId).SerialNumbers.Add(imeiNumber);
                            dbContext.SerialNumbers.Add(imeiNumber);
                            //store.Stocks.First(s => s.ProductId == stock.ProductId).SerialNumbers.Add(imeiNumber);
                        }
                    }
                }
                else
                {
                    dbContext.Stocks.Add(stock);
                    //store.Stocks.Add(stock);
                }
            }
           
            
            dbContext.SaveChanges();

        }
        public void SubtractSingleStock(int storeId, Stock stock)
        {
            Stock chosenStock = GetStock(storeId, stock.ProductId);
            if (chosenStock != null && GetProductCount(storeId, stock.ProductId) >= stock.Quantity)
            {
                //dbContext.Stocks.Find(stock.ProductId).Quantity -= stock.Quantity;
                //Store store = dbContext.Stores.Include(s => s.Stocks).
                //                                     ThenInclude(s => s.SerialNumbers).
                //                                    Include(o => o.Orders).
                //                                    ThenInclude(o => o.OrderItems).
                //                                    Include(s => s.Users).
                //                                    FirstOrDefault(i => i.StoreId == storeId);
                
                    chosenStock.Quantity -= stock.Quantity;

                    //store.Stocks.First(s => s.ProductId == stock.ProductId).Quantity -= stock.Quantity;
                    if (GetStockType(stock.ProductId).ToLower() == "phone")
                    {
                        foreach (SerialNumber imeiNumber in stock.SerialNumbers)
                        {
                            // dbContext.Stocks.Find(stock.ProductId).SerialNumbers.Remove(imeiNumber);
                            //    store.Stocks.First(s => s.ProductId == stock.ProductId).SerialNumbers.Remove(imeiNumber);
                            //store.Stocks.First(s => s.ProductId == stock.ProductId).SerialNumbers.Remove(imeiNumber);
                            dbContext.SerialNumbers.Remove(imeiNumber);

                        }
                    }
                

            }
            dbContext.SaveChanges();

        }
        public void ReconcileInventory(int storeId, IEnumerable<Stock> stocks)
        {
            IEnumerable<Stock> chosenStocks = GetAllStoreStocks(storeId);
            SubtractStocks(storeId,chosenStocks );

            
            AddStocks(storeId, stocks);
        }
        public void Update (Stock stock)
        {


            //Stock oldStock = dbContext.Find<Stock>(stock.ProductId);
            dbContext.Stocks.Update(stock);
            dbContext.SaveChanges();
        }
    }
}
