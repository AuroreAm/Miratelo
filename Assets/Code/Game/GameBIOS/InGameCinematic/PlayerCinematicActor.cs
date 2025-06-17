using Pixify;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    public static class PlayerCinematicActorLibrary
    {
        public static action Dummy ()
        {
            new ac_idle ();
            return TreeFinalize ();
        }
    }
}