using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Triheroes.Code.Axeal;
using System.Drawing;

namespace Triheroes.Code
{
    [inked]
    // IK foot manager for humanoids
    public class foot_ik : moon
    {
        [link]
        ground ground;
        [link]
        skin skin;

        SkinIk IK;
        Transform[] foots;
        Quaternion [] default_rotation;
        float base_offset;
        
        Vector3 forward => vecteur.ldir ( skin.roty, Vector3.forward );

        float get_ik_l () => IK.ani.GetFloat(sh.lik);
        float get_ik_r () => IK.ani.GetFloat(sh.rik);

        public enum foot { left, right }
        public foot dominand_foot { private set; get; }

        protected override void _ready ()
        {
            IK = skin.ani.gameObject.AddComponent<SkinIk>();
            IK._ik += _ik;
            IK._late_ik += set_ik_rot;

            foots = new Transform[] { IK.ani.GetBoneTransform(HumanBodyBones.LeftFoot), IK.ani.GetBoneTransform(HumanBodyBones.RightFoot) };
        }

        public class ink : ink <foot_ik> {
            public ink ( Quaternion [] _default_rotation, float _base_offset ) {
                o.default_rotation = _default_rotation;
                o.base_offset = _base_offset;
            }
        }

        Quaternion rotlik;
        Quaternion rotrik;

        void _ik ()
        {
            if (!IK.on && ground)
                IK.on = true;

            if (IK.on)
            {
                calculate_ik();
                check_can_continue_ik();
            }
            set_dominant_foot ();
        }

        void set_ik_rot ()
        {
            // foot rotation
            foots[0].rotation = Quaternion.Lerp(foots[0].rotation, rotlik, IK.iklx);
            foots[1].rotation = Quaternion.Lerp(foots[1].rotation, rotrik, IK.ikrx);
        }

        void set_dominant_foot ()
        {
            if (dominand_foot == foot.right && get_ik_l() > get_ik_r())
                dominand_foot = foot.left;
            if (dominand_foot == foot.left && get_ik_l() < get_ik_r())
                dominand_foot = foot.right;
        }

        void calculate_ik ()
        {
            // postion offset
            float PosYl = 0;
            float PosYr = 0;

            // postion of ik positions, used in OnAnimatorIK ();
            IK.ikl = foots[0].position;
            IK.ikr = foots[1].position;

            // rotation of ik, used in LateUpdate after animation
            rotlik = foots[0].rotation;
            rotrik = foots[1].rotation;

            // weight of ik from animation
            IK.iklx = get_ik_l();
            IK.ikrx = get_ik_r();

            // calculation // fix here
            if (Physics.Raycast(foots[0].position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 1, vecteur.Solid))
            {
                PosYl = base_offset + hit.point.y - skin.position.y;
                IK.ikl = new Vector3 (hit.point.x,hit.point.y + base_offset,hit.point.z);
                
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                rotlik = surfaceRotation * Quaternion.LookRotation(forward) * default_rotation[0];
            }

            if (Physics.Raycast(foots[1].position + Vector3.up * 0.5f, Vector3.down, out hit, 1, vecteur.Solid))
            {
                PosYr = base_offset + hit.point.y - skin.position.y;
                IK.ikr = new Vector3 (hit.point.x,hit.point.y + base_offset,hit.point.z);

                // Simpler rotation fix
                Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                rotrik = surfaceRotation * Quaternion.LookRotation(forward) * default_rotation[1];
            }

            // model offset postion to compensate ik
            skin.offset_posy = Mathf.Min (PosYl * IK.iklx + .1f, PosYr * IK.ikrx + .1f, 0);
        }

        void check_can_continue_ik ()
        {
            // if not on ground, disable Ik
            if (!ground)
            {
                skin.offset_posy = 0;
                IK.on = false;
            }
        }
    }
}
