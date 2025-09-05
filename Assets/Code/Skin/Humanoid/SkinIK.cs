using System;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// component with direct access to the animator IK system
    /// </summary>
    public class SkinIk : MonoBehaviour
    {
        public Animator ani;
        public bool on;
        bool _on;

        public Vector3 ikr;
        public Vector3 ikl;

        public float ikrx;
        public float iklx;

        public Action _ik;

        public Action _late_ik;

        void Awake()
        {
           ani = GetComponent<Animator>();
        }

        void LateUpdate()
        {
            _late_ik?.Invoke();
        }

        void OnAnimatorIK()
        {
            _ik?.Invoke();

            if (_on)
            {
                ani.SetIKPosition(AvatarIKGoal.LeftFoot, ikl);
                ani.SetIKPosition(AvatarIKGoal.RightFoot, ikr);
                ani.SetIKPositionWeight(AvatarIKGoal.LeftFoot, iklx);
                ani.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikrx);

                if (on == false)
                {
                    _on = false;
                    iklx = 0;
                    ikrx = 0;
                    ani.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                    ani.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                }
            }
            else
            {
                if (on)
                    _on = true;
            }
        }
    }
}
