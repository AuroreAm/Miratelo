using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    [CoreBase]
    public abstract class m_tween : core
    {
    }

    // tween linear unscaled delta time
    public class mt_linear_u : m_tween
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

        public mt_linear_u (Func<float> GetX, Action<float> SetX, Action OnEnd = null)
        {
            this.GetX = GetX;
            this.SetX = SetX;
            this.OnEnd = OnEnd;
        }

        public void CacheAction( Func<float> GetX, Action<float> SetX, Action OnEnd = null )
        {
            this.GetX = GetX;
            this.SetX = SetX;
            this.OnEnd = OnEnd;
        }

        /// <summary>
        /// start the tween using the cached action
        /// </summary>
        public void Start ( float TargetX, float speed)
        {
            enabled = true;
            this.TargetX = TargetX;
            this.speed = speed;
        }

        public void Stop ()
        {
            enabled = false;
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