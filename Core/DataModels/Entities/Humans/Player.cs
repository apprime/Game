using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

namespace Core.Entities.Humans
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

        #region TEMP Event Management 

        private Connection c = new Connection(); //TODO: This needs to go away at some point
        public bool Send(string eventString)
        {
            Debug.WriteLine("Receiving message: ");
            Debug.WriteLine(eventString);
            c.Send(eventString);

            return true;
        }
        //Placeholder for Websockets connection
        private class Connection
        {
            public void Send(string message)
            {
                Debug.WriteLine("Linking message to Websocket");
            }
        }

        #endregion

        public string Name { get; set; }

        public Id Id { get; set; }

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
