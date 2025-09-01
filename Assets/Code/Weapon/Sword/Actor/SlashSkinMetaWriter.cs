using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [RequireComponent(typeof(SkinModel))]
    [RequireComponent(typeof(Animator))]
    public class SlashSkinMetaWriter : Writer, ISerializationCallbackReceiver
    {
        [SerializeField]
        public List<SlashPath> SerializedPaths;

        public Dictionary <term, SlashPath> Paths = new Dictionary<term, SlashPath> ();

        public override void OnWriteBlock()
        {}

        public override void AfterWrite(block b)
        {
            b.GetPix <d_slash_skin_meta> ().Paths = Paths;
        }

        public override void RequiredPix(in List<Type> a)
        {
            a.A <d_slash_skin_meta> ();
        }

        public void OnBeforeSerialize()
        {
            SerializedPaths = new List<SlashPath> ();
            if (Paths != null)
                SerializedPaths.AddRange(Paths.Values);
        }

        public void OnAfterDeserialize()
        {
            Paths = new Dictionary<term, SlashPath> ();
            if (SerializedPaths != null)
                foreach (var v in SerializedPaths)
                {
                    Paths.Add(v.key, v);
                }
        }
    }
}