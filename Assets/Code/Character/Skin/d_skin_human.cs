using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_hand : pix
    {
        public Transform [] Hand {private set; get;}

        public class package : PreBlock.Package <d_hand>
        {
            public package ( Transform [] hand  )
            {
                o.Hand = hand;
            }
        }

    }

    public class sp_bow : s_skin_procedural
    {
        [Depend]
        s_bow_user sbu;
        [Depend]
        s_skin ss;

        public Transform bow => sbu.Weapon.transform;

        public bool FollowTargetRotation;

        // transform cache
        Transform Spine;
        public Vector3 TargetRotation; // x - y euleur target rotation

        public override void Create ()
        {
            Spine = ss.Ani.GetBoneTransform ( HumanBodyBones.Spine );
        }

        protected override void Step()
        {
            TargetRotation.z = bow.eulerAngles.z;

            Quaternion Diff = Quaternion.Euler(TargetRotation) * Quaternion.Inverse(bow.rotation);
            
            if ( FollowTargetRotation )
            Spine.rotation = Diff * Spine.rotation;
        }
    }
}