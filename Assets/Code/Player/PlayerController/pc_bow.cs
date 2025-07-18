using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    //INPROGRESS
    /*
    public class pr_aim : reflection
    {
        [Depend]
        m_equip me;

        [Depend]
        pc_aim pa;

        action AimMovement;

        public override void Create()
        {
            TreeStart ( me.character );
            new parallel () {StopWhenFirstNodeStopped = true};
                new ac_aiming ();
                new ac_have_target ();
                new pc_lateral_move ();
            end();
            AimMovement = TreeFinalize ();
        }

        public override void Main()
        {
            if ( me.weaponUser is m_bow_user && mm.priority < Pri.def2nd && mm.secondState == pa )
            mm.SetState ( AimMovement, Pri.def2nd, true );

            if ( me.weaponUser is m_bow_user && Player.Aim.Active &&  mm.acceptSecondState && mm.secondState != pa )
            {
                mm.SetSecondState ( pa, Pri.SubAction );
            }
        }
    }*/
/*

    public class pc_aim : action
    {
        [Depend]
        m_bow_user mbu;
        [Depend]
        ac_aim ca;

        protected override void BeginStep()
        {
            ca.Aquire (this);
        }

        protected override bool Step()
        {
            Vector3 RotDirection = Vecteur.RotDirection ( mbu.Weapon.BowString.position, s_camera.o.PointScreenCenter( mbu.character.transform ) );
            ca.Aim ( RotDirection );

            if (Player.Action2.OnActive)
                ca.StartShoot ();

            if (Player.Aim.OnRelease)
            return true;

            return false;
        }

        protected override void Stop()
        {
            ca.Free (this);
        }
    }*/
}
