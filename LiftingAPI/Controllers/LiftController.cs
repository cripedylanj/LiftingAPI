using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiftingAPI.Entities;
using LiftingAPI.Helpers;
using LiftingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiftingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/lifts")]
    public class LiftController : Controller
    {
        private IDocumentDBRepository<Lift> _repository;
        private IUpdateModel<Lift> _updateHelper;

        public LiftController(IDocumentDBRepository<Lift> repository,
            IUpdateModel<Lift> updateHelper)
        {
            _repository = repository;
            _updateHelper = updateHelper;
        }
        [HttpGet(Name = "GetAllLifts")]
        public IActionResult GetAllLifts()
        {
            var liftsToReturn = _repository.GetItemsAsync(r => (r.Type == "lift")).Result;
            return new JsonResult(liftsToReturn);
        }
        [HttpGet("{id}", Name = "GetLift")]
        public IActionResult GetLifter(String id)
        {
            var liftToGet = _repository.GetItemAsync(id).Result;
            if (liftToGet == null)
            {
                return NotFound();
            }
            return new JsonResult(liftToGet);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLift(string id)
        {
            _repository.DeleteItemAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateLift([FromBody] Lift lift, string id)
        {
            if (lift == null)
            {
                return BadRequest();
            }
            var liftToCreate = _updateHelper.CopyNonNullItems(lift, _repository.GetItemAsync(id).Result);
            if (liftToCreate == null)
            {
                return BadRequest();
            }
            var returnItem = _repository.UpdateItemAsync(id, liftToCreate).Result;
            return CreatedAtRoute("GetLifter", new { id = liftToCreate.Id }, liftToCreate);
        }
    }
}