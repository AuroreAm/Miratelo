using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class guard : thought.chain
    {
        reaction [] Reactions;
        int [] ReflexionKeys;
        chain main;

        protected override void OnAquire()
        {
            for (int i = 0; i < Reactions.Length; i++)
            {
                ReflexionKeys [i] = Stage.Start ( Reactions [i] );
            }

            main.Aquire (this);
        }

        public guard ( reaction [] _reflexions, chain thought )
        {
            main = thought;
            Reactions = _reflexions;
            ReflexionKeys = new int [Reactions.Length];
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            for (int i = 0; i < ReflexionKeys.Length; i++)
            Stage.Stop ( ReflexionKeys [i] ) ;
            return true;
        }

        protected override void OnFree()
        {
            for (int i = 0; i < ReflexionKeys.Length; i++)
            Stage.Stop ( ReflexionKeys [i] );

            main.Free (this);
        }
    }
}