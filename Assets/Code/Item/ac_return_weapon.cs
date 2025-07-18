using System;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// return a weapon in ms.r_arm
    /// </summary>
    public class ac_return_weapon : motor
    {
        public override int Priority => Pri.SubAction;

        [Depend]
        s_mind sm;

        [Depend]
        s_skin ss;
        [Depend]
        s_equip se;
        
        WeaponPlace to;

        term ReturnAnimation;

        public bool prepared => to != null;

        protected override void Start()
        {
            if (se.weaponUser == null)
            Debug.LogError("the character have no weapon to return");
            if (to == null)
            throw new InvalidOperationException ("No place to return, must set the place before doing this action");
            ss.PlayState ( ss.r_arm, ReturnAnimation, 0.1f, null, null, done );
        }

        public void SetPlaceToReturn ( WeaponPlace toPlace )
        {
            to = toPlace;
            ReturnAnimation = se.weaponUser.WeaponBase.DefaultReturnAnimation;
        }

        override protected void Stop()
        {
            Weapon w = se.weaponUser.WeaponBase;
            se.RemoveWeaponUser ();
            sm.TriggerThinking ();

            to.Put(w);
            to = null;
        }

        override protected void Abort()
        {
            to = null;
        }

        void done ()
        {
            SelfStop ();
        }
    }
}