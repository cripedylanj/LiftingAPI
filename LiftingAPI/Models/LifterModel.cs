using Newtonsoft.Json;
using System;

namespace LiftingAPI.Models
{
    public class LifterModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }
        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "liftingnumbers")]
        public LiftingNumbersModel LiftingNumbers { get; set; }
    }
}
