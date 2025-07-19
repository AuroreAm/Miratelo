using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class guard : thought
    {
        reflexion [] Reflexions;
        int [] ReflexionKeys;
        thought main;

        protected override void OnAquire()
        {
            for (int i = 0; i < Reflexions.Length; i++)
            {
                ReflexionKeys [i] = Stage.Start ( Reflexions [i] );
            }

            main.Aquire (this);
        }

        public guard ( reflexion [] _reflexions, thought thought )
        {
            main = thought;
            Reflexions = _reflexions;
            ReflexionKeys = new int [Reflexions.Length];
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            for (int i = 0; i < ReflexionKeys.Length; i++)
            Stage.Stop ( ReflexionKeys [i] ) ;
            return true;
        }
    }
}