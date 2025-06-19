using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class p_slash_attack : piece
    {
        public Sword sword { private set; get; }

        Vector3 position;
        Quaternion rotation;
        Vector3 previousPosition;
        Quaternion previousRotation;
        float length;
        float timeLeft;

        static Sword _sword;
        static float _duration;
        public static void Fire ( int name, Sword sword, float duration )
        {
            _sword = sword;
            _duration = duration;
            UnitPoolMaster.GetUnit(name);
        }

        protected override void OnStart()
        {
            sword = _sword;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
            previousPosition = position;
            previousRotation = rotation;
            length = sword.Length;
            timeLeft = _duration;
        }

        public override void Create()
        {
            rays = new Line [5];
        }

        Line [] rays;
        public override void Main()
        {
            Shift ();
            LineCalculation ();
            Raycast ();

            timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                    unit.Return_();
        }

        void Shift ()
        {
            previousPosition = position;
            previousRotation = rotation;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
        }

        void LineCalculation ()
        {
            for (int j = 0; j < 5; j++)
            {
                rays[j] = new Line(previousPosition + previousRotation * Vector3.forward * length * (j / 4f), position + rotation * Vector3.forward * length * (j / 4f));
            }
        }

        void Raycast ()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Physics.Linecast(rays[i].start, rays[i].end, Vecteur.Solid))
                { }
            }
        }
        
        public struct Line
        {
            public Vector3 start;
            public Vector3 end;
            public Line(Vector3 start, Vector3 end)
            {
                this.start = start;
                this.end = end;
            }
        }
    }
}