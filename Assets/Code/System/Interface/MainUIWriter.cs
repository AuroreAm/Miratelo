using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class MainUIWriter : Writer
    {
        public RawImage Overlay;

        public Text InteractText;
        public Transform PlayerHUD;

        public Animation TitleAnimation;
        public Text TitleText;

        public Animation EventTitleAnimation;
        public Text EventTitleText;

        public Text Debug;

        public override void RequiredPix( in List<Type> a )
        {
            a.A <gf_transition> ();
            a.A <gf_title> ();
            a.A <gf_interact> ();
            a.A <gf_player_hud> ();
            a.A <gf_debug> ();
        }

        public override void OnWriteBlock()
        {
            new gf_transition.package ( Overlay );
            new gf_interact.package (InteractText);
            new gf_player_hud.package ( PlayerHUD );
            new gf_debug.package ( Debug );
        }

        public override void AfterWrite(block b)
        {
            // MAP TITLE
            gf_title title = b.GetPix<gf_title> ();
            title.SetANewTitleAnimationGameObject ( TriheroesTitle.MapTitle, TitleAnimation );
            title.SetANewTitleTextGameObject ( TriheroesTitle.MapTitle, TitleText );

            // EVENT TITLE
            title.SetANewTitleAnimationGameObject ( TriheroesTitle.EventTitle, EventTitleAnimation );
            title.SetANewTitleTextGameObject ( TriheroesTitle.EventTitle, EventTitleText );
        }

    }
}