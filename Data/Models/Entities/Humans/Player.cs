using Data.Models.Entities.EntityInterfaces;
using Data.Models.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models.Entities.Humans
{
    public class Player : IPositioned, IActor
    {
        private Location _location;

        //TODO: Dont allow this, use a proper way to instatiate objects
        public Player(string data)
        {
            var dataParts = data.Split('/');

            Id = Id.FromString(dataParts[0]);
            Name = dataParts[1];
            Score = int.Parse(dataParts[2]);
            ConnectionId = dataParts[3];
            Party = Enumerable.Empty<IEntity>();
            LoggedOutPosition = new Position("001001001001"); //Todo: This is hardcoded to always spawn player in first hardcoded location. 
        }

        [JsonConstructor]
        public Player(string id, string name, int score, Location location, IEnumerable<Player> party)
        {
            Id = Id.FromString(id);
            Name = name;
            Score = score;
            Location = location;
            Party = party;
        }

        public string Name { get; set; }

        public Id Id { get; set; }

        //TODO: I think we can use string to identify a user in SignalR.
        //      Also, I think groups could and should be used, question is if we need grouping 
        //      on SignalR level or whether we just use the gamestate for this.
        public string ConnectionId { get; set; }
        public IEnumerable<string> ConnectionGroups { get; set; }
        public Position LoggedOutPosition { get; set; } //Todo: This needs a default value;


        public int Score { get; set; }

        public IEnumerable<IEntity> Party { get; internal set; }

        public Position Position
        {
            get => Id.Position;
            set => Id.Position = value;
        }

        public Location Location
        {
            get => _location;
            set 
            {
                Id.Position = value.Position; //The id needs updating so that the repos know where to look.
                _location = value;
            }
        }

        public HitPoints HitPoints { get; }

        //Todo: These are values needed by CombatMutator. 
        //They should exist on a spell or monster only. (Player is not IAttack)
        public int Damage { get; } = 2;
        public string AttackName { get; } = "Placeholder strike";
        public DamageType DamageType { get; } = DamageType.Physical;

        public Damage Mitigate(IAttack attacker, Damage payload)
        {
            throw new NotImplementedException();
        }

        public Spellbook Spellbook { get; set; }
        public Ink Currency { get; set; }
        public Bucket Bucket{ get; set; }
    }
}
