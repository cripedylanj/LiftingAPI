using LiftingAPI.Controllers;
using LiftingAPI.Entities;
using LiftingAPI.Helpers;
using LiftingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LiftingAPI.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestController()
        {
            var repo = new DocumentDBRepository<LifterModel>();
            var helper = new UpdateModel<LifterModel>();
            var controller = new LifterController(repo, helper);
            LifterModel testLifter = new LifterModel
            {
                Age = "19",
                Name = "Dylan",
                Weight = "185",
                LiftingNumbers = new LiftingNumbersModel
                {
                    Squat = "385",
                    Bench = "275",
                    Deadlift = "485"
                }
            };
            controller.CreateLifter(testLifter);
            var model = repo.CreateItemAsync(testLifter).Result;
            JsonResult result = (JsonResult) controller.GetLifter(model.Id);
            testLifter.Id = model.Id;
            JsonResult expected = new JsonResult(testLifter);
            Assert.AreEqual(result.ToString(), expected.ToString());
        }
    }
}
