using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Serializable]
    public class skin_writer : PixWriter
    {
        public SkinModel Model;
        SkinModel model;

        public override void RequiredPix( in List <Type> a)
        {
            a.A <s_skin> ();
            a.A <d_dimension> ();
            a.A <s_element> ();
            
            if (Model.CompatibleIk)
            a.A <s_skin_foot_ik> ();

            if (Model.Hand != null && Model.Hand.Length >0 )
            a.A <d_hand> ();

            if (Model.SwordPlaces.Length > 0 || Model.BowPlaces.Length > 0)
            a.A <s_inv_0> ();
        }

        public override void OnWriteBlock()
        {
            // Instantiate the model
            model = GameObject.Instantiate(Model).GetComponent<SkinModel>();

            new d_dimension.package ( model.h, model.r, model.m );
            new s_skin.package ( model.gameObject, new Vector2 (model.offsetRotationY, model.offsetPositionY) );

            if (model.Hand != null && model.Hand.Length >0 )
            new d_hand.package ( model.Hand );
            
            if (model.SwordPlaces.Length > 0 || model.BowPlaces.Length > 0)
            new s_inv_0.package (model.SwordPlaces, model.BowPlaces);
        }

        public override void AfterWrite(block b)
        {
            b.GetPix <s_element> ().SetElement ( model.Element.Write () );

            // destroy the model component
            ScriptableObject.Destroy (model);
        }
    }

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