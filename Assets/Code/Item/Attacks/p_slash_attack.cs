using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_slash_attack : ThingSystem <p_slash_attack>
    {
        public static s_slash_attack o;

        public s_slash_attack ()
        {
            o = this;
        }

        public static int Fire ( Sword sword, float duration )
        {
            o.pool.NextPiece ().Set (sword, duration);
            return o.pool.GetPiece ();
        }
    }

    public class p_slash_attack : thing
    {
        public Sword sword { private set; get; }

        Vector3 position;
        Quaternion rotation;
        Vector3 previousPosition;
        Quaternion previousRotation;
        float length;
        float timeLeft;

        public void Set(Sword sword, float duration)
        {
            this.sword = sword;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
            previousPosition = position;
            previousRotation = rotation;
            length = sword.Length;
            this.timeLeft = duration;
        }

        public override void Create()
        {
            rays = new Line [5];
        }

        Line [] rays;
        public override bool Main()
        {
            Shift ();
            LineCalculation ();
            Raycast ();

            timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                    return true;

            return false;
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