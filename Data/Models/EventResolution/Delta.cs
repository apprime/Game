using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Entities.Humans;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Data.Models.EventResolution
{
    public class Delta
    {
        //Todo: is actor, targets key and value enough to describe a delta? Rollbacks?
        public Delta()
        {
        }

        [JsonConstructor]
        public Delta(IEntity actor, IEnumerable<Player> targets)
        {
            Actor = actor;
            Targets = targets;
        }

        public string Message { get; set; }
        public IEntity Actor { get; set; }
        public IEnumerable<IEntity> Targets { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
