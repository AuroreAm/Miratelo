using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class a_slash_attack : virtus.pixi
    {
        public Sword sword { private set; get; }

        Vector3 position;
        Quaternion rotation;
        Vector3 previousPosition;
        Quaternion previousRotation;
        float length;
        float timeLeft;
        Action<int> onHit;

        static Sword _sword;
        static float _duration;
        public static void Fire ( int name, Sword sword, float duration )
        {
            _sword = sword;
            _duration = duration;
            VirtualPoolMaster.RentVirtus(name);
        }

        protected override void Start()
        {
            sword = _sword;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
            previousPosition = position;
            previousRotation = rotation;
            length = sword.Length;
            timeLeft = _duration;
            Hitted.Clear ();
        }

        protected override void Create1()
        {
            rays = new Line [5];
            hit = new RaycastHit[5];
        }

        Line [] rays;
        protected override void Step()
        {
            Shift ();
            LineCalculation ();
            Raycast ();

            timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                    v.Return_();
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

        RaycastHit[] hit;
        float hitNumber;
        List <int> Hitted = new List<int>();
        void Raycast ()
        {
            for (int i = 0; i < 5; i++)
            {
                hitNumber = Physics.SphereCastNonAlloc( rays[i].Ray, .25f, hit, rays[i].Distance,Vecteur.SolidCharacter );

                if (hitNumber > 0)
                {
                    for (int j = 0; j < hitNumber; j++)
                    {
                        if ( !Hitted.Contains (hit[j].collider.id()) && Element.ElementActorIsNotAlly ( hit[j].collider.id (), sword.Owner.faction ) )
                        {
                            Element.Clash ( sword.element, hit[j].collider.id (), new Slash (1,hit[j].point, rays[i].Ray.direction, sword.Sharpness) );
                            onHit?.Invoke ( hit[j].collider.id () );
                            Hitted.Add ( hit[j].collider.id () );
                        }
                    }
                }
            }
        }

        public void Link ( Action <int> _OnHit )
        {
            onHit = _OnHit;
        }
        
        public struct Line
        {
            public Ray Ray;
            public float Distance;
            public Line(Vector3 start, Vector3 end)
            {
                Ray = new Ray(start, end - start);
                Distance = Vector3.Distance(start, end);
            }
        }
    }
}