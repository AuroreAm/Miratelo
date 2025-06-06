using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class Sword : Weapon
    {
        public sealed override WeaponType Type { get; } = WeaponType.Sword;
        public sealed override SuperKey DefaultDrawAnimation { get; } = AnimationKey.take_sword;
        public sealed override SuperKey DefaultReturnAnimation { get; } = AnimationKey.return_sword;

        public float Lenght = 10;

        
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine ( transform.position, transform.position + transform.TransformDirection ( Lenght * Vector3.forward ) );
    }
    #endif
    }
}