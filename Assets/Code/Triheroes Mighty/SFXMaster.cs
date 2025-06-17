using Pixify;
using UnityEngine;
using UnityEngine.Audio;

namespace Triheroes.Code
{
    // sfx for UI and master for every SFX
    public class SFXMaster : module
    {
        public static SFXMaster o;
        public static AudioMixerGroup MainMixer;
        AudioSource AuUI;

        public override void Create()
        {
            o = this;
            SubResources <AudioMixer>.LoadAll ( "SE" );
            MainMixer = SubResources<AudioMixer>.q(new SuperKey("MainMixer")).FindMatchingGroups ("Master") [0];

            AuUI = character.gameObject.AddComponent<AudioSource>();
            AuUI.spatialBlend = 0;
        }

        public void PlayUISFX ( int name )
        {
            AuUI.PlayOneShot ( SubResources<AudioClip>.q ( name ) );
        }
    }

    // individual independent SFX
    public class s_sfx : ThingSystem<s_sfx.p_sfx>
    {
        protected override int InitialPieces => 5;
        public static s_sfx o;

        public s_sfx()
        { o = this; }

        public static void Play(int name, Vector3 pos)
        {
            o.pool.NextPiece().Set(SubResources<AudioClip>.q(name), pos);
            o.pool.GetPiece();
        }

        public class p_sfx : thing
        {
            AudioSource Au;

            public override void Create()
            {
                Au = new GameObject("SFX Channel").AddComponent<AudioSource>();
                Au.spatialBlend = 1;
                Au.outputAudioMixerGroup = SFXMaster.MainMixer;
            }

            public void Set ( AudioClip clip, Vector3 pos )
            {
                Au.clip = clip;
                Au.transform.position = pos;
            }

            public sealed override void BeginStep()
            {
                Au.Play ();
            }

            public override bool Main()
            {
                if (!Au.isPlaying)
                    return true;
                return false;
            }
        }
    }


}