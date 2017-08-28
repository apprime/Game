using Core.Entities;
using Core.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
