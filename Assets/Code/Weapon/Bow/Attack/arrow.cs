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

        protected override void _start ()
        {
            active_arrows.Add (this);

            timeleft = 30;
            position = _pos;
            rotation = _rot;
            speed = _spd;
        }

        protected override void _stop()
        {
            active_arrows.Remove (this);
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

        static List <arrow> active_arrows = new List<arrow>();
        public static void deflect ( Vector3 position, Vector3 normal )
        {
            arrow to = null;
            foreach ( var a in active_arrows )
            if (a.position == position)
            to = a;

            if ( to != null )
            to.rotation = vecteur.rot_direction_quaternion ( Vector3.zero, normal );
        }

        protected override void _step ()
        {
            cast ();
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

        void signal()
        {
            if ( Physics.Raycast(position, vecteur.forward (rotation), out RaycastHit hit, 2 * speed, vecteur.Character) )
            {
                if (pallas.contains ( hit.collider.id()) ) 
                    pallas.radiate ( hit.collider.id(), new incomming_arrow(position, speed) );
            }
        }

        void lifetime()
        {
            timeleft -= Time.deltaTime;

            if (timeleft <= 0)
                virtus.return_();
        }

        [inked]
        public class spectre : Code.spectre
        {
            [link]
            arrow arrow;

            skin skin;

            public class ink : ink <spectre>
            {
                public ink ( skin skin )
                {
                    o.skin = skin;
                }
            }

            protected override void _step ()
            {
                Graphics.DrawMesh ( skin.mesh, arrow.position, arrow.rotation.appied_after (skin.roty), skin.material, 0);
            }
        }
        
        [Serializable]
        public struct skin
        {
            public Vector3 roty;
            public Mesh mesh;
            public Material material;
        }
    }

    public struct incomming_arrow
    {
        public Vector3 position;
        public float speed;

        public incomming_arrow(Vector3 position, float speed)
        {
            this.position = position;
            this.speed = speed;
        }
    }
}