using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class gf_transition : pix
    {
        static gf_transition o;

        mt_linear_u Tween;
        public override void Create()
        {
            o = this;
            Tween = new mt_linear_u ( GetX, SetX, OnEnd);
        }

        float alpha = 1;
        RawImage image;

        public class package : PreBlock.Package <gf_transition>
        {
            public package ( RawImage image )
            {
                o.image = image;
                image.color = new Color ( 0,0,0, o.alpha );
            }
        }

        public static void FadeFromBlack()
        {
            o.Tween.Start (0, 0.2f);
        }

        public static void FadeToBlack()
        {
            o.Tween.Start (1, 0.2f);
            o.image.gameObject.SetActive (true);
        }

        float GetX ()
        {
            return alpha;
        }

        void SetX ( float value )
        {
            alpha = value;
            image.color = new Color(0, 0, 0, value);
        }

        void OnEnd()
        {
            if (alpha == 0)
                image.gameObject.SetActive (false);
        }
    }

    [Category ("graphic frame")]
    public class g_fade : action
    {
        [Export]
        public bool ToBlack;
        protected override void Start()
        {
            if (ToBlack)
                gf_transition.FadeToBlack();
            else
                gf_transition.FadeFromBlack();

            SelfStop ();
        }
    }
}
