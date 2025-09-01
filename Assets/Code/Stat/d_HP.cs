using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // core health point of a character
    public class d_HP : pix, IElementListener <Damage>
    {
        public override void Create()
        {
            base.Create();
        }

        public float HP
        {
            get { return _HP; }
            private set { _HP = Mathf.Clamp(value, 0, MaxHP); }
        }

        public void Set (float Max)
        {
            MaxHP = Max;
            HP = MaxHP;
        }

        public void OnMessage(Damage context)
        {
            HP -= context.raw;
            Debug.Log (HP);
        }

        public float MaxHP { private set; get; }
        float _HP;
    }

    public struct Damage
    {
        public float raw;

        public Damage (float raw)
        {
            this.raw = raw;
        }
    }
}