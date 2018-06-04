using LiftingAPI.Entities;
using LiftingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LiftingAPI.Helpers;

namespace LiftingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/lifters")]
    public class LifterController : Controller
    {
        private IDocumentDBRepository<LifterModel> _repository;
        private IUpdateModel<LifterModel> _updateHelper;

        public LifterController(IDocumentDBRepository<LifterModel> repository,
            IUpdateModel<LifterModel> updateHelper)
        {
            _repository = repository;
            _updateHelper = updateHelper;
        }
        [HttpGet(Name = "GetAllLifters")]
        public IActionResult GetAllLifters()
        {
            var liftersToReturn = _repository.GetItemsAsync(r => (r.Type == "lifter")).Result;
            return new JsonResult(liftersToReturn);
        }
        [HttpGet("{id}", Name = "GetLifter")]
        public IActionResult GetLifter(String id)
        {
            var lifterToGet = _repository.GetItemAsync(id).Result;
            if (lifterToGet == null)
            {
                return NotFound();
            }
            return new JsonResult(lifterToGet);
        }
        [HttpPost]
        public IActionResult CreateLifter([FromBody] LifterModel lifter)
        {
            if (lifter == null)
            {
                return BadRequest();
            }
            var lifterToReturn = _repository.CreateItemAsync(lifter).Result;
            return CreatedAtRoute("GetLifter", new { id = lifterToReturn.Id}, lifterToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLifter(string id)
        {
            _repository.DeleteItemAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateLifter([FromBody] LifterModel lifter, string id)
        {
            if (lifter == null)
            {
                return BadRequest();
            }
            var lifterToCreate = _updateHelper.CopyNonNullItems(lifter, _repository.GetItemAsync(id).Result);
            if (lifterToCreate == null)
            {
                return BadRequest();
            }
            var returnItem = _repository.UpdateItemAsync(id, lifterToCreate).Result;
            return CreatedAtRoute("GetLifter", new { id = lifterToCreate.Id }, lifterToCreate);
        }
    }
}
