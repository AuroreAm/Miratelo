using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Sword;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Slash", menuName = "RPG/SlashModel")]
    public class SlashAuthor : VirtusCreator
    {
        public Material TrailMaterial;
        public int FrameNumber = 1;

        public enum type { normal, hooker, hook_spammer, knocker }
        public type Type;

        protected override void _virtus_create()
        {
            new Sword.slash.ink ( TrailMaterial, FrameNumber );
            
            if ( Type == type.hooker )
            new ink <hooker> ();
            else if (Type == type.hook_spammer)
            new ink <hook_spammer> ();
            else if ( Type == type.knocker )
            new ink <knocker> ();
        }
    }
}
