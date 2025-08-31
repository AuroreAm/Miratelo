using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class Sword : Weapon
    {
        public sealed override WeaponType Type { get; } = WeaponType.Sword;
        public sealed override term DefaultDrawAnimation { get; } = AnimationKey.take_sword;
        public sealed override term DefaultReturnAnimation { get; } = AnimationKey.return_sword;

        public float Length = 10;
        // TODO: change this to hash
        public String SlashName;
        public String HookSlashName;

        public void OpenParry ()
        {
            gameObject.layer = Vecteur.ATTACK;
        }

        public void CloseParry ()
        {
            gameObject.layer = 0;
        }
        
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine ( transform.position, transform.position + transform.TransformDirection ( Length * Vector3.forward ) );
    }
    #endif
    }
}