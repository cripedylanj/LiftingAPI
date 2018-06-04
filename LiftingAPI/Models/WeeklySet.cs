using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftingAPI.Models
{
    public class WeeklySet
    {
        [JsonProperty(PropertyName = "reps")]
        public int[] Reps { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public int[] Weight { get; set; }
    }
}
