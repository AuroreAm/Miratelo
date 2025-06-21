using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// transition to: aim
    /// </summary>
    [Unique]
    public class t_aim : action
    {
        protected override bool Step()
        {
            if ( Player.Aim.OnActive )
            selector.CurrentSelector.SwitchTo ( StateKey2.aim );

            return base.Step();
        }
    }

    [Unique]
    public class pc_aim : action
    {
        [Depend]
        m_bow_user mbu;
        [Depend]
        c_aim ca;

        protected override void BeginStep()
        {
            ca.Aquire (this);
        }

        protected override bool Step()
        {
            Vector3 RotDirection = Vecteur.RotDirection ( mbu.Weapon.BowString.position, m_camera.o.PointScreenCenter( mbu.character.transform ) );
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
    }
}
