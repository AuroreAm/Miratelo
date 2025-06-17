using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // core health point of a character
    public class m_stat_HP : module
    {
        public float HP
        {
            get { return _HP; }
            set { _HP = Mathf.Clamp(value, 0, MaxHP); }
        }

        public void Set (float Max)
        {
            MaxHP = Max;
            HP = MaxHP;
        }

        float MaxHP;
        float _HP;
    }
}
