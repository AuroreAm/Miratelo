using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_perfect_dash : reflexion, IElementListener<incomming_slash>
    {
        bool Ready;
        ac_slash attacker;

        [Depend]
        pc_perfect_dash pfd;

        [Depend]
        s_element se;
        
        [Depend]
        ac_dash ad;

        protected override void Start()
        {
            se.LinkMessage (this);
        }

        protected override void Stop()
        {
            se.UnlinkMessage (this);
        }

        protected override void Step()
        {
            if (Ready && attacker.on == false && !pfd.on)
            {
                if (ad.on)
                Stage.Start ( pfd );
                Reset ();
            }
        }
        
        public void OnMessage(incomming_slash context)
        {
            ReadyForPerfectDash ( ActorList.Get ( context.sender ).block.GetPix <ac_slash> () );
        }

        void ReadyForPerfectDash ( ac_slash _attacker )
        {
            Ready = true;
            attacker = _attacker;
        }

        void Reset ()
        {
            Ready = false;
            attacker = null;
        }
    }

        public class pc_perfect_dash : action
    {
        [Depend]
        bullet_time bt; int key_bt;

        [Depend]
        ac_dash ad;

        protected override void Start()
        {
            bt.Set (.4f);
            key_bt = Stage.Start ( bt );
        }

        protected override void Step()
        {
            if (!ad.on)
            SelfStop ();
        }

        protected override void Stop()
        {
            Stage.Stop ( key_bt );
        }
    }

}