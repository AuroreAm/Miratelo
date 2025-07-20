using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class conditional : thought.final
    {
        [Depend]
        s_mind sm;

        condition [] conditions;
        chain endgoal;
        chain main;

        public conditional ( condition [] _conditions, chain _thought )
        {
            conditions = _conditions;
            endgoal = _thought;
        }

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
                        main = sm.GetThought ( conditions [i].solution );
                        main.Aquire (this);
                        // NOTE: sometimes this might aquire its own parent, then there's error, I have to make sure this doesn't happen when building the behavior tree
                        return;
                    }
                    else
                    {
                        Finish ();
                        return;
                    }
                }
                main = endgoal;
                endgoal.Aquire (this);
            }
        }

        protected override void OnFree()
        {
            main.Free (this);
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            if (guest != endgoal)
            {
                Rethought ();
                return false;
            }
            else
            return true;
        }
    }
}