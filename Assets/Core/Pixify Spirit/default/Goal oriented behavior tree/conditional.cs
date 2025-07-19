using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class abstraction : thought.final
    {
        [Depend]
        s_mind sm;

        condition [] conditions;
        thought main;

        protected override void OnAquire()
        {
            Rethought ();
        }

        void Rethought ()
        {
            for (int i = 0; i < conditions.Length; i++)
            {

                if ( ! conditions [i].Check () )
                {
                    if ( sm.ThoughtExists ( conditions [i].solution ) )
                    {
                        sm.GetThought ( conditions [i].solution ).Aquire (this);
                        // NOTE: sometimes this might aquire its own parent, then there's error, I have to make sure this doesn't happen when building the behavior tree
                        return;
                    }
                    else
                    {
                        Finish ();
                        return;
                    }
                }
                Aquire (main);
            }
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            if (guest != main)
            {
                Rethought ();
                return false;
            }
            else
            return true;
        }
    }
}