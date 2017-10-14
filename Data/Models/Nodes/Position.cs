using Data.Repositories;
using System;

namespace Data.Models.Nodes
{
    class Position
    {
        private string _internal;

        public Position(string input)
        {
            if(input.Length != 4)
            {
                throw new ArgumentException("Positions must always be string of length 4");
            }
            _internal = input;
        }

        public Region Region { get
            {
                return (new RegionRepository()).Get(_internal[1]);
            }
        }

        public Sector Sector
        {
            get
            {
                return (new SectorRepository()).Get(_internal[3]);
            }
        }

    }
}
