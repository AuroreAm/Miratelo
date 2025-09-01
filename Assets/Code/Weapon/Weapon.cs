using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class Weapon : MonoBehaviour
    {
        public string Name;
        public abstract WeaponType Type { get; }
        public abstract term DefaultDrawAnimation { get; }
        public abstract term DefaultReturnAnimation { get; }

        public d_actor Owner { get; protected set; }
        

        void Awake ()
        {
            gameObject.layer = Vecteur.TRIGGER;
            new pi_weapon (this, gameObject.GetInstanceID ());
        }

        public void Aquire (d_actor Owner)
        {
            if (this.Owner) Debug.LogError ("Weapon already owned");
            this.Owner = Owner;
            gameObject.layer = 0;
        }

        public void Release ()
        {
            gameObject.layer = Vecteur.TRIGGER;
        }

    }
    
    public enum WeaponType {Sword, Bow};

    public class pi_weapon : p_interactable
    {
        public pi_weapon(Weapon weapon, int InstanceId)
        {
            this.weapon = weapon;
            Register (InstanceId);
        }

        public Weapon weapon;
    }
}