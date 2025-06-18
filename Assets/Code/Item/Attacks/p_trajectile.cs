using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class s_trajectile : ThingSystem<p_trajectile>
    {
        public static s_trajectile o;

        public s_trajectile()
        { o = this; }

        public static int Fire(PieceSkin skin, Vector3 pos, Quaternion rot, float spd)
        {
            o.pool.NextPiece().Set(skin, pos, rot, spd);
            return o.pool.GetPiece();
        }
    }

    public class p_trajectile : thing
    {
        float speed;
        float timeLeft;
        Vector3 position;
        Quaternion rotation;
        PieceSkin skin;

        public void Set(PieceSkin skin, Vector3 pos, Quaternion rot, float spd)
        {
            speed = spd;
            this.skin = skin;
            position = pos;
            rotation = rot;
            speed = spd;
            timeLeft = 30;
        }

        public override bool Main()
        {
            float spd = speed * Time.deltaTime;

            if (Physics.Raycast(position, Vecteur.Forward(rotation), out RaycastHit Hit, spd, Vecteur.SolidCharacterAttack))
            {
                position += Vecteur.Forward(rotation) * Hit.distance;
                // attack
            }
            else
                position += Vecteur.Forward(rotation) * spd;

            Graphics.DrawMesh(skin.Mesh, position, rotation.AppliedAfter(skin.RotY), skin.Material, 0);

            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
                return true;

            return false;
        }
    }
}