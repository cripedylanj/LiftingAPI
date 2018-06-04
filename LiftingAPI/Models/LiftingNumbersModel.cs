using Newtonsoft.Json;

namespace LiftingAPI.Models
{
    public class LiftingNumbersModel
    {
        [JsonProperty(PropertyName = "squat")]
        public string Squat { get; set; }
        [JsonProperty(PropertyName = "bench")]
        public string Bench { get; set; }
        [JsonProperty(PropertyName = "deadlift")]
        public string Deadlift { get; set; }
    }
}
