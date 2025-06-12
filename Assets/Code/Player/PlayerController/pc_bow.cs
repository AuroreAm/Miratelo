using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    [Category("player controller")]
    [NodeDescription("transition to: aim")]
    public class t_aim : action
    {
        protected override bool Step()
        {
            if ( Player.GetButtonDown (BoutonId.Fire1) )
            selector.CurrentSelector.SwitchTo ( StateKey2.aim );

            return base.Step();
        }
    }

    [Unique]
    [Category("player controller")]
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

            if (Player.GetButtonDown (BoutonId.Fire3))
            return true;

            return false;
        }

        protected override void Stop()
        {
            ca.Free (this);
        }
    }
}
