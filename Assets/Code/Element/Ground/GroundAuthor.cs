using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class GroundAuthor : MonoBehaviour
    {
        public enum GroundType { wood, asphalt }
        public GroundType type;

        void Start()
        {
            ground_element Ground = null;
            switch (type)
            {
                case GroundType.wood:
                    Ground = new pg_wood ();
                    break;
                case GroundType.asphalt:
                    Ground = new pg_asphalt ();
                    break;
            }
            Ground.register ( gameObject.GetInstanceID () );

            Destroy (this);
        }
    }
}
