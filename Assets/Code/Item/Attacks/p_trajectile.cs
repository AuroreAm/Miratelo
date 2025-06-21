using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class p_trajectile : piece
    {
        float speed;
        float timeLeft;
        public Vector3 position;
        Quaternion rotation;
        PieceSkin skin;

        public void Set(PieceSkin skin)
        {
            this.skin = skin;
        }

        protected override void OnStart()
        {
            timeLeft = 30;
            position = _pos;
            rotation = _rot;
            speed = _spd;
        }

        static Vector3 _pos;
        static Quaternion _rot;
        static float _spd;

        public static void Fire ( int name, Vector3 pos, Quaternion rot, float spd)
        {
            _pos = pos;
            _rot = rot;
            _spd = spd;
            UnitPoolMaster.GetUnit(name);
        }

        public override void Main()
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
            unit.Return_();
        }
    }
}