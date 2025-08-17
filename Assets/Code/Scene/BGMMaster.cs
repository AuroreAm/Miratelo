using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class BGMMaster : pix
    {
        AudioSource Main;

        public override void Create ()
        {
            Main = Stage.o.gameObject.AddComponent <AudioSource> ();
            Main.spatialBlend = 0;
            Main.loop = true;
        }

        public void PlayBGM (int name)
        {
            Main.Stop ();
            Main.clip = SubResources <AudioClip>.q (name);
            Main.Play ();
        }
    }
}