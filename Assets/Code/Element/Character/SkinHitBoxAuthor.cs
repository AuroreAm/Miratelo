using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinHitBoxAuthor : SkinWriterModule {

        protected override void _create() {
            
            Collider [] HitBoxes = GetComponentsInChildren <Collider> ();

            foreach ( var h in HitBoxes ) {
                new hitbox ( h );
            }
        }
        
        #if UNITY_EDITOR
        [ContextMenu("clean ragdoll useless component")]
        void clean () {
            Joint [] joints = GetComponentsInChildren <Joint> ();

            foreach ( var r in joints )
            DestroyImmediate (r);

            Rigidbody [] rigidbodies = GetComponentsInChildren <Rigidbody> ();

            foreach ( var r in rigidbodies )
            DestroyImmediate (r);

            UnityEditor.EditorUtility.SetDirty ( gameObject );
        }
        #endif
    }
}