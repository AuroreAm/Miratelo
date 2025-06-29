using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // will set as idle if the character has nothing to do
    public class r_idle : reflection
    {
        [Depend]
        ac_idle ai;

        public override void Main()
        {
            if (mst.state == null)
                mst.SetState (ai, Pri.def);
        }
    }

    public class ac_idle : action
    {
        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_gravity_mccc mgm;
        [Depend]
        m_skin ms;

        protected override void BeginStep()
        {
            ms.PlayState (0, AnimationKey.idle,0.1f);
            mccc.Aquire (this);
            mgm.Aquire (this);
        }

        protected override void Stop()
        {
            mccc.Free (this);
            mgm.Free (this);
        }
    }
}
