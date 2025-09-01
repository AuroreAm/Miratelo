using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorWriter : Writer
    {
        public int Faction;
        public string ActorName;
        public List <Weapon> AttachedWeapon;

        public override void RequiredPix(in List<Type> a)
        {
            a.A < d_actor > ();

            if (AttachedWeapon.Count > 0)
                a.A < s_equip > ();
        }

        public override void OnWriteBlock()
        {
            new d_actor.package ( Faction, ActorName );
        }

        public override void AfterWrite(block b)
        {
            if (AttachedWeapon.Count > 0)
            for (int i = 0; i < AttachedWeapon.Count; i++)
            b.GetPix <s_equip> ().inventory.RegisterWeapon( GameObject.Instantiate ( AttachedWeapon[i] ));
        }
    }
}