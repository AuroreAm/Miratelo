using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("actor")]
    public class t_move : action
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_capsule_character_controller mccc;

        protected override bool Step()
        {
            if (mgd.onGround && mccc.verticalVelocity < 0 && Vector3.Angle (Vector3.up, mgd.groundNormal) <= 45)
            selector.CurrentSelector.SwitchTo (StateKey2.move);
            return false;
        }
    }

    [Category("actor")]
    public class t_fall : action
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_capsule_character_controller mccc;

        protected override bool Step()
        {
            if (!mgd.onGround && mccc.verticalVelocity < 0)
            selector.CurrentSelector.SwitchTo (StateKey2.fall);
            return false;
        }
    }
}