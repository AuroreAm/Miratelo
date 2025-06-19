using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "RPG/ArrowModel")]
    public class TrajectileAuthor : UnitAuthor
    {
        public PieceSkin Skin;

        protected override void OnInstance(Unit newUnit)
        {
            newUnit.RequirePiece<p_trajectile>().Set(Skin);
        }
    }
}
