using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class s_trajectile : PieceSystem <s_trajectile.d_trajectile>
    {
        public static s_trajectile o;

        public s_trajectile ()
        {
            o = this;
        }

        public override void Execute()
        {
            float spd;

            for (int i = pieces.Count - 1; i >= 0; i--)
            {
                spd = pieces [i].speed * Time.deltaTime;

                if (Physics.Raycast (pieces [i].position, Vecteur.Forward (pieces [i].rotation), out RaycastHit Hit, spd, Vecteur.SolidCharacterAttack ))
                {
                    pieces [i].position += Vecteur.Forward (pieces [i].rotation) * Hit.distance;
                    Debug.Log ( "hit ");
                }
                else
                pieces [i].position += Vecteur.Forward (pieces [i].rotation) * spd;

                Graphics.DrawMesh( pieces [i].skin.Mesh, pieces [i].position, pieces [i].rotation.AppliedAfter(pieces [i].skin.RotY), pieces [i].skin.Material, 0);

                pieces[i].timeLeft -= Time.deltaTime;
                if (pieces[i].timeLeft <= 0)
                    ReturnPiece(pieces[i]);
            }
        }

        public static void Fire ( PieceSkin skin, Vector3 pos, Quaternion rot, float spd )
        {
            o.PeekPiece().Set ( skin, pos, rot, spd );
            o.GetPiece ();
        }

        public class d_trajectile : piece
        {
            public float speed;
            public float timeLeft;
            public Vector3 position;
            public Quaternion rotation;
            public PieceSkin skin;

            public void Set ( PieceSkin skin, Vector3 pos, Quaternion rot, float spd )
            {
                speed = spd;
                this.skin = skin;
                position = pos;
                rotation = rot;
                speed = spd;
            }
        }
    }
}