using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class health_point : moon, pearl<damage>
    {
        public float HP
        {
            get { return _HP; }
            private set { _HP = Mathf.Clamp(value, 0, maxHP); }
        }

        public float maxHP { private set; get; }
        float _HP;

        public void set (float max)
        {
            maxHP = max;
            HP = maxHP;
        }

        public void _radiate(damage gleam)
        {
            HP -= gleam.raw;
            Debug.Log (HP);
        }
    }

    public struct damage
    {
        public float raw;

        public damage (float raw)
        {
            this.raw = raw;
        }
    }
}
