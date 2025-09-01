using System;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    /// <summary>
    /// Animation Controller metadata
    /// </summary>
    [CreateAssetMenu(menuName = "Triheroes/Animation Metadata")]
    public class AniExt : ScriptableObject, ISerializationCallbackReceiver
    {
        // SERIALIZED FIELDS
        [SerializeField]
        List<State> SerializedStates;
        [SerializeField]
        List<StateExt> SerializedExtStates;
        public RuntimeAnimatorController Model;

        // RUNTIME FIELDS
        public Dictionary<term, State> States;
        public Dictionary<term, StateExt> StatesExt;

        /// <summary>
        /// true if layer in the index layer is not a sync layer
        /// </summary>
        public bool[] RealLayer;

        public static AniExt Get ( RuntimeAnimatorController model ) => Resources.Load<AniExt>("AnimatorMetadata/" + model.name);

        [Serializable]
        public struct State
        {
            public term Key;
            public float Duration;
            public float[] EvPoint;
        }

        [Serializable]
        public struct StateExt
        {
            public term Key;
            public float f;
            public Vector3 v3;
            public Quaternion q;
        }

        public void OnBeforeSerialize()
        {
            SerializedStates = new List<State>();
            if (States != null)
                SerializedStates.AddRange(States.Values);

            SerializedExtStates = new List<StateExt>();
            if (StatesExt != null)
                SerializedExtStates.AddRange(StatesExt.Values);
        }

        public void OnAfterDeserialize()
        {
            States = new Dictionary<term, State>();
            if (SerializedStates != null)
                foreach (var v in SerializedStates)
                {
                    States.Add(v.Key, v);
                }

            StatesExt = new Dictionary<term, StateExt>();
            if (SerializedExtStates != null)
                foreach (var v in SerializedExtStates)
                {
                    StatesExt.Add(v.Key, v);
                }
        }
    }
}
