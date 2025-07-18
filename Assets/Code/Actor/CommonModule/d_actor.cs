using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO: dependence order: need m_skin to be initialized first
    [Serializable]
    public class actor_writer : PixWriter
    {
        public int Faction;
        public List <Weapon> AttachedWeapon;

        public override void RequiredPix(in List<Type> a)
        {
            a.A < d_actor > ();

            if (AttachedWeapon.Count > 0)
            {
                a.A < s_equip > ();
            }
        }

        public override void OnWriteBlock()
        {
            new d_actor.package ( Faction );
        }

        public override void AfterWrite(block b)
        {
            if (AttachedWeapon.Count > 0)
            for (int i = 0; i < AttachedWeapon.Count; i++)
            b.GetPix <s_equip> ().inventory.RegisterWeapon( GameObject.Instantiate ( AttachedWeapon[i] ));
        }
    }

    public class d_actor : pix
    {
        [Depend]
        public character c;
        [Depend]
        public s_skin ss;
        [Depend]
        public d_dimension dd;

        // TODO faction reregister to ActorFaction
        public int faction {private set; get;}

        public class package : PreBlock.Package <d_actor>
        {
            public package (int faction)
            {
                o.faction = faction;
                ActorFaction.Register (o, faction);
            }
        }

        public d_actor target {get; private set;}

         // Character Locking this Character
        public List <d_actor> lockers = new List<d_actor>();
        public d_actor primaryLocker => (lockers.Count>0)?lockers[0]:null;

        public override void Create()
        {
            c.gameObject.layer = Vecteur.CHARACTER;
        }

        public void LockATarget ( d_actor actor )
        {
            if (actor == null)
            return;
            
            UnlockTarget ();
            target = actor;
            actor.lockers.Add ( this );
        }

        public void UnlockTarget()
        {
            if (target != null && target.lockers.Contains (this))
            target.lockers.Remove (this);
            target = null;
        }

        public d_actor GetNearestFacedFoe ( float distance )
        {
            List<d_actor> foe = ActorFaction.GetFoes(faction);
            foe.Sort( new SortDistanceA (ss.rotY.y, ss.Coord.position, distance) );

            if (foe.Count > 0 && Vector3.Distance(ss.Coord.position, foe[0].ss.Coord.position) < distance)
                return foe[0];

            return null;
        }
        
        public static implicit operator bool(d_actor exists)
        {
            if (exists != null)
            return exists.c.gameObject;
            else
            return false;
        }
    }
}
