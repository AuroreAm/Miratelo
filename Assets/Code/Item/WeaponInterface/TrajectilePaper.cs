using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu (menuName = "RPG/trajectile paper") ]
    public class TrajectilePaper : ScriptableObject
    {
        public PieceSkin Skin;

        public void Shoot ( Vector3 pos, Quaternion rot, float speed )
        {
            s_trajectile.Fire ( Skin, pos, rot , speed );
        }

    }
}
