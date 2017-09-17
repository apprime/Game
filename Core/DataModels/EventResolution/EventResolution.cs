using Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Core.EventResolution
{
    internal enum EventResolutionType
    {
        Rollback = 0,
        Commit = 1
    }

    [Flags]
    internal enum EventTargets
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
        internal EventTargets Targets { get; set; }

        [JsonProperty]
        internal IEntity Actor { get; set; }

        [JsonProperty]
        internal EventResolutionType Resolution { get; set; }

        [JsonProperty]
        internal List<Delta> Deltas { get; set; }
        #endregion

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}