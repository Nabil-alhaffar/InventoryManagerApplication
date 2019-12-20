using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManager.MobileAppService.Models;
namespace InventoryManager.MobileAppService.Controllers
{
    [Route("api/serialNumber")]
    [ApiController]
    public class SerialNumberController:ControllerBase
    {
        private readonly ISerialNumberRepository SerialNumberRepository;
        public SerialNumberController(ISerialNumberRepository serialNumberRepository)
        {
            SerialNumberRepository = serialNumberRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SerialNumber>> List()
        {
            return SerialNumberRepository.GetAll().ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SerialNumber> GetSerialNumber(string id)
        {
            SerialNumber serialNumber = SerialNumberRepository.Get(id);

            if (serialNumber == null)
                return NotFound();

            return serialNumber;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SerialNumber> Create(SerialNumber serialNumber)
        {
            if (ModelState.IsValid)
            {
                SerialNumberRepository.Add(serialNumber);
            }
            return CreatedAtAction(nameof(GetSerialNumber), new { serialNumber.SerialNumberValue }, serialNumber);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] SerialNumber serialNumber)
        {
            try
            {
                SerialNumberRepository.Update(serialNumber);
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
        public ActionResult Delete(string id)
        {
            SerialNumber serialNumber = SerialNumberRepository.Remove(id);

            if (serialNumber == null)
                return NotFound();

            return Ok();
        }


    }
}