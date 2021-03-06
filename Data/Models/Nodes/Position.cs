﻿using System;

namespace Data.Models.Nodes
{
    public struct Position
    {
        private readonly byte[] _internal;

        private Position(string input)
        {
            _internal = new byte[4];

            _internal[0] = byte.Parse(input.Substring(0, 3));
            _internal[1] = byte.Parse(input.Substring(3, 3));
            _internal[2] = byte.Parse(input.Substring(6, 3));
            _internal[3] = byte.Parse(input.Substring(9, 3));
        }

        private Position(byte continent, byte region, byte sector, byte location)
        {
            _internal = new byte[4];

            _internal[0] = continent;
            _internal[1] = region;
            _internal[2] = sector;
            _internal[3] = location;
        }

        public static Position FromString(string input)
        {
            if (input.Length != 12)
            {
                throw new ArgumentException("Positions must always be string of length 12, 3 for each digit in the positions. Padded with zeroes if needed (such as 001, 010 and 100)");
            }

            return new Position(input);
        }

        public static Position FromNumbers(byte continent, byte region, byte sector, byte location)
        {
            return new Position(continent, region, sector, location);
        }

        public Position StripLocation()
        {
            return FromNumbers(Continent, Region, Sector, 0);
        }

        public Position StripSector()
        {
            return FromNumbers(Continent, Region, 0, 0);
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

        public override string ToString()
        {
            return _internal[0].ToString() + _internal[1].ToString() + _internal[2].ToString() + _internal[3].ToString();
        }

        public static bool operator == (Position a, Position b) => a.Equals(b);
        public static bool operator != (Position a, Position b) => !a.Equals(b);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (byte item in _internal)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
                return hash;
            }
        }
    }
}
