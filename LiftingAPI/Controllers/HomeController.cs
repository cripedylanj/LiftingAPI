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
    public class HomeController : Controller
    {
        private IDocumentDBRepository<LifterModel> _repository;
        private IUpdateModel<LifterModel> _updateHelper;

        public HomeController(IDocumentDBRepository<LifterModel> repository, 
            IUpdateModel<LifterModel> updateHelper)
        {
            _repository = repository;
            _updateHelper = updateHelper;
        }
        public IActionResult Index()
        {
            var lifters = _repository.GetItemsAsync(l => (l.Type == "lifter")).Result;
            return View(lifters);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LifterModel lifter)
        {
            LifterModel lifterToAdd = new LifterModel
            {
                Type = "lifter",
                Name = lifter.Name,
                Gender = lifter.Gender,
                Age = lifter.Age,
                Weight = lifter.Weight,
                LiftingNumbers = new LiftingNumbersModel
                {
                    Squat = lifter.LiftingNumbers.Squat,
                    Bench = lifter.LiftingNumbers.Bench,
                    Deadlift = lifter.LiftingNumbers.Deadlift
                }
            };
            var lifterToReturn = _repository.CreateItemAsync(lifterToAdd).Result;
            return RedirectToAction(nameof(Details), new { id = lifterToReturn.Id });
        }
        public IActionResult Details(string id)
        {
            var model = _repository.GetItemAsync(id).Result;
            return View(model);
        }
        public IActionResult Edit(string id)
        {
            var model = _repository.GetItemAsync(id).Result;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(LifterModel lifter, string id)
        {
            var lifterToCreate = _updateHelper.CopyNonNullItems(lifter, _repository.GetItemAsync(id).Result);
            var lifterToReturn = _repository.UpdateItemAsync(id, lifterToCreate).Result;
            return RedirectToAction(nameof(Details), new { id = lifterToReturn.Id });
        }
        public IActionResult Delete(string id)
        {
            _repository.DeleteItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CalculateWilks()
        {
            return View();
        }
    }
}