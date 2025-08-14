using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class s_sfx : pix
    {
        [Depend]
        character c;

        AudioSource Au;

        public override void Create()
        {
            Au = c.gameObject.AddComponent<AudioSource>();
            Au.outputAudioMixerGroup = SFXMaster.MainMixer;
            Au.spatialBlend = 1;
        }

        public void PlaySFX ( int name )
        {
            Au.PlayOneShot ( SubResources<AudioClip>.q ( name ) );
        }
    }
}