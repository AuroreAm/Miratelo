using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class gf_title : graphic_frame
    {
        static gf_title o;

        public override void Create()
        {
            o = this;
            Stage.Start1 (this);
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

        protected override void Step()
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

    [Category ("graphic frame")]
    public class g_show_title : action
    {
        public string Text;
        public term TitleId;

        protected override void Start()
        {
            gf_title.SetTitleText ( TitleId, Text );
            gf_title.ShowTitle ( TitleId );
            SelfStop ();
        }
    }

    public static class TriheroesTitle
    {
        public static readonly term MapTitle = new term ("MapTitle");
        public static readonly term EventTitle = new term ("EventTitle");
    }
}