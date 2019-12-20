using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;
namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository OrderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Order>> List()
        {
            return OrderRepository.GetAll().ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Order> GetOrder(int id)
        {

            Order order = OrderRepository.Get(id);

            if (order == null)
                return NotFound();

            return order;
            
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> Create([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                OrderRepository.Add(order);
            }
            return CreatedAtAction(nameof(GetOrder), new { order.OrderId }, order);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] Order order)
        {
            try
            {
                OrderRepository.Update(order);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            Order order = new Order();
            if (ModelState.IsValid)
            {
                 order = OrderRepository.Remove(id);
            }
            if (order == null)
                return NotFound();

            return Ok();
        }

    }
}
