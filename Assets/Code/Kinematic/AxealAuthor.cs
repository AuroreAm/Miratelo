using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    public class AxealAuthor : SkinAuthorModule {
        
        public float Height;
        public float Radius;
        public float Mass;

        public Transform[] Hand;

        public override void _create ()
        {
            new axeal.ink (Mass);
            new capsule.ink (Height, Radius);

            if ( Hand.Length > 0 )
            new hands.ink ( Hand );
        }

        #if UNITY_EDITOR
        public void OnDrawGizmosSelected(  )
        {
            Transform t = transform;
            // draw the character dimension capsule
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position + Radius / 2 * Vector3.up, Radius);
            Gizmos.DrawWireSphere(t.position + ((Height - Radius / 2) * Vector3.up), Radius);
            Gizmos.DrawLine(t.position + Radius / 2 * Vector3.up + (Radius * Vector3.left), t.position + ((Height - Radius / 2) * Vector3.up) + (Radius * Vector3.left));
            Gizmos.DrawLine(t.position + Radius / 2 * Vector3.up + (Radius * Vector3.right), t.position + ((Height - Radius / 2) * Vector3.up) + (Radius * Vector3.right));
            Gizmos.DrawLine(t.position + Radius / 2 * Vector3.up + (Radius * Vector3.forward), t.position + ((Height - Radius / 2) * Vector3.up) + (Radius * Vector3.forward));
            Gizmos.DrawLine(t.position + Radius / 2 * Vector3.up + (Radius * Vector3.back), t.position + ((Height - Radius / 2) * Vector3.up) + (Radius * Vector3.back));
        }

        [ContextMenu("Add Hand transform")]
        void AddHandTransform()
        {
            Animator Ani = GetComponent<Animator>();
            GameObject A = new GameObject("LH"); A.transform.SetParent(Ani.GetBoneTransform(HumanBodyBones.LeftHand));
            A.transform.localPosition = Vector3.zero;
            GameObject B = new GameObject("RH"); B.transform.SetParent(Ani.GetBoneTransform(HumanBodyBones.RightHand));
            B.transform.localPosition = Vector3.zero;
            Hand = new Transform[2] { A.transform, B.transform };
        }
        #endif
    }
}