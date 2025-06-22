using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_camera_shake : module
    {
        [Depend]
        m_camera mc;
        Animator Ani;

        static m_camera_shake o;
        public override void Create()
        {
            o = this;
        }

        public void Set ( Animator ani )
        {
            Ani = ani;
        }


        public static void Shake ( Vector3 pos )
        {
            o.Ani.Play ( "shake0" );
        }
    }
}