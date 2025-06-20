using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class gf_title : graphic_frame
    {
        static gf_title o;

        public override void Create()
        {
            o = this;
            Aquire (new Void ());
        }

        Dictionary<int, Text> Titles = new Dictionary<int, Text>();
        public void SetANewTitleTextGameObject ( int id, Text Component )
        {
            Titles.Add ( id, Component );
        }

        Dictionary<int, Animation> TitlesAnimations = new Dictionary<int, Animation>();
        public void SetANewTitleAnimationGameObject ( int id, Animation Component )
        {
            TitlesAnimations.Add ( id, Component );
            Component.gameObject.SetActive (false);
        }

        List <Animation> RunningAnimations = new List<Animation> ();
        public static void ShowTitle ( int id ) => o.ShowTitleInternal ( id );
        void ShowTitleInternal ( int id )
        {
            RunningAnimations.Add ( TitlesAnimations[id] );
            TitlesAnimations[id].gameObject.SetActive (true);
            TitlesAnimations[id].Play ();
        }

        public static void SetTitleText ( int id, string text )
        {
            o.Titles[id].text = text;
        }

        public override void Main()
        {
            for (int i = RunningAnimations.Count - 1; i >= 0 ; i--)
            {
                if (!RunningAnimations[i].isPlaying)
                {
                RunningAnimations[i].gameObject.SetActive (false);
                RunningAnimations.RemoveAt (i);
                }
            }
        }
    }

    public class g_show_title : action
    {
        public string Text;
        public SuperKey TitleId;

        protected override bool Step()
        {
            gf_title.SetTitleText ( TitleId, Text );
            gf_title.ShowTitle ( TitleId );
            return true;
        }
    }

    public static class TriheroesTitle
    {
        public static readonly SuperKey MapTitle = new SuperKey ("MapTitle");
        public static readonly SuperKey EventTitle = new SuperKey ("EventTitle");
    }
}