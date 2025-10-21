using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class SlashSkinPathWriter : SkinWriterModule, ISerializationCallbackReceiver
    {
        [SerializeField]
        public List <slay.path> SerializedPaths;

        public Dictionary <term, slay.path> Paths = new Dictionary<term, slay.path> ();

        protected override void _create()
        {
            new ink < slay.skin_path >().o.paths = Paths;
        }

        public void OnBeforeSerialize()
        {
            SerializedPaths = new List<slay.path> ();
            if (Paths != null)
                SerializedPaths.AddRange(Paths.Values);
        }

        public void OnAfterDeserialize()
        {
            Paths = new Dictionary<term, slay.path> ();
            if (SerializedPaths != null)
                foreach (var v in SerializedPaths)
                {
                    Paths.Add(v.key, v);
                }
        }

    }
}
