using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftingAPI.Models
{
    public class Lift
    {
        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "typeoflift")]
        public string TypeOfLift { get; set; }
        [JsonProperty(PropertyName = "difficulty")]
        public string Difficulty { get; set; }
        [JsonProperty(PropertyName = "weeklysets")]
        public WeeklySet[] WeeklySets { get; set; }
    }
}
