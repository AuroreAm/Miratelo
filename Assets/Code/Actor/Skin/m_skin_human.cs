using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_hand : module
    {
        public Transform [] Hand;
    }

    public class msp_bow : m_skin_procedural
    {
        [Depend]
        m_bow_user mbu;
        [Depend]
        m_skin ms;

        public Transform bow => mbu.Weapon.transform;

        public bool FollowTargetRotation;

        // transform cache
        public Transform Spine;
        public Vector3 TargetRotation; // x - y euleur target rotation

        public override void Create ()
        {
            Spine = ms.Ani.GetBoneTransform ( HumanBodyBones.Spine );
        }

        public override void Main()
        {
            TargetRotation.z = bow.eulerAngles.z;

            Quaternion Diff = Quaternion.Euler(TargetRotation) * Quaternion.Inverse(bow.rotation);
            
            if ( FollowTargetRotation )
            Spine.rotation = Diff * Spine.rotation;
        }
    }
}