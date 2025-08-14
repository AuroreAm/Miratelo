using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinModel : MonoBehaviour
    {
        [Header("Character dimension")]
        [Tooltip("height")]
        public float h;
        [Tooltip("radius")]
        public float r;
        [Tooltip("mass")]
        public float m;

        public float offsetRotationY;
        public float offsetPositionY;

        public bool CompatibleIk;

        [Header("common transform")]
        public Transform[] Hand;
        public Transform[] SwordPlaces;
        public Transform[] BowPlaces;

        [Header("element")]
        public PixPaper <element> Element;
        
        #if UNITY_EDITOR
        public void OnDrawGizmosSelected(  )
        {
            Transform t = transform;
            // draw the character dimension capsule
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position + r / 2 * Vector3.up, r);
            Gizmos.DrawWireSphere(t.position + ((h - r / 2) * Vector3.up), r);
            Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.left), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.left));
            Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.right), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.right));
            Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.forward), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.forward));
            Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.back), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.back));
        }

        #region editortool
            [ContextMenu("Add Hand transform")]
            void AddHandHandler()
            {
                Animator Ani = GetComponent<Animator>();
                GameObject A = new GameObject("LH"); A.transform.SetParent(Ani.GetBoneTransform(HumanBodyBones.LeftHand));
                A.transform.localPosition = Vector3.zero;
                GameObject B = new GameObject("RH"); B.transform.SetParent(Ani.GetBoneTransform(HumanBodyBones.RightHand));
                B.transform.localPosition = Vector3.zero;
                Hand = new Transform[2] { A.transform, B.transform };
            }
        #endregion

        #endif
    }
}