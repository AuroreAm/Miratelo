using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class warrior : moon
    {
        [link]
        actor actor;

        [link]
        character c;

        [link]
        public skin skin;

        public class ink : ink <warrior>
        {
            public ink ( int faction )
            {
                o.faction = faction;
            }
        }
        
        public int faction {private set; get;}
        public warrior target {get; private set;}

         // Character Locking this Character
        public List <warrior> lockers = new List<warrior>();
        public warrior primary_locker => (lockers.Count>0)?lockers[0]:null;

        protected override void _ready()
        {
            pallas.register ( this, faction );
        }

        public void lock_target ( warrior actor )
        {
            if ( actor == null )
            return;
            
            unlock_target ();
            target = actor;
            actor.lockers.Add ( this );
        }

        public void unlock_target ()
        {
            if (target != null && target.lockers.Contains (this))
            target.lockers.Remove (this);
            target = null;
        }

        public warrior get_nearest_foe ( float distance )
        {
            List < warrior > foe = pallas.get_foes(faction);
            foe.Sort( new SortDistanceA (skin.roty, skin.position, distance) );

            if (foe.Count > 0 && Vector3.Distance(skin.position, foe[0].skin.position) < distance)
                return foe[0];

            return null;
        }

        public static implicit operator bool(warrior exists)
        {
            if (exists != null)
            return exists.c.gameobject;
            else
            return false;
        }

        public static explicit operator actor (warrior a) => a.actor;
    }

    [path ("test")]
    public class test_get_enemy : action
    {
        [link]
        warrior warrior;

        protected override void _start()
        {
            warrior.lock_target ( pallas.get_foes (warrior.faction) [0] );
            stop ();
        }
    }
}