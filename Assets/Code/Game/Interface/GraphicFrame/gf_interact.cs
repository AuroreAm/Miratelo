using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class gf_interact : pix
    {
        static gf_interact o;

        public class package : PreBlock.Package <gf_interact>
        {
            public package ( Text text )
            {
                o.text = text;
                text.color = new Color(1, 1, 1, 0);
            }
        }

        mt_linear_u Tween;
        public override void Create()
        {
            o = this;
            Tween = new mt_linear_u ( GetX, SetX );
            CacheAction ();
        }
        
        Text text;
        float alpha = 0;

        Func<float> _getX; Action<float> _setX;

        /// <summary>
        /// show the interact text, need to be called per frame the text needs to be shown
        /// </summary>
        public static void ShowInteractText ( string text )
        {
            o.text.text = text;
            o.Tween.Start (1, 2);
        }

        void CacheAction()
        {
            _getX = GetX;
            _setX = SetX;
        }

        float GetX ()
        {
            return alpha;
        }

        void SetX ( float value )
        {
            alpha = value;
            text.color = new Color(1, 1, 1, value);
            o.Tween.Start (0, 2);
        }
    }
}