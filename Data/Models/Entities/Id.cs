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
        private const int MinLength = 14;
        private HashSet<char> Prefixes = new HashSet<char>{ 'M', 'P', 'L' };

        private Id(string raw)
        {
            Validate(raw);
            Prefix = raw[0];
            Position = Position.FromString(raw.Substring(1, 12));
            Trunk = raw.Substring(13);
        }

        private Id(char prefix, Position position, string trunk)
        {
            Prefix = prefix;
            Position = position;
            Trunk = trunk;
        }

        public readonly char Prefix;
        public readonly string Trunk;
        public Position Position;

        //Todo: This should be replaced by some sort of Type input and an actual generation of Id hash;
        public static Id FromString(string input)
        {
            return new Id(input);
        }

        public static Id FromString(char prefix, string input)
        {
            input = prefix + input;
            return new Id(input);
        }

        public static Id FromParts(char prefix, Position position, string trunk)
        {
            return new Id(prefix, position, trunk);
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

        public override bool Equals(object input)
        {
            if(input is Id that)
            {
                return Prefix.Equals(that.Prefix)
                    && Position.Equals(that.Position)
                    && Trunk.Equals(that.Trunk);
            }

            return false;
        }

        public override int GetHashCode()
        {
            //TODO2 for year 2140: If there are large lists of these Id's (unknown) we might need a stronger hashing here.
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Trunk.GetHashCode();
                hash = hash * 23 + Position.GetHashCode();
                hash = hash * 23 + Prefix.GetHashCode();
                return hash;
            }
            
        }
        
        public static bool operator == (Id a, Id b) => a.Equals(b);
        public static bool operator != (Id a, Id b) => !a.Equals(b);
    }
}
