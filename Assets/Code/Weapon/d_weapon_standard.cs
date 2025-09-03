using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public abstract class d_weapon_standard : dat
    {
        [Link]
        character c;
        public d_actor Owner { get; protected set; }
        public Transform Coord => c.GameObject.transform;

        public void Aquire (d_actor Owner)
        {
            if (this.Owner) Debug.LogError ("Weapon already owned");
            this.Owner = Owner;
            c.GameObject.layer = 0;
        }
    }
}