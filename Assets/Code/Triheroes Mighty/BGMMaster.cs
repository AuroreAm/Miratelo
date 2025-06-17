using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class BGMMaster : module
    {
        static BGMMaster o;
        AudioSource Main;

        public override void Create ()
        {
            o = this;
            Main = character.gameObject.AddComponent <AudioSource> ();
            Main.spatialBlend = 0;
            Main.loop = true;
        }

        public static void PlayBGM (int name)
        {
            o.Main.Stop ();
            o.Main.clip = SubResources <AudioClip>.q (name);
            o.Main.Play ();
        }
    }
}