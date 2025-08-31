using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class a_trajectile : virtus.pixi
    {
        public float speed { private set; get; }
        float timeLeft;
        public Vector3 position { private set; get; }
        Quaternion rotation;
        PieceSkin skin;

        public class package : PreBlock.Package <a_trajectile>
        {
            public package ( PieceSkin skin )
            {
                o.skin = skin;
            }
        }

        protected override void Start()
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
            VirtualPoolMaster.RentVirtus(name);
        }

        protected override void Step()
        {
            ShootCast ();
            DrawGraphic ();
            SendAlert ();
            LifeTime ();
        }

        void ShootCast ()
        {
            float spd = speed * Time.deltaTime;

            if (Physics.Raycast( position, Vecteur.Forward(rotation), out RaycastHit Hit, spd, Vecteur.SolidCharacterAttack) )
            {
                position += Vecteur.Forward(rotation) * Hit.distance;
                // attack
            }
            else
                position += Vecteur.Forward(rotation) * spd;
        }

        void DrawGraphic ()
        {
            Graphics.DrawMesh(skin.Mesh, position, rotation.AppliedAfter(skin.RotY), skin.Material, 0);
        }

        void SendAlert ()
        {
            if (Physics.Raycast(position, Vecteur.Forward(rotation), out RaycastHit hit, 2 * speed, Vecteur.Character))
            {
                if (Element.Contains(hit.collider.id()))
                {
                    Element.SendMessage(hit.collider.id(), new incomming_trajectile(position, speed));
                }
            }
        }

        void LifeTime ()
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            v.Return_();
        }
    }

    public struct incomming_trajectile
    {
        public Vector3 position;
        public float speed;

        public incomming_trajectile ( Vector3 position, float speed )
        {
            this.position = position;
            this.speed = speed;
        }
    }
}