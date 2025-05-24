using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    /// <summary>
    /// Animation Controller metadata
    /// </summary>
    [CreateAssetMenu(menuName = "Triheroes/Animation Metadata")]
    public class AniExt : ScriptableObject, ISerializationCallbackReceiver
    {
        
        public RuntimeAnimatorController Model;

        // SERIALIZED FIELDS
        [SerializeField]
        List<State> SerializedStates;
        [SerializeField]
        List<StateExt> SerializedExtStates;

        public Dictionary<SuperKey, State> States;
        public Dictionary<SuperKey, StateExt> StatesExt;


        /// <summary>
        /// true if layer in the index layer is not a sync layer
        /// </summary>
        public bool[] RealLayer;

        [Serializable]
        public struct State
        {
            public SuperKey Key;
            public float Duration;
            public float[] EvPoint;
        }

        [Serializable]
        public struct StateExt
        {
            public SuperKey Key;
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
            States = new Dictionary<SuperKey, State>();
            if (SerializedStates != null)
                foreach (var v in SerializedStates)
                {
                    States.Add(v.Key, v);
                }

            StatesExt = new Dictionary<SuperKey, StateExt>();
            if (SerializedExtStates != null)
                foreach (var v in SerializedExtStates)
                {
                    StatesExt.Add(v.Key, v);
                }
        }
    }
}
