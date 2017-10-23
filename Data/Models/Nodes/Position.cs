using System;

namespace Data.Models.Nodes
{
    public struct Position
    {
        private byte[] _internal;

        public Position(string input)
        {
            if(input.Length != 12)
            {
                throw new ArgumentException("Positions must always be string of length 12, 3 for each digit in the positions. Padded with zeroes if needed (such as 001, 010 and 100)");
            }

            _internal = new byte[4];

            _internal[0] = byte.Parse(input.Substring(0, 3));
            _internal[1] = byte.Parse(input.Substring(3, 3));
            _internal[2] = byte.Parse(input.Substring(6, 3));
            _internal[3] = byte.Parse(input.Substring(9, 3));
        }

        public static Position FromString(string input)
        {
            return new Position(input);
        }

        public byte Continent
        {
            get
            {
                return _internal[0];
            }
        }

        public byte Region
        {
            get
            {
                return _internal[1];
            }
        }

        public byte Sector
        {
            get
            {
                return _internal[2];
            }
        }

        public byte Location
        {
            get
            {
                return _internal[3];
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Position posB)
            {
                return Continent == posB.Continent
                    && Region == posB.Region
                    && Sector == posB.Sector
                    && Location == posB.Location;
            }
            else
            {
                return false;
            }
        }

        public static bool operator == (Position a, Position b) => a.Equals(b);
        public static bool operator != (Position a, Position b) => !a.Equals(b);

        public override int GetHashCode()
        {
            return _internal.GetHashCode();
        }
    }
}
