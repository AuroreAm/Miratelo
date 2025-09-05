using System;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class AniExt : ScriptableObject, ISerializationCallbackReceiver
    {
        // SERIALIZED FIELDS
        [SerializeField]
        List<state> SerializedStates;
        public RuntimeAnimatorController Model;

        // RUNTIME FIELDS
        public Dictionary<term, state> states;

        /// <summary>
        /// true if layer in the index layer is not a sync layer
        /// </summary>
        public bool[] states_type;

        public static AniExt get (RuntimeAnimatorController model) => Resources.Load<AniExt>("AnimatorMetadata/" + model.name);

        [Serializable]
        public struct state
        {
            public term key;
            public float duration;
            public float[] evs;
        }

        public void OnBeforeSerialize()
        {
            SerializedStates = new List<state>();
            if (states != null)
                SerializedStates.AddRange(states.Values);
        }

        public void OnAfterDeserialize()
        {
            states = new Dictionary<term, state>();
            if (SerializedStates != null)
                foreach (var v in SerializedStates)
                {
                    states.Add(v.key, v);
                }
        }
    }
}
