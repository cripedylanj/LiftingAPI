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
    public class LiftTrackerController : Controller
    {
        private IDocumentDBRepository<Lift> _repository;
        private IUpdateModel<Lift> _updateHelper;

        public LiftTrackerController(IDocumentDBRepository<Lift> repository,
            IUpdateModel<Lift> updateHelper)
        {
            _repository = repository;
            _updateHelper = updateHelper;
        }
        public IActionResult Index()
        {
            var lifts = _repository.GetItemsAsync(l => (l.Type == "lift")).Result;
            return View(lifts);
        }
        public IActionResult CreateLift()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLift(Lift lift)
        {
            Lift liftToAdd = new Lift
            {
                Type = "lift",
                Name = lift.Name,
                TypeOfLift = lift.TypeOfLift,
                Difficulty = lift.Difficulty,
                WeeklySets = new WeeklySet[0]
            };
            var liftToReturn = _repository.CreateItemAsync(liftToAdd).Result;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult LiftDetails(string id)
        {
            Lift lift = _repository.GetItemAsync(id).Result;
            return View(lift);
        }
        [HttpPost]
        public IActionResult LiftDetails(Lift lift, string id)
        {
            Lift liftTemp = lift;
            liftTemp.WeeklySets = new WeeklySet[20];
            liftTemp.WeeklySets = lift.WeeklySets;
            Lift updatedLift = _updateHelper.CopyNonNullItems(lift,
                _repository.GetItemAsync(id).Result);
            _repository.UpdateItemAsync(id, updatedLift);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            _repository.DeleteItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditLift(string id)
        {
            return View("CreateLift", _repository.GetItemAsync(id).Result);
        }
        [HttpPost]
        public IActionResult EditLift(Lift lift, string id)
        {
            var liftToCreate = _updateHelper.CopyNonNullItems(lift, _repository.GetItemAsync(id).Result);
            if (liftToCreate == null)
            _repository.UpdateItemAsync(id, liftToCreate);
            return RedirectToAction(nameof(LiftDetails), new { id = liftToCreate.Id });
        }
        public IActionResult CreateNewWeek(string id)
        {
            return View(_repository.GetItemAsync(id).Result);
        }
        [HttpPost]
        public IActionResult CreateNewWeek(int[] set, string id)
        {
            int[] reps = new int[set.Length / 2];
            int[] weight = new int[set.Length / 2];
            int j = 0;
            for (int i = 0; i < set.Length; i += 2)
            {
                reps[j] = set[i];
                weight[j] = set[i + 1];
                j++;
            }
            WeeklySet newWeek = new WeeklySet
            {
                Reps = reps,
                Weight = weight
            };
            Lift temp = _repository.GetItemAsync(id).Result;
            Lift lift = new Lift
            {
                WeeklySets = new WeeklySet[temp.WeeklySets.Length + 1]
            };
            for (int i = 0; i < temp.WeeklySets.Length; i++)
            {
                lift.WeeklySets[i] = temp.WeeklySets[i];
            }
            lift.WeeklySets[temp.WeeklySets.Length] = newWeek;
            var liftToCreate = _updateHelper.CopyNonNullItems(lift, _repository.GetItemAsync(id).Result);
            _repository.UpdateItemAsync(id, liftToCreate);
            return new JsonResult(Url.Action(nameof(LiftDetails), new { id }));
        }
        public IActionResult DeleteWeek(int week, string id)
        {
            Lift temp = _repository.GetItemAsync(id).Result;
            Lift lift = new Lift
            {
                WeeklySets = new WeeklySet[temp.WeeklySets.Length - 1]
            };
            for (int i = 0; i < week - 1; i++)
            {
                lift.WeeklySets[i] = temp.WeeklySets[i];
            }
            for (int i = week - 1; i < temp.WeeklySets.Length - 1; i++)
            {
                lift.WeeklySets[i] = temp.WeeklySets[i + 1];
            }
            var liftToCreate = _updateHelper.CopyNonNullItems(lift, _repository.GetItemAsync(id).Result);
            _repository.UpdateItemAsync(id, liftToCreate);
            return RedirectToAction(nameof(LiftDetails), new { id });
        }
    }
}