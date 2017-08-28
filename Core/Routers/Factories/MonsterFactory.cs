using Core.Entites.Monsters;
using System;

namespace Core.Factories
{
    internal static class MonsterFactory
    {
        internal static Monster Hydrate(Monster monster)
        {
            throw new NotImplementedException();
        }

        //TODO: Fix templates so that it is actually a useful thing to switch on
        internal static Monster Create(string template)
        {
            switch(template)
            {
                case "A":
                    return new AdmiralAardwark();
                default:
                    throw new ArgumentException("Failed trying to create Monster with template: " + template);
            }
        }
    }
}
