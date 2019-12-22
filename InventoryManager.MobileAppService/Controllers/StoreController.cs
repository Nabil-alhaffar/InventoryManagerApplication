using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.Models;
using InventoryManager.MobileAppService.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStockRepository StockRepository;
        private readonly IStoreRepository StoreRepository;
        public StoreController(IStoreRepository storeRepository, IStockRepository stockRepository)
        {

            StoreRepository = storeRepository;
            StockRepository = stockRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Store>> List()
        {
            return StoreRepository.GetAll().ToList();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Store> GetStore(int id)
        {
            Store store = StoreRepository.Get(id);

            if (store == null)
                return NotFound();

            return store;
        }
  
        [HttpGet("storeName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Store> GetStoreByStoreName(string storeName)
        {
            Store store = StoreRepository.GetStoreByStoreName(storeName);

            if (store == null)
                return NotFound();

            return store;
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Store> Create([FromBody]Store store)
        {
            StoreRepository.Add(store);
            return CreatedAtAction(nameof(GetStore), new { store.StoreId }, store);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] Store store)
        {
            try
            {
                StoreRepository.Update(store);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}/stocks/PhoneItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <IEnumerable<Stock>> GetPhoneStocks(int id) =>  StockRepository.GetAllPhoneStocks(id).ToList();

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}/stocks/AccessoryItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Stock>> GetAccessoryStocks(int id) => StockRepository.GetAllAccessoriesStocks(id).ToList();

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}/stocks/Quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetStocksCount([FromBody]int id, [FromRoute]Product product) => StockRepository.GetProductCount(id, product.Id);


        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPost("{id}/stocks")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult <IEnumerable<Stock>> AddInventory(int id, [FromBody] IEnumerable<Stock> stocks)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    StockRepository.AddStocks(id, stocks);
                    return CreatedAtAction(nameof(stocks), new { stocks.First().ProductId }, stocks.First().StoreId);

                    //     StoreRepository.AddStocks(storeId, stocks);
                }
                catch (Exception)
                {
                    return BadRequest("");
                }
            }
            else
                return BadRequest("invalid modelstate");
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPost("{id}/stocks/subtract/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Stock>> RemoveInventory(int id, [FromBody]IEnumerable<Stock> stocks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StockRepository.SubtractStocks(id, stocks);
                    //  StoreRepository.SubtractStocks(storeId, products);
                }
                catch (Exception)
                {
                    return BadRequest("Error while subtracting inventory.");

                }
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid ModelState");
            }
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPost("{id}/stocks/reconcile/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Stock>> ReconcileInventory(int storeId, [FromBody]IEnumerable<Stock> stocks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StockRepository.ReconcileInventory(storeId, stocks);
                    //  StoreRepository.SubtractStocks(storeId, products);
                }
                catch (Exception)
                {
                    return BadRequest("Error while subtracting inventory.");

                }
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid ModelState");
            }
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}/stocks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Stock>> GetStoreStocks(int id)
        {
            IEnumerable<Stock> stocks = StockRepository.GetAllStoreStocks(id);

            if (stocks == null)
                return NotFound();

            return stocks.ToList();
        }
      



    }
}
