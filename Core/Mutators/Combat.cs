using Data.Models.Entities;
using Data.Models.Exceptions;
using System;


namespace Core.Mutators
{
    /// <summary>
    /// Handles mutation of combat related actions.
    /// </summary>
    public static class Combat
    {
        public static Damage Attack(IAttack attacker, Damage payload)
        {
            throw new TodoException("Cannot attack yet");    
        }

        public static Damage Mitigate(IDestructible defender, Damage payload)
        {
            throw new TodoException("Cannot defend yet");
        }

        internal static void Setup(Damage damage)
        {
            throw new NotImplementedException();
        }

        internal static void Cleanup(Damage damage)
        {
            throw new NotImplementedException();
        }
    }
}
