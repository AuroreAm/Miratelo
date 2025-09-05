using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    // IK foot manager for humanoids
    public class foot_ik : moon
    {
        [link]
        ground ground;
        [link]
        skin skin;

        SkinIk IK;
        Transform[] Foot;
        float get_ik_l () => IK.ani.GetFloat(hash.lik);
        float get_ik_r () => IK.ani.GetFloat(hash.rik);

        public enum foot { left, right }
        public foot dominand_foot { private set; get; }

        protected override void _ready ()
        {
            IK = skin.ani.gameObject.AddComponent<SkinIk>();
            IK._ik += _ik;
            IK._late_ik += set_ik_rot;

            Foot = new Transform[] { IK.ani.GetBoneTransform(HumanBodyBones.LeftFoot), IK.ani.GetBoneTransform(HumanBodyBones.RightFoot) };
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
            // TODO: foot rotation are different from character to character, this is a quick fix
            // foot rotation
            Foot[0].rotation = Quaternion.Lerp(Foot[0].rotation, rotlik, IK.iklx);
            Foot[1].rotation = Quaternion.Lerp(Foot[1].rotation, rotrik, IK.ikrx);
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
            float PosYl = 0;
            float PosYr = 0;

            IK.ikl = Foot[0].position;
            IK.ikr = Foot[1].position;
            rotlik = Foot[0].rotation;
            rotrik = Foot[1].rotation;

            IK.iklx = get_ik_l();
            IK.ikrx = get_ik_r();

            /*if (Physics.Raycast(Foot[0].position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 1, Vecteur.Solid))
            {
                PosYl = hit.point.y - ss.Coord.position.y + 0.1f;
                S.ikl = hit.point + new Vector3(0, 0.1f, 0);
                Rotlik = Quaternion.FromToRotation(Foot[0].forward, hit.normal) * Foot[0].rotation;
            }
            if (Physics.Raycast(Foot[1].position + Vector3.up * 0.5f, Vector3.down, out hit, 1, Vecteur.Solid))
            {
                PosYr = hit.point.y - ss.Coord.position.y + 0.1f;
                S.ikr = hit.point + new Vector3(0, 0.1f, 0);
                Rotrik = Quaternion.FromToRotation(Foot[1].forward, hit.normal) * Foot[1].rotation;
            }*/

            skin.offset_posy = Mathf.Min(PosYl * IK.iklx, PosYr * IK.ikrx, 0);
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
