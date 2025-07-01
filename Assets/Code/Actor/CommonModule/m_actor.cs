using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO: dependence order: need m_skin to be initialized first
    [Serializable]
    public class actor_writer : ModuleWriter
    {
        public List <Weapon> AttachedWeapon;
        public int Faction;

        public override void WriteModule(Character character)
        {
            var ma = character.RequireModule<m_actor>();
            ActorFaction.Register (ma, Faction);
            ma.faction = Faction;

            if (AttachedWeapon.Count > 0)
            {
                var me = character.RequireModule<m_equip>();
                for (int i = 0; i < AttachedWeapon.Count; i++)
                    me.inventory.RegisterWeapon( GameObject.Instantiate ( AttachedWeapon[i] ));
            }
        }
    }

    public class m_actor : module
    {
        [Depend]
        public m_skin ms;
        [Depend]
        public m_dimension md;

        public int faction;


        public m_actor target {get; private set;}

         // Character Locking this Character
        public List <m_actor> lockers = new List<m_actor>();
        public m_actor primaryLocker => (lockers.Count>0)?lockers[0]:null;

        public override void Create()
        {
            character.gameObject.layer = Vecteur.CHARACTER;
        }

        public void LockATarget ( m_actor actor )
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

        public m_actor GetNearestFacedFoe ( float distance )
        {
            List<m_actor> foe = ActorFaction.GetFoes(faction);
            foe.Sort( new SortDistanceA<m_actor>(ms.rotY.y, ms.Coord.position, distance) );

            if (foe.Count > 0 && Vector3.Distance(ms.Coord.position, foe[0].ms.Coord.position) < distance)
                return foe[0];

            return null;
        }
        
    }
}
