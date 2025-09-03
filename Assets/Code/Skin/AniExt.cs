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
        List<State> SerializedStates;
        public RuntimeAnimatorController Model;

        // RUNTIME FIELDS
        public Dictionary<term, State> States;

        /// <summary>
        /// true if layer in the index layer is not a sync layer
        /// </summary>
        public bool[] RealLayer;

        public static AniExt Get(RuntimeAnimatorController model) => Resources.Load<AniExt>("AnimatorMetadata/" + model.name);

        [Serializable]
        public struct State
        {
            public term Key;
            public float Duration;
            public float[] EvPoint;
        }

        public void OnBeforeSerialize()
        {
            SerializedStates = new List<State>();
            if (States != null)
                SerializedStates.AddRange(States.Values);
        }

        public void OnAfterDeserialize()
        {
            States = new Dictionary<term, State>();
            if (SerializedStates != null)
                foreach (var v in SerializedStates)
                {
                    States.Add(v.Key, v);
                }
        }
    }
}
