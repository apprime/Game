using Core.EventResolution;

namespace Core
{
    //Hmm.
    internal static class EnumExtension
    {
        //This replaces Enum.HasFlag(), which does some undesired things.
        internal static bool Contains(this EventTargets a, EventTargets b)
        {
            return (a & b) != 0;
        }
    }
}
