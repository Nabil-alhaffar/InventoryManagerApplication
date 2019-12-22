using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;

        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Product>> List()
        {
            return ProductRepository.GetAll().ToList();
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetItem(int id)
        {
            Product item = ProductRepository.Get(id);

            if (item == null)
                return NotFound();

            return item;
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Add(product);
            }
            return CreatedAtAction(nameof(GetItem), new { product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] Product item)
        {
            try
            {
                ProductRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "DistrictManager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            Product item = ProductRepository.Remove(id);

            if (item == null)
                return NotFound();

            return Ok();
        }
    }
}
