using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_equip : module
    {
        public List <Weapon> weapons { private set; get; } = new List<Weapon> ();
        public int ptrWeapon { private set; get;} = -1;

        public m_weapon_user weaponUser;

        public Transform SwordPlace;
        public Transform ShieldPlace;
        public Transform BowPlace;

        [Depend]
        m_equip me;

        public void Set ( Transform SwordPlace, Transform ShieldPlace, Transform BowPlace )
        {
            this.SwordPlace = SwordPlace;
            this.ShieldPlace = ShieldPlace;
            this.BowPlace = BowPlace;
        }

        public void AttachWeapon ( Weapon weapon )
        {
            weapons.Add (weapon);

            ReAttachWeapon ( weapon );
        }

        public void ReAttachWeapon ( Weapon weapon )
        {
            if (weapon is Sword)
            weapon.transform.SetParent(SwordPlace.transform);
            else if (weapon is Bow)
            weapon.transform.SetParent(BowPlace.transform);

            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    public class ac_equip_adw : action
    {
        [Depend]
        m_equip me;
        public int index;

        public ac_equip_adw (int id)
        {
            index = id;
        }

        protected override bool Step()
        {
            me.character.GetUnique <ac_draw_weapon> ().Set ( me.weapons [index] );
            return true;
        }
    }

    public class ac_equip_arw : action
    {
        [Depend]
        m_equip me;

        protected override bool Step()
        {
            me.character.GetUnique <ac_return_weapon> ().Set ( me.weaponUser.WeaponBase );
            return true;
        }
    }
}
