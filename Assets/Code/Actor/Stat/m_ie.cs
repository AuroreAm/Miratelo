using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // Imediate energy of a character
    public class m_ie : module
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

    [RegisterAsBase]
    // typical character imediate energy generator
    public class m_ie_metabolism : m_stat_generator
    {
        [Depend]
        m_ie mie;

        public float IEPerSecond;
        public float DelayTime;

        bool CanGenerate;
        float delay;
        float previousIE;

        public override void Create()
        {
            Aquire (this);
        }

        public override void Main()
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

            if (CanGenerate && mie.IE < mie.MaxIE)
            mie.IE += IEPerSecond * Time.deltaTime;

            // chech if IE was decreased
            if ( previousIE > mie.IE )
            CanGenerate = false;

            previousIE = mie.IE;
        }
    }
}
