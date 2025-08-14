using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class guard : thought.chain
    {
        reflexion [] Reflexions;
        int [] ReflexionKeys;
        chain main;

        protected override void OnAquire()
        {
            for (int i = 0; i < Reflexions.Length; i++)
            {
                ReflexionKeys [i] = Stage.Start1 ( Reflexions [i] );
            }

            main.Aquire (this);
        }

        public guard ( reflexion [] _reflexions, chain thought )
        {
            main = thought;
            Reflexions = _reflexions;
            ReflexionKeys = new int [Reflexions.Length];
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            for (int i = 0; i < ReflexionKeys.Length; i++)
            Stage.Stop1 ( ReflexionKeys [i] ) ;
            return true;
        }

        protected override void OnFree()
        {
            for (int i = 0; i < ReflexionKeys.Length; i++)
            Stage.Stop1 ( ReflexionKeys [i] );

            main.Free (this);
        }
    }
}