using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_aim : reflexion
    {
        [Depend]
        s_equip se;

        [Depend]
        pc_aim pa;

        protected override void Step()
        {
            if ( !pa.on && Player.Aim.Active && se.weaponUser is s_bow_user )
                Stage.Start ( pa );
        }
    }

    public class pc_aim : action, IMotorHandler
    {
        [Depend]
        s_bow_user sbu;
        [Depend]
        character c;

        [Depend]
        s_motor sm;

        [Depend]
        pc_lateral_move plm;
        [Depend]
        ac_aim ca;
        int key_lm;

        public void OnMotorEnd(motor m)
        {
            SelfStop ();
        }

        protected override void Start()
        {
            sm.SetSecondState ( ca, this );

            if (!ca.on)
            SelfStop ();
            
            key_lm = Stage.Start (plm);
        }

        protected override void Step()
        {
            Vector3 RotDirection = Vecteur.RotDirection ( sbu.Weapon.BowString.position, s_camera.o.PointScreenCenter( c.gameObject.transform ) );
            ca.Aim ( RotDirection );

            if (Player.Action2.OnActive)
                ca.StartShoot ();

            if (Player.Aim.OnRelease)
            SelfStop ();
        }

        protected override void Stop()
        {
            Stage.Stop ( key_lm );

            if (sm.secondState == ca)
            sm.EndSecondState (this);
        }
    }
}
