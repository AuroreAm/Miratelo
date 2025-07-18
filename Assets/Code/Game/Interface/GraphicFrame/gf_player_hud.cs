using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixify;

namespace Triheroes.Code
{
    public class gf_player_hud : pix
    {
        public static gf_player_hud o;

        Image [] Heart;
        Image [] HeartContainer;
        mt_linear_u HPTween;

        RectTransform IECenter;
        Image[] IE;
        mt_linear_u IETween;
        mt_linear_u IETweenAlpha;

        public override void Create()
        {
            o = this;
            HPTween = new mt_linear_u ( GetHPX, SetHPX );
            IETween = new mt_linear_u( GetIEX, SetIEX );
            IETweenAlpha = new mt_linear_u ( GetIEAlpha, SetIEAlpha );
        }

        public class package : PreBlock.Package <gf_player_hud>
        {
            public package ( Transform Base )
            {
                // HP HUD
                Transform HPBase = Base.GetChild (0);
                int hearCount = HPBase.childCount;
                o.Heart = new Image [hearCount];
                o.HeartContainer = new Image [hearCount];
                for (int i = 0; i < hearCount; i++)
                {
                    o.Heart[i] = HPBase.GetChild(i).GetChild(0).GetComponent<Image> ();
                    o.HeartContainer[i] = HPBase.GetChild (i).GetComponent<Image> ();
                }

                // IE HUD
                Transform IEBase = Base.GetChild (1);
                o.IECenter = IEBase.GetChild (0).GetComponent<RectTransform> ();
                o.IE = new Image [o.IECenter.childCount];
                for (int i = 0; i < o.IECenter.childCount; i++)
                    o.IE[i] = o.IECenter.GetChild (i).GetComponent<Image> ();
            }
        }

        public void SetIdentity ( float MaxHP, float CurrentHP, float MaxIE, float CurrentIE, d_dimension refChar )
        {
            for (int i = 0; i < HeartContainer.Length; i++)
            HeartContainer [i].gameObject.SetActive ( i<MaxHP? true : false );
            HPTween.StopTween ();
            SetShownHP ( CurrentHP );

            IEReference = refChar;
            this.MaxIE = MaxIE;
            for (int i = 0; i < IE.Length; i++)
            IE[i].gameObject.SetActive ( i<MaxIE? true : false );
            ArrangeIEImage ( (int) Mathf.Ceil (MaxIE) );
            IETween.StopTween ();
            SetShownIE ( CurrentIE );
        }

        public void SetCurrentHP ( float CurrentHP )
        {
            if (CurrentHP != ShownHP)
            HPTween.Start ( CurrentHP, 3 );
        }

        float GetHPX () { return ShownHP; }

        void SetHPX ( float value )
        {
            SetShownHP ( value );
        }

        float ShownHP;
        void SetShownHP ( float HP )
        {
            ShownHP = HP;
            for (int i = 0; i < Heart.Length; i++)
            {
                float fill = HP - (float) i;
                if (fill > 1)
                    fill = 1;
                Heart[i].fillAmount = fill;
            }
        }

        public void SetCurrentIE ( float CurrentIE )
        {
            if (CurrentIE != ShownIE)
            IETween.Start ( CurrentIE, 3 );
        }
        
        float GetIEX () { return ShownIE; }

        void SetIEX ( float value )
        {
            SetShownIE ( value );
        }

        d_dimension IEReference;
        float MaxIE;
        float ShownIE;
        void SetShownIE ( float imediateEnergy )
        {
            ShownIE = imediateEnergy;
            // get raw screen space position next to the character
            Vector3 ppos = s_camera.o.Cam.WorldToViewportPoint ( IEReference.position );
            // move IE Center there
            IECenter.anchoredPosition = new Vector2 ( ppos.x * graphic_frame.Wd, ppos.y * graphic_frame.Hd ) + new Vector2 (-64,0);

            for (int i = 0; i < IE.Length; i++)
            {
                float a = imediateEnergy - (float) i;
                if ( a > 1 ) a = 1;
                IE [i].color = new Color (1,1,1,a);
            }

            // if IE is full, hide IE container
            if ( imediateEnergy >= MaxIE )
                IETweenAlpha.Start ( 0, 3 );
            else if ( IEAlpha == 0 )
                IETweenAlpha.Start ( 1, 3 );
        }

        float IEAlpha;
        float GetIEAlpha () { return IEAlpha; }
        void SetIEAlpha ( float value )
        {
            IEAlpha = value;
            for (int i = 0; i < IE.Length; i++)
            IE [i].color = new Color (1,1,1,IEAlpha);
        }

        void ArrangeIEImage ( int count )
        {
            for (int i = 0; i < count; i++)
            {
                float angle = i * Mathf.PI * 2 / count;
                float x = Mathf.Cos (angle + 45) * 8;
                float y = Mathf.Sin (angle + 45) * 8;

                IE[i].rectTransform.anchoredPosition = new Vector2 (x, y);
            }
        }

    }
}