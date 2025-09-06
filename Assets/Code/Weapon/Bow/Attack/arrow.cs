using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [star (order.arrow)]
    public class arrow : virtus.star
    {
        public float speed { private set; get; }
        public Vector3 position { private set; get; }
        float timeleft;
        Quaternion rotation;
        skin piece;

        public class ink : ink < arrow >
        {
            public ink (skin skin)
            {
                o.piece = skin;
            }
        }

        protected override void _start ()
        {
            timeleft = 30;
            position = _pos;
            rotation = _rot;
            speed = _spd;
        }

        static Vector3 _pos;
        static Quaternion _rot;
        static float _spd;

        public static void fire (int name, Vector3 pos, Quaternion rot, float spd)
        {
            _pos = pos;
            _rot = rot;
            _spd = spd;
            orion.rent (name);
        }

        protected override void _step ()
        {
            cast ();
            graphic ();
            signal ();
            lifetime ();
        }

        void cast()
        {
            float spd = speed * Time.deltaTime;

            if (Physics.Raycast(position, vecteur.forward (rotation), out RaycastHit Hit, spd, vecteur.SolidCharacterAttack))
            {
                position += vecteur.forward (rotation) * Hit.distance;
                // attack
            }
            else
                position += vecteur.forward (rotation) * spd;
        }

        void graphic()
        {
            Graphics.DrawMesh ( piece.mesh, position, rotation.appied_after (piece.roty), piece.material, 0);
        }

        void signal()
        {
            if ( Physics.Raycast(position, vecteur.forward (rotation), out RaycastHit hit, 2 * speed, vecteur.Character) )
            {
                if (pallas.contains (hit.collider.id())) 
                    pallas.radiate (hit.collider.id(), new incomming_trajectile(position, speed));
            }
        }

        void lifetime()
        {
            timeleft -= Time.deltaTime;

            if (timeleft <= 0)
                virtus.return_();
        }

        
        [Serializable]
        public struct skin
        {
            public Vector3 roty;
            public Mesh mesh;
            public Material material;
        }
    }

    public struct incomming_trajectile
    {
        public Vector3 position;
        public float speed;

        public incomming_trajectile(Vector3 position, float speed)
        {
            this.position = position;
            this.speed = speed;
        }
    }
}
