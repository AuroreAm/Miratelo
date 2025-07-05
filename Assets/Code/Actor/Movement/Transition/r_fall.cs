using System.Collections;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    // will tell the character to fall when on ground
    public class r_fall : reflection
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_gravity_mccc mgm;

        [Depend]
        ac_fall af;

        public override void Main()
        {
            if (!mgd.onGround && mgm.gravity < 0)
                mm.SetState (af);
        }
    }

    public class r_fall_with_hard : reflection
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_gravity_mccc mgm;

        [Depend]
        ac_fall af;

        [Depend]
        ac_fall_hard afh;

        float time;

        public override void Main()
        {
            if (!mgd.onGround && mgm.gravity < 0 && !(mm.state is ac_fall))
            {
                if (mm.SetState (af))
                time = 0;
            }
            else if (mm.state == af)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    mm.SetState (afh);
                    time = 0;
                }
            }
        }
    }
}