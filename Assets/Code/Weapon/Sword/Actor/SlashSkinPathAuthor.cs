using Lyra;
using System.Collections.Generic;
using UnityEngine;
using static Triheroes.Code.Sword.Combat.slash;

namespace Triheroes.Code
{
    public class SlashSkinPathAuthor : SkinAuthorModule, ISerializationCallbackReceiver
    {
        [SerializeField]
        public List <path> SerializedPaths;

        public Dictionary <term, path> Paths = new Dictionary<term, path> ();

        public override void _creation()
        {
            new ink < skin_path >().o.paths = Paths;
        }

        public void OnBeforeSerialize()
        {
            SerializedPaths = new List<path> ();
            if (Paths != null)
                SerializedPaths.AddRange(Paths.Values);
        }

        public void OnAfterDeserialize()
        {
            Paths = new Dictionary<term, path> ();
            if (SerializedPaths != null)
                foreach (var v in SerializedPaths)
                {
                    Paths.Add(v.key, v);
                }
        }

    }
}
