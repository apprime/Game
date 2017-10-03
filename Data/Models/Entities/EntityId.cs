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
        private const int MaxLength = 4; //Todo: set these 
        private const int MinLength = 4;
        private HashSet<char> Prefixes = new HashSet<char>{ 'M', 'P', 'S' };

        private Id(string raw)
        {
            //Todo: Validate raw input
            Validate(raw);
            Prefix = raw[0];
            Trunk = raw.Substring(1);
        }

        public char Prefix { get; private set; }
        public string Trunk { get; private set; }

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

        public override bool Equals(object input)
        {
            if(input is Id that)
            {
                return Prefix.Equals(that.Prefix) && Trunk.Equals(that.Trunk);
            }

            //Not the correct type
            return false;
        }
    }
}
