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
                mst.SetState (af,Pri.Action);
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
            if (!mgd.onGround && mgm.gravity < 0 && !(mst.state is ac_fall))
            {
                if (mst.SetState (af,Pri.Action,true))
                time = 0;
            }
            else if (mst.state == af)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    mst.SetState (afh,Pri.Action2nd);
                    time = 0;
                }
            }
        }
    }
}