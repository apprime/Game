using System;

namespace Data.Models.Nodes
{
    public struct Position
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

        public char Continent
        {
            get
            {
                return _internal[0];
            }
        }

        public string Region
        {
            get
            {
                return _internal.Substring(0, 2);
            }
        }

        public string Sector
        {
            get
            {
                return _internal.Substring(0, 3);
            }
        }

        public static Position FromString(string input)
        {
            return new Position(input);
        }

        public string Location
        {
            get
            {
                return _internal;
            }
        }

        public override string ToString()
        {
            return Location.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Position posB)
            {
                return Location == posB.Location;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            //Location is the entire value, 4 chars as a string.
            return Location.GetHashCode();
        }
    }
}
