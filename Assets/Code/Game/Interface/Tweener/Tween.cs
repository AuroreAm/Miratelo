using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    public abstract class m_tween : core
    {
    }

    [RegisterAsBase]
    public class mt_linear : m_tween
    {
        Func<float> GetX;
        Action<float> SetX;
        float TargetX;
        float speed;
        Action OnEnd;

        protected override void OnAquire()
        {
            enabled = false;
        }

        public void Start(float TargetX, float speed, Func<float> GetX, Action<float> SetX, Action OnEnd = null)
        {
            enabled = true;
            this.TargetX = TargetX;
            this.speed = speed;
            this.GetX = GetX;
            this.SetX = SetX;
            this.OnEnd = OnEnd;
        }

        public override void Main()
        {
            SetX(Mathf.MoveTowards(GetX(), TargetX, speed * Time.unscaledDeltaTime));
            if (GetX() == TargetX)
            {
                enabled = false;
                OnEnd?.Invoke();
            }
        }
    }
}