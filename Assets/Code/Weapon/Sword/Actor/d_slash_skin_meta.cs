using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class d_slash_skin_meta : pix
    {
        public Dictionary <term, SlashPath> Paths = new Dictionary <term, SlashPath> ();
    }

    [System.Serializable]
    public struct SlashPath
    {
        public term key;
        // time interval between two points
        public const float Delta = 0.005f;
        public Vector3[] Orig;
        public Vector3[] Dir;
    }
}