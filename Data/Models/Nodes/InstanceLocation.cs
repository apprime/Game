using Data.Models.Gamestate;
using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class InstanceLocation : Location
    {
        public InstanceLocation(string name, int position)
            : base(name, position)
        {
            //TODO: Not sure if we need this. It should probably be the other way around, Scene keeps track of Location
            Instances = new Dictionary<int, Scene>();
        }

        public static Dictionary<int, Scene> Instances { get; set; }
    }
}
