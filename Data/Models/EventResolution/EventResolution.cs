using Data.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Data.Models.EventResolution
{
    public enum EventResolutionType
    {
        Rollback = 0,
        Commit = 1
    }

    [Flags]
    public enum EventTargets
    {
        None = 0,
        Player = 1,
        Target = 2,
        Party = 4,
        Nearby = 8,
        Friendly = 16,
        Hostile = 32,
        World = 64,
    }

    public static class EnumExtensions
    {
        //This replaces Enum.HasFlag(), which does some undesired things.
        public static bool Contains(this EventTargets a, EventTargets b)
        {
            return (a & b) != 0;
        }
    }

    [JsonObject]
    public class EventResult
    {
        public EventResult()
        {
            Deltas = new List<Delta>();
        }

        #region Properties
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventTargets Targets { get; set; }

        [JsonProperty]
        public IEntity Actor { get; set; }

        [JsonProperty]
        public EventResolutionType Resolution { get; set; }

        [JsonProperty]
        public List<Delta> Deltas { get; set; }
        #endregion

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}