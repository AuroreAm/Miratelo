using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    // IK foot manager for humanoids
    public class s_skin_foot_ik : shard
    {
        [harmony]
        d_ground_data groundData;
        [harmony]
        s_skin skin;

        SkinIk skinIK;
        Transform[] Foot;
        public float GetLeftFootIkCurves() => skinIK.Ani.GetFloat(Hash.lik);
        public float GetRightFootIkCurves() => skinIK.Ani.GetFloat(Hash.rik);

        public enum FootId { left, right }
        public FootId DominantFoot { private set; get; }

        protected override void harmony ()
        {
            skinIK = skin.Ani.gameObject.AddComponent<SkinIk>();
            skinIK.OnIk += FootIk;
            skinIK.LateIk += CheckIkRot;

            Foot = new Transform[] { skinIK.Ani.GetBoneTransform(HumanBodyBones.LeftFoot), skinIK.Ani.GetBoneTransform(HumanBodyBones.RightFoot) };
        }

        Quaternion rotlik;
        Quaternion rotrik;

        void FootIk()
        {
            if (!skinIK.IKon && groundData.onGround)
                skinIK.IKon = true;

            if (skinIK.IKon)
            {
                CalculateIkPos();
                CheckCanContinueIk();
            }
            UpdateDominantFoot();
        }

        void CheckIkRot()
        {
            // TODO: foot rotation are different from character to character, this is a quick fix
            // foot rotation
            Foot[0].rotation = Quaternion.Lerp(Foot[0].rotation, rotlik, skinIK.iklx);
            Foot[1].rotation = Quaternion.Lerp(Foot[1].rotation, rotrik, skinIK.ikrx);
        }

        void UpdateDominantFoot()
        {
            if (DominantFoot == FootId.right && GetLeftFootIkCurves() > GetRightFootIkCurves())
                DominantFoot = FootId.left;
            if (DominantFoot == FootId.left && GetLeftFootIkCurves() < GetRightFootIkCurves())
                DominantFoot = FootId.right;
        }

        void CalculateIkPos()
        {
            float PosYl = 0;
            float PosYr = 0;

            skinIK.ikl = Foot[0].position;
            skinIK.ikr = Foot[1].position;
            rotlik = Foot[0].rotation;
            rotrik = Foot[1].rotation;

            skinIK.iklx = GetLeftFootIkCurves();
            skinIK.ikrx = GetRightFootIkCurves();

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

            skin.OffsetPosY = Mathf.Min(PosYl * skinIK.iklx, PosYr * skinIK.ikrx, 0);
        }

        void CheckCanContinueIk()
        {
            // if not on ground, disable Ik
            if (!groundData.onGround)
            {
                skin.OffsetPosY = 0;
                skinIK.IKon = false;
            }
        }
    }
}
