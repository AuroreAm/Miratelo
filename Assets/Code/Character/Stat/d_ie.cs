using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // Imediate energy of a character
    public class d_ie : pix
    {
        public float IE
        {
            get { return _IE; }
            set { _IE = Mathf.Clamp(value, 0, MaxIE); }
        }

        public void Set (float Max)
        {
            MaxIE = Max;
            IE = MaxIE;
        }

        public float MaxIE { private set; get; }
        float _IE;
    }

    // typical character imediate energy generator
    public class s_ie_metabolism : s_stat_generator
    {
        [Depend]
        d_ie die;

        public float IEPerSecond;
        public float DelayTime;

        bool CanGenerate;
        float delay;
        float previousIE;

        public override void Create()
        {
            Stage.Start1 (this);
        }

        protected override void Step()
        {
            if (!CanGenerate)
            {
                if (delay >= DelayTime)
                {
                    CanGenerate = true;
                    delay = 0;
                }
                else
                    delay += Time.deltaTime;
            }

            if (CanGenerate && die.IE < die.MaxIE)
            die.IE += IEPerSecond * Time.deltaTime;

            // chech if IE was decreased
            if ( previousIE > die.IE )
            CanGenerate = false;

            previousIE = die.IE;
        }
    }
}
