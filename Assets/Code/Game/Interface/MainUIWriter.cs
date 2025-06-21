using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class MainUIWriter : Scripter
    {
        public RawImage Overlay;

        public Text InteractText;
        public Transform PlayerHUD;

        public Animation TitleAnimation;
        public Text TitleText;

        public Animation EventTitleAnimation;
        public Text EventTitleText;
        override public void OnWrite(Character c)
        {
            // OVERLAY
            c.RequireModule <gf_transition> ().Set (Overlay);

            // MAP TITLE
            gf_title title = c.RequireModule<gf_title> ();
            title.SetANewTitleAnimationGameObject ( TriheroesTitle.MapTitle, TitleAnimation );
            title.SetANewTitleTextGameObject ( TriheroesTitle.MapTitle, TitleText );
            // EVENT TITLE
            title.SetANewTitleAnimationGameObject ( TriheroesTitle.EventTitle, EventTitleAnimation );
            title.SetANewTitleTextGameObject ( TriheroesTitle.EventTitle, EventTitleText );

            // INTERACT TEXT
            c.RequireModule<m_interact> ().Set (InteractText);

            // PLAYER HUD
            c.RequireModule<gf_player_hud> ().Set ( PlayerHUD );
        }
    }
}