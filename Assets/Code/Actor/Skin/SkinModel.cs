using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Serializable]
    public class skin_writer : ModuleWriter
    {
        public SkinModel Model;

        public override void WriteModule(Character character)
        {
            // Instantiate the model
            SkinModel model = GameObject.Instantiate(Model).GetComponent<SkinModel>();

            character.RequireModule <m_dimension> ().Set ( model.h, model.r, model.m );
            character.RequireModule <m_skin>().Set ( model.gameObject, model.AniExt, new Vector2 (model.offsetRotationY, model.offsetPositionY) );

            // element modules
            character.RequireModule <m_element> ();
            
            // additional modules for skin
            if (model.CompatibleIk)
            character.RequireModule<m_skin_foot_ik>();

            if (model.Hand != null && model.Hand.Length >0 )
            character.RequireModule <m_hand> ().Hand = model.Hand;

            if (model.SwordPlaces.Length > 0 || model.BowPlaces.Length > 0)
            character.RequireModule<m_inv_0>().SetPlaces(model.SwordPlaces, model.BowPlaces);

            character.RequireModule<m_element>().SetElement ( model.Element.Write () );

            // destroy the model
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

        // metadata
        public AniExt AniExt;
        public float offsetRotationY;
        public float offsetPositionY;

        public bool CompatibleIk;

        [Header("common transform")]
        public Transform[] Hand;
        public Transform[] SwordPlaces;
        public Transform[] BowPlaces;

        [Header("element")]
        public AtomPaper <element> Element;
        
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