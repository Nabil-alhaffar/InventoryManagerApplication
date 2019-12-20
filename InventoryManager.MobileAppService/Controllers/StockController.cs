using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;
namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly IStockRepository StockRepository;
        private readonly ISerialNumberRepository SerialNumberRepository;
        public StockController(IStockRepository stockRepository, ISerialNumberRepository serialNumberRepository)
        {
            StockRepository = stockRepository;
            SerialNumberRepository = serialNumberRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Stock>> ListAllStocks()
        {
            return StockRepository.GetAllStocks().ToList();
        }
        
        [HttpGet("{storeId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Stock> GetSingleStock(int storeId, int productId)
        {
            Stock stock = StockRepository.GetStock(storeId, productId);
            if (stock == null)
                return NotFound();

            return stock;
        }

        [HttpPost("stock")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create([FromBody]Stock stock)
        {
            if (ModelState.IsValid)
            {
                StockRepository.AddSingleStock(stock.StoreId, stock);
            }
            return CreatedAtAction(nameof(GetSingleStock), new { stock.ProductId }, stock);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] Stock stock)
        {
            try
            {
                StockRepository.Update(stock);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }


    }
}


