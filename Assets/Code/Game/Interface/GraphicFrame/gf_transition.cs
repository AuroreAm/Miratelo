using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    public class gf_transition : module
    {
        static gf_transition o;

        mt_linear_u Tween;
        public override void Create()
        {
            o = this;
            Tween = new mt_linear_u ( GetX, SetX, OnEnd);
            Tween.Aquire (this);
        }

        float alpha = 1;
        RawImage image;

        public void Set (RawImage image)
        {
            this.image = image;
            image.color = new Color(0, 0, 0, alpha);
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

    public class g_fade : action
    {
        public bool ToBlack;
        protected override bool Step()
        {
            if (ToBlack)
                gf_transition.FadeToBlack();
            else
                gf_transition.FadeFromBlack();

            return true;
        }
    }
}
