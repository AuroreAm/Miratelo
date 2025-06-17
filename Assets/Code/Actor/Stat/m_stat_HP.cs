using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_stat_HP : module
    {
        [Depend]
        m_element me;

        public override void Create()
        {
            me.RegisterOnClash (OnClash);
        }

        void OnClash ( ClashEvent e )
        {
            HP -= e.force.raw;
            Debug.Log (HP);
        }

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
