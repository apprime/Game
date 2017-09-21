using Data.Models.Gamestate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Data.Models.Entities.Humans
{
    public class Player : IEntity, IActor
    {
        //TODO: Dont allow this, use a proper way to instatiate objects
        public Player(string data)
        {
            var dataParts = data.Split('/');

            Id = Id.FromString(dataParts[0]);
            Name = dataParts[1];
            Score = int.Parse(dataParts[2]);
            Party = Enumerable.Empty<IEntity>();
        }

        [JsonConstructor]
        public Player(string id, string name, int score, Scene scene, IEnumerable<Player> party)
        {
            Id = Id.FromString(id);
            Name = name;
            Score = score;
            Scene = scene;
            Party = party;
        }

        public string Name { get; set; }

        public Id Id { get; set; }

        //TODO: I think we can use string to identify a user in SignalR.
        //      Also, I think groups could and should be used, question is if we need grouping 
        //      on SignalR level or whether we just use the gamestate for this.
        public string ConnectionId { get; set; }
        public IEnumerable<string> ConnectionGroups { get; set; }

        public int Score { get; set; }

        public IEnumerable<IEntity> Party { get; internal set; }

        public Scene Scene { get; set; }

        public HitPoints HitPoints { get; }

        public Damage Attack(IDestructible target, Damage payload)
        {
            const string attackName = "Placeholder strike";
            const int damage = 2;
            DamageType dt = DamageType.Physical;

            payload.Actor = this;
            payload.Total = damage;
            payload.DamageType = dt.ToString("G");
            payload.Name = attackName;
            payload.Target = target;

            return payload;
        }

        //TODO: Placeholder. This is to be used for AoE and other special effects
        public IEnumerable<Damage> Attack(IEnumerable<IDestructible> targets, Damage payload)
        {
            foreach (var t in targets)
            {
               yield return Attack(t, new Damage());
            }
        }

        public Damage Mitigate(IAttack attacker, Damage payload)
        {
            throw new NotImplementedException();
        }

        public Spellbook Spellbook { get; set; }
        public Ink Currency { get; set; }
        public Bucket Bucket{ get; set; }


    }
}
