using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public abstract class d_weapon_standard : shard
    {
        [harmony]
        character c;
        public d_actor Owner { get; protected set; }
        public Transform Coord => c.form.transform;

        public void Aquire (d_actor Owner)
        {
            if (this.Owner) Debug.LogError ("Weapon already owned");
            this.Owner = Owner;
            c.form.layer = 0;
        }
    }
}