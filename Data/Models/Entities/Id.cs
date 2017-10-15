using Data.Models.Nodes;
using System;
using System.Collections.Generic;

namespace Data.Models.Entities
{
    /// <summary>
    /// All Entities have a unique Id. 
    /// In order to be able to quickly identify the type, a char is used as a prefix.
    /// 
    /// </summary>
    public class Id
    {
        private const int MaxLength = 20;
        private const int MinLength = 6;
        private HashSet<char> Prefixes = new HashSet<char>{ 'M', 'P', 'S' };

        private Id(string raw)
        {
            Validate(raw);
            Prefix = raw[0];
            Position = Position.FromString(raw.Substring(1, 4));
            Trunk = raw.Substring(5);
        }

        private Id(char prefix)
        {
            Prefix = prefix;
            Trunk = GenerateTrunk();
        }

        private static string GenerateTrunk()
        {
            return (new Random().Next() * 1000).ToString();
        }

        //private static Random GetRandom()
        //{

        //}

        private static Random GetRandom(int seed)
        {
            return new Random(seed);
        }

        public readonly char Prefix;
        public readonly Position Position;
        public readonly string Trunk;

        //Todo: This should be replaced by some sort of Type input and an actual generation of Id hash;
        public static Id FromString(string input)
        {
            return new Id(input);
        }

        private void Validate(string raw)
        {
            if(raw.Length > MaxLength)
            {
                throw new ArgumentException("The given raw Id is too long");
            }

            if(raw.Length < MinLength)
            {
                throw new ArgumentException("The given raw id is too short");
            }

            if(!Prefixes.Contains(raw[0]))
            {
                throw new ArgumentException("The prefix of given raw Id is not a valid prefix");
            }
        }

        public static Id Create(char prefix)
        {
            return new Id(prefix);
        }

        public override bool Equals(object input)
        {
            if(input is Id that)
            {
                return Prefix.Equals(that.Prefix) && Trunk.Equals(that.Trunk);
            }

            //Not the correct type
            return false;
        }

        public override int GetHashCode()
        {
            //TODO: I am undecided on whether Trunk should be unique on its own
            // If it is, then these acrobatics are unnecessary.

            //TODO2 for year 2140: If there are large lists of these Id's (unknown) we might need a stronger hashing here.
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Trunk.GetHashCode();
                hash = hash * 23 + Prefix.GetHashCode();
                return hash;
            }
            
        }
    }
}
