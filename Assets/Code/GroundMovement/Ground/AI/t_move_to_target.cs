using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class t_move_to_target : thought.final
    {
        [Depend]
        d_actor da;
        float StopDistance;

        public bool CloseEnoughToTarget ()
        {
            if (!da.target) return false;
            return Vector3.Distance ( da.dd.position, da.target.dd.position ) < StopDistance + da.dd.r + da.target.dd.r;
        }

        [Category ("movement")]
        public class move_to_target : package.o <t_move_to_target>
        {
            [Export]
            public float StopDistance;

            protected override void BeforeStart()
            {
                main.StopDistance = StopDistance;
            }
        }
    }
}