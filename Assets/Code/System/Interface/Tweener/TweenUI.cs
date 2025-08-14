using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    [PixiBase]
    public abstract class m_tween : pixi.self_managed
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
            this.TargetX = TargetX;
            this.speed = speed;

            if (!on)
            Stage.Start1 ( this );
        }

        public void StopTween ()
        {
            if (on)
            SelfStop ();
        }

        protected override void Step ()
        {
            SetX(Mathf.MoveTowards(GetX(), TargetX, speed * Time.unscaledDeltaTime));
            if (GetX() == TargetX)
            {
                SelfStop ();
                OnEnd?.Invoke();
            }
        }
    }
}