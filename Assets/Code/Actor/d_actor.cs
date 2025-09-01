using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_actor : pix
    {
        [Depend]
        public character c;
        [Depend]
        public s_skin ss;
        [Depend]
        public d_dimension dd;
        [Depend]
        public s_element se;

        public block block => b;

        // TODO faction reregister to ActorFaction
        public int faction {private set; get;}
        public string ActorName { private set; get; }

        public class package : PreBlock.Package <d_actor>
        {
            public package (int faction, string actorName)
            {
                o.faction = faction;
                o.ActorName = actorName ;
                o.term = new term (actorName);

                ActorList.Register (o, faction);
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
            List<d_actor> foe = ActorList.GetFoes(faction);
            foe.Sort( new SortDistanceA (ss.rotY, ss.Coord.position, distance) );

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
