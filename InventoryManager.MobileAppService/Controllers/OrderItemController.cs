using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;

namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/orderitem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository OrderItemRepository;
        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            OrderItemRepository = orderItemRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderItem>> List()
        {
            return OrderItemRepository.GetAll().ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderItem> GetOrderItem(int id)
        {

            OrderItem orderItem = OrderItemRepository.Get(id);

            if (orderItem == null)
                return NotFound();

            return orderItem;

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OrderItem> Create([FromBody]OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                OrderItemRepository.Add(orderItem);
            }
            return CreatedAtAction(nameof(GetOrderItem), new { orderItem.OrderId }, orderItem);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] OrderItem orderItem)
        {
            try
            {
                OrderItemRepository.Update(orderItem);
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
            OrderItem orderItem = new OrderItem();
            if (ModelState.IsValid)
            {
                orderItem = OrderItemRepository.Remove(id);
            }
            if (orderItem == null)
                return NotFound();

            return Ok();
        }

    }
}
