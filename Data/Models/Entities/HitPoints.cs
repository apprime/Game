using Newtonsoft.Json;

namespace Data.Models.Entities
{
    [JsonObject]
    public class HitPoints
    {
        [JsonProperty]
        public int Current { get; set; }
        [JsonProperty]
        public int Total { get; set; }
    }
}