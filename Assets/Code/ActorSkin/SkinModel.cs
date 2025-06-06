using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category ("Actor")]
    public class skin_writer : ModuleWriter
    {
        public SkinModel Model;

        public override void WriteModule(Character character)
        {
            character.RequireModule <m_dimension> ().Set ( Model.h, Model.r, Model.m );
            character.RequireModule <m_skin>().Set ( Model.gameObject, Model.AniExt, new Vector2 (Model.offsetRotationY, Model.offsetPositionY) );
            
            // additional modules for skin
            if (Model.CompatibleIk)
            character.RequireModule<m_skin_foot_ik>();

            if (Model.Hand != null && Model.Hand.Length >0 )
            character.RequireModule <m_hand> ().Hand = Model.Hand;

            if (Model.SwordPlace != null || Model.BowPlace != null || Model.ShieldPlace != null)
            character.RequireModule <m_equip> ().Set ( Model.SwordPlace, Model.ShieldPlace, Model.BowPlace );
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
        public Transform SwordPlace;
        public Transform BowPlace;
        public  Transform ShieldPlace;

        
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